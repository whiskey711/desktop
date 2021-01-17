using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
using ECG_ISHNE;

namespace Uvic_Ecg_ArbutusHolter.DownloadData
{
    class DownloadMethod
    {
        PatientResource pResource = new PatientResource();
        EcgDataResources eResource = new EcgDataResources();
        Client inClient;
        public DownloadMethod(Client client)
        {
            inClient = client;
        }
        public async Task<bool> GetOneTestAndPatient(EcgTest t)
        {
            try
            {
                ManageFile.CheckFolder(ManageFile.testFolderName + t.EcgTestId);
                ManageFile.SaveTestInfo(t, ManageFile.testFolderName + t.EcgTestId + ManageFile.testInfoName);
                bool b = await GetData(t);
                if (!b)
                {
                    goto Failed;
                }
                RestModel<PatientInfo> pMod = await pResource.GetPatientById(t.PatientId, inClient);
                if (ErrorInfo.OK.ErrorMessage != pMod.ErrorMessage)
                {
                    goto Failed;
                }
                PatientInfo p = pMod.Entity.Model;
                ManageFile.SavePatientInfo(p, ManageFile.testFolderName + t.EcgTestId + ManageFile.patientInfoName);
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
                goto Failed;
            }
            goto Success;
        Success:
            return true;
        Failed:
            return false;
        }
        public async Task<bool> GetData(EcgTest t)
        {
            List<Exception> exls = new List<Exception>();
            RestModel<EcgRawData> rawDataMod;
            try
            {
                // rawDataMod stores ecgRawData objects without actual ecg data
                rawDataMod = await eResource.GetRawDataLs(inClient, t.EcgTestId);
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
                    rawDataMod = await eResource.GetData(inClient, d.EcgRawDataId);
                    if (ErrorInfo.OK.ErrorMessage != rawDataMod.ErrorMessage || !CompareSize(d, rawDataMod.Entity.Model))
                    {
                        goto Failed;
                    }
                    ManageFile.CheckFolder(ManageFile.testFolderName + t.EcgTestId + ManageFile.dataFolderName + d.EcgRawDataId);
                    ManageFile.SaveData(rawDataMod.Entity.Model, ManageFile.testFolderName + t.EcgTestId +
                                        ManageFile.dataFolderName + d.EcgRawDataId + ManageFile.dataName);
                    ManageFile.SaveDataInfo(d, ManageFile.testFolderName + t.EcgTestId +
                                            ManageFile.dataFolderName + d.EcgRawDataId + ManageFile.dataInfoName);
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
        public static bool CompareSize(EcgRawData thedata, EcgRawData actualData)
        {
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
                string msg = ManageFile.rootMsg + fex.Message + ManageFile.wrongRawDataMsg + actualData.EcgRawDataId;
                throw new FileRelatedException(msg, fex);
            }
            catch (ArgumentException aex)
            {
                string msg = ManageFile.rootMsg + aex.Message + ManageFile.wrongRawDataMsg + actualData.EcgRawDataId;
                throw new FileRelatedException(msg, aex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CheckIntegrity(DirectoryInfo dir)
        {
            bool integrity = true;
            Dictionary<int, FileInfo> localDataDict = new Dictionary<int, FileInfo>();
            List<EcgRawData> CompleteDataLs = new List<EcgRawData>();
            FileInfo result;
            List<Exception> exls = new List<Exception>();
            RestModel<EcgRawData> rawDataMod;
            try
            {
                EcgTest testJson = ManageFile.ReadTestJson(dir.FullName + ManageFile.testInfoName);
                PatientInfo patient = ManageFile.ReadPatientInfo(dir.FullName + ManageFile.patientInfoName);
                rawDataMod = await eResource.GetRawDataLs(inClient, testJson.EcgTestId);
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
                        integrity = false;
                        continue;
                    }
                    else if (d.Size != result.Length)
                    {
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
        public static void ConverToIsne(DirectoryInfo dir)
        {
            try
            {
                List<string> rawDataPathLs = ManageFile.GetLocalDataPathLs(dir);
                PatientInfo patient = ManageFile.ReadPatientInfo(dir.FullName + ManageFile.patientInfoName);
                EcgTest test = ManageFile.ReadTestJson(dir.FullName + ManageFile.testInfoName);
                ISHNEUtility.ConvertToISHNE(rawDataPathLs, patient, test, dir.FullName + ManageFile.ishneName);
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
        public static List<EcgRawData> CreateDataLs(List<Entity<EcgRawData>> entls)
        {
            List<EcgRawData> dls = new List<EcgRawData>();
            foreach (var ent in entls)
            {
                dls.Add(ent.Model);
            }
            return dls;
        }
        public static List<EcgTest> CreateTestLs(List<Entity<EcgTest>> entls)
        {
            List<EcgTest> els = new List<EcgTest>();
            foreach (var ent in entls)
            {
                els.Add(ent.Model);
            }
            return els;
        }
    }
}
