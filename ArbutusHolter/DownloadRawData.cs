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
        readonly string testFolderName = @"\ecgTest";
        readonly string dataFolderName = @"\rawData";
        readonly string testInfoName = @"\test.json";
        readonly string dataInfoName = @"\rawData.json";
        readonly string patientInfoName = @"\patient.json";
        readonly string dataName = @"\data";
        readonly string ishneFileName = @"\ishne";
        readonly string rootMsg = "An exception occurred during the program downloading ecg data in the background ";
        readonly string wrongRawDataMsg = " the problemed ecg raw data is ecgRawData ";
        readonly DateTime earliestTime = DateTime.Parse("2000/01/01");
        public async Task MainProcess(Client client)
        {
            List<Exception> exls = new List<Exception>();
            drwClient = client;
            List<DirectoryInfo> testDirs = new List<DirectoryInfo>();
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
                        b = await GetTestAndPatient(start, end);
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
                        b = await GetTestAndPatient(start, end);
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
                
                    b = await CheckIntegrity(testDir);
                    if (b)
                    {
                        // convertIshne once have data
                        ConverToIsne(testDir);
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
        private async Task<bool> GetTestAndPatient(DateTime start, DateTime end)
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
            List<EcgTest> finishTestLs = CreateTestLs(ecgTestMod.Feed.Entities);
            foreach (EcgTest t in finishTestLs)
            {
                try
                {
                    ManageFile.CheckFolder(testFolderName + t.EcgTestId);
                    ManageFile.SaveTestInfo(t, testFolderName + t.EcgTestId + testInfoName);
                    b = await GetData(t);
                    if (!b)
                    {
                        failedTestLs.Add(t);
                    }
                    pMod = await pResource.GetPatientById(t.PatientId, drwClient);
                    if (ErrorInfo.OK.ErrorMessage != pMod.ErrorMessage)
                    {
                        continue;
                    }
                    PatientInfo p = pMod.Entity.Model;
                    ManageFile.SavePatientInfo(p, testFolderName + t.EcgTestId + patientInfoName);
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
        private async Task<bool> GetData(EcgTest t)
        {
            List<Exception> exls = new List<Exception>();
            try
            {
                // rawDataMod stores ecgRawData objects without actual ecg data
                rawDataMod = await eResource.GetRawDataLs(drwClient, t.EcgTestId);
                if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage)
                {
                    goto Failed;
                }
            }
            catch (HttpRequestException hrex)
            {
                throw hrex;
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
            List<EcgRawData> dataLs = CreateDataLs(rawDataMod.Feed.Entities);

            foreach (EcgRawData d in dataLs)
            {
                // rawDataMod stores ecgRawData objects with only actual ecg data
                try
                {
                    rawDataMod = await eResource.GetData(drwClient, d.EcgRawDataId);
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage || !CompareSize(d))
                    {
                        failedRawDataLs.Add(d);
                        continue;
                    }
                    ManageFile.CheckFolder(testFolderName + t.EcgTestId + dataFolderName + d.EcgRawDataId);
                    ManageFile.SaveData(rawDataMod.Entity.Model, testFolderName + t.EcgTestId +
                                        dataFolderName + d.EcgRawDataId + dataName);
                    ManageFile.SaveDataInfo(d, testFolderName + t.EcgTestId +
                                            dataFolderName + d.EcgRawDataId + dataInfoName);
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
                    ManageFile.SaveTestInfo(t, testFolderName + t.EcgTestId + testInfoName);
                    rawDataMod = await eResource.GetRawDataLs(drwClient, t.EcgTestId);
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage)
                    {
                        continue;
                    }
                    List<EcgRawData> dataLs = CreateDataLs(rawDataMod.Feed.Entities);
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
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage || !CompareSize(d))
                    {
                        continue;
                    }
                    string subPath = testFolderName + d.EcgTestId + dataFolderName + d.EcgRawDataId;
                    ManageFile.CheckFolder(subPath);
                    ManageFile.SaveDataInfo(d, subPath + dataInfoName);
                    ManageFile.SaveData(rawDataMod.Entity.Model, subPath + dataName);
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
                EcgTest t = ManageFile.ReadTestJson(testDir.FullName + testInfoName);
                pMod = await pResource.GetPatientById(t.PatientId, drwClient);
                if (ErrorInfo.OK.ErrorMessage != pMod.ErrorMessage)
                {
                    return;
                }
                PatientInfo p = pMod.Entity.Model;
                ManageFile.SavePatientInfo(p, @"\" + testDir.Name + patientInfoName);
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
        private async Task<bool> CheckIntegrity(DirectoryInfo dir)
        {
            bool integrity = true;
            Dictionary<int, FileInfo> localDataDict = new Dictionary<int, FileInfo>();
            List<EcgRawData> CompleteDataLs = new List<EcgRawData>();
            FileInfo result;
            List<Exception> exls = new List<Exception>();
            try
            { 
                EcgTest testJson = ManageFile.ReadTestJson(dir.FullName + testInfoName);
                PatientInfo patient = ManageFile.ReadPatientInfo(dir.FullName + patientInfoName);
                rawDataMod = await eResource.GetRawDataLs(drwClient, testJson.EcgTestId);
                if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage)
                {
                    goto NotReady;
                }
                CompleteDataLs = CreateDataLs(rawDataMod.Feed.Entities);
                localDataDict = ManageFile.GetLocalDataDict(dir);    
            }
            catch (HttpRequestException hrex)
            {
                throw hrex;
            }
            catch (FileRelatedException frex)
            {
                MessageBox.Show(frex.Message);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(frex.ToString(), frex.StackTrace, w);
                }
                goto NotReady;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                goto NotReady;
            }
            foreach (EcgRawData d in CompleteDataLs)
            {
                try
                {
                    if (!localDataDict.TryGetValue(d.EcgRawDataId, out result))
                    {
                        failedRawDataLs.Add(d);
                        integrity = false;
                        continue;
                    }
                    else if (d.Size != result.Length)
                    {
                        failedRawDataLs.Add(d);
                        integrity = false;
                    }
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
                goto NotReady;
            }
            return integrity;
            
        NotReady:
            return false;
        }
        private void ConverToIsne(DirectoryInfo dir)
        {
            try
            {
                List<string> rawDataPathLs = ManageFile.GetLocalDataPathLs(dir);
                PatientInfo patient = ManageFile.ReadPatientInfo(dir.FullName + patientInfoName);
                EcgTest test = ManageFile.ReadTestJson(dir.FullName + testInfoName);
                ISHNEUtility.ConvertToISHNE(rawDataPathLs, patient, test, dir.FullName + ishneFileName);
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
        private List<EcgTest> CreateTestLs(List<Entity<EcgTest>> entls)
        {
            List<EcgTest> els = new List<EcgTest>();
            foreach (var ent in entls)
            {
                els.Add(ent.Model);
            }
            return els;
        }
        private List<EcgRawData> CreateDataLs(List<Entity<EcgRawData>> entls)
        {
            List<EcgRawData> dls = new List<EcgRawData>();
            foreach (var ent in entls)
            {
                dls.Add(ent.Model);
            }
            return dls;
        }
        private bool CompareSize(EcgRawData thedata)
        {
            if (rawDataMod.Entity.Model == null)
            {
                return false;
            }
            EcgRawData actualData = rawDataMod.Entity.Model;
            string stringOfData = actualData.RawData;
            try
            {
                byte[] data = Convert.FromBase64String(stringOfData);
                if (data.Length == thedata.Size)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException fex)
            {
                string msg = rootMsg + fex.Message + wrongRawDataMsg + actualData.EcgRawDataId;
                throw new FileRelatedException(msg, fex);
            }
            catch (ArgumentException aex)
            {
                string msg = rootMsg + aex.Message + wrongRawDataMsg + actualData.EcgRawDataId;
                throw new FileRelatedException(msg, aex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
