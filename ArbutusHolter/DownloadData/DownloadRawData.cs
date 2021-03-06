using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
using Uvic_Ecg_ArbutusHolter.DownloadData;
using ECG_ISHNE;
using System.Dynamic;
using Newtonsoft.Json;

namespace Uvic_Ecg_ArbutusHolter
{
    class DownloadRawData
    {
        Client drwClient;
        RestModel<EcgTest> ecgTestMod;
        RestModel<EcgRawData> rawDataMod;
        RestModel<PatientInfo> pMod;
        List<EcgTest> failedTestLs = new List<EcgTest>();
        List<EcgRawData> failedRawDataLs = new List<EcgRawData>();
        List<int> failedPatientIdLs = new List<int>();
        EcgDataResources eResource = new EcgDataResources();
        PatientResource pResource = new PatientResource();
        DateTime start, end;
        string lastReqTime;
        bool b;
        readonly DateTime earliestTime = DateTime.Parse("2000/01/01");
        public async Task MainProcess(Client client)
        {
            drwClient = client;
            List<Exception> exls = new List<Exception>();           
            List<DirectoryInfo> testDirs = new List<DirectoryInfo>();
            DownloadMethod method = new DownloadMethod(drwClient);
            // Check root folder, failedtest and faileddata files. If no such folder and files, create new one 
            try
            {
                ManageFile.CheckFolder(null);
                if (ManageFile.CheckFailedTest())
                {
                    failedTestLs = ManageFile.ReadFailedTestLs();
                }
                if (ManageFile.CheckFailedData())
                {
                    failedRawDataLs = ManageFile.ReadFailedDataLs();
                }
                // Check lastrequest file existence
                if (!ManageFile.CheckLastReqest())
                {
                    DialogResult res = MessageBox.Show(ErrorInfo.DownloadAllData.ErrorMessage, ErrorInfo.Caption.ErrorMessage, MessageBoxButtons.YesNo);
                    if (DialogResult.No == res)
                    {
                        ManageFile.UpdateRequestTime(DateTime.Now);
                    }
                    else if (DialogResult.Yes == res)
                    {
                        start = earliestTime;
                        end = DateTime.Now;
                        b = await GetTestAndPatient(start, end, method);
                        if (b)
                        {
                            ManageFile.UpdateRequestTime(DateTime.Now);
                        }
                        else
                        {
                            ManageFile.UpdateRequestTime(start);
                        }
                    }

                }
                else
                {
                    lastReqTime = ManageFile.ReadLastReqTime();
                    if (string.IsNullOrEmpty(lastReqTime))
                    {
                        ManageFile.UpdateRequestTime(DateTime.Now);
                    }
                    else if (DateTime.TryParse(lastReqTime, out start))
                    {
                        end = DateTime.Now;
                        b = await GetTestAndPatient(start, end, method);
                        if (b)
                        {
                            ManageFile.UpdateRequestTime(DateTime.Now);
                        }
                    }
                }
                await GetFailedTest();
                await GetFailedData();
                testDirs = ManageFile.GetDirectoryInfos();
            }
            catch (TokenExpiredException teex)
            {
                throw teex;
            }
            catch (HttpRequestException hrex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(hrex.ToString(), hrex.StackTrace, w);
                }
                MessageBox.Show(ErrorInfo.ConnectionProblem.ErrorMessage);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            foreach (DirectoryInfo testDir in testDirs)
            {
                try
                {
                    // Get failed patient has to check all folder on harddrive
                    await GetFailedPatient(testDir);
                    if (ManageFile.CheckIsne(testDir))
                    {
                        Console.WriteLine("skip");
                        continue;
                    }
                
                    b = await method.CheckIntegrity(testDir);
                    if (b)
                    {
                        // convertIshne once have data
                        DownloadMethod.ConverToIsne(testDir);
                        Console.WriteLine("In");
                    }
                }
                catch (TokenExpiredException teex)
                {
                    throw teex;
                }
                catch (HttpRequestException hrex)
                {
                    using (StreamWriter w = File.AppendText(FileName.Log.Name))
                    {
                        LogHandle.Log(hrex.ToString(), hrex.StackTrace, w);
                    }
                    MessageBox.Show(ErrorInfo.ConnectionProblem.ErrorMessage);
                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                AggregateException agex = new AggregateException(exls);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(agex.ToString(), agex.StackTrace, w);
                }
                MessageBox.Show(agex.Message);
            }
            try
            { 
                ManageFile.ReplaceFailedTest(failedTestLs);
                ManageFile.ReplaceFailedData(failedRawDataLs);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                MessageBox.Show(ex.Message);
            }

        }
        private async Task<bool> GetTestAndPatient(DateTime start, DateTime end, DownloadMethod method)
        {
            List<Exception> exls = new List<Exception>();
            try
            {
                ecgTestMod = await eResource.GetFinishedTest(drwClient, start, end);
            }
            catch (HttpRequestException hrex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(hrex.ToString(), hrex.StackTrace, w);
                }
                goto Failed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                goto Failed;
            }
            if (ErrorInfo.OK.ErrorMessage != ecgTestMod.ErrorMessage)
            {
                MessageBox.Show(ecgTestMod.ErrorMessage);
                goto Failed;
                
            }
            List<EcgTest> finishTestLs = DownloadMethod.CreateTestLs(ecgTestMod.Feed.Entities);
            foreach (EcgTest t in finishTestLs)
            {
                try
                {
                    await method.GetOneTestAndPatient(t);
                }
                catch (HttpRequestException hrex)
                {
                    throw hrex;
                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                AggregateException agex = new AggregateException(exls);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(agex.ToString(), agex.StackTrace, w);
                }
                MessageBox.Show(agex.Message);
                goto Failed;
            }
            goto Success;
        Success:
            return true;
        Failed:
            return false;
        }
        private async Task GetFailedTest()
        {
            List<int> index = new List<int>();
            List<Exception> exls = new List<Exception>();
            foreach (EcgTest t in failedTestLs)
            {
                try
                {
                    // serv has api to get ecg test by ecgtest id
                    ManageFile.SaveTestInfo(t, ManageFile.testFolderName + t.EcgTestId + ManageFile.testInfoName);
                    rawDataMod = await eResource.GetRawDataLs(drwClient, t.EcgTestId);
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage)
                    {
                        continue;
                    }
                    List<EcgRawData> dataLs = DownloadMethod.CreateDataLs(rawDataMod.Feed.Entities);
                    foreach (EcgRawData d in dataLs)
                    {
                        failedRawDataLs.Add(d);
                    }
                    index.Add(failedTestLs.IndexOf(t));
                }
                catch (HttpRequestException hrex)
                {
                    throw hrex;
                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                AggregateException agex = new AggregateException(exls);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(agex.ToString(), agex.StackTrace, w);
                }
                MessageBox.Show(agex.Message);
            }
            for (int i = index.Count - 1; i >= 0; i--)
            {
                failedTestLs.RemoveAt(index[i]);
            }
        }
        private async Task GetFailedData()
        {
            List<int> index = new List<int>();
            List<Exception> exls = new List<Exception>();
            foreach (EcgRawData d in failedRawDataLs)
            {
                try
                {
                    rawDataMod = await eResource.GetData(drwClient, d.EcgRawDataId);
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage || !DownloadMethod.CompareSize(d, rawDataMod.Entity.Model))
                    {
                        continue;
                    }
                    string subPath = ManageFile.testFolderName + d.EcgTestId + ManageFile.dataFolderName + d.EcgRawDataId;
                    ManageFile.CheckFolder(subPath);
                    ManageFile.SaveDataInfo(d, subPath + ManageFile.dataInfoName);
                    ManageFile.SaveData(rawDataMod.Entity.Model, subPath + ManageFile.dataName);
                    index.Add(failedRawDataLs.IndexOf(d));
                }
                catch (HttpRequestException hrex)
                {
                    throw hrex;
                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                AggregateException agex = new AggregateException(exls);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(agex.ToString(), agex.StackTrace, w);
                }
                MessageBox.Show(agex.Message);
            }
            for (int i = index.Count - 1; i >= 0; i--)
            {
                failedRawDataLs.RemoveAt(index[i]);
            }
        }
        private async Task GetFailedPatient(DirectoryInfo testDir)
        {
            try
            {
                if (ManageFile.CheckPatient(testDir))
                {
                    return;
                }
                EcgTest t = ManageFile.ReadTestJson(testDir.FullName + ManageFile.testInfoName);
                pMod = await pResource.GetPatientById(t.PatientId, drwClient);
                if (ErrorInfo.OK.ErrorMessage != pMod.ErrorMessage)
                {
                    return;
                }
                PatientInfo p = pMod.Entity.Model;
                ManageFile.SavePatientInfo(p, @"\" + testDir.Name + ManageFile.patientInfoName);
            }
            catch (HttpRequestException hrex)
            {
                throw hrex;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                MessageBox.Show(ex.Message);
            }
        }
    }
}
