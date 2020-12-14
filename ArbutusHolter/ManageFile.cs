using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using Newtonsoft.Json;
using Uvic_Ecg_Model;
using PdfSharp.Pdf.Annotations;
using System.Windows.Forms;

namespace Uvic_Ecg_ArbutusHolter
{
    class ManageFile
    {
        private static readonly string folderName = Directory.GetParent(Directory.GetCurrentDirectory())
                                                    .Parent.Parent.FullName + @"\Data";
        private static readonly string lastRequestPath = folderName + @"\lastrequesttime.txt";
        private static readonly string failedTestPath = folderName + @"\failedtestls.json";
        private static readonly string failedDataPath = folderName + @"\faileddatals.json";
        private static readonly string testFolderName = "ecgTest";
        private static readonly string dataFolderName = "rawData";
        private static readonly string testInfoName = "test.json";
        private static readonly string dataInfoName = "rawData.json";
        private static readonly string patientInfoName = @"\patient.json";
        private static readonly string dataName = @"\data";
        private static readonly string dataPattern = "data*";
        private static readonly string ishneName = @"\ishne";
        private static readonly string rootMsg = "An exception occurred during the program downloading ecg data in the background ";
        public static void CheckFolder(string subPath)
        {
            try
            {
                Directory.CreateDirectory(folderName + subPath);
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }  
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + folderName + subPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + folderName + subPath + "\n" + nsex.Message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool CheckFailedTest()
        {
            try
            {
                return File.Exists(failedTestPath);
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedTestPath + "\n" + aex.Message; 
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedTestPath + "\n" + nsex.Message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public static bool CheckFailedData()
        {
            try
            {
                return File.Exists(failedDataPath);
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedDataPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedDataPath + "\n" + nsex.Message;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public static void UpdateRequestTime(DateTime time)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(lastRequestPath))
                {
                    file.WriteLine(time);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedDataPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedDataPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateFailedTest(EcgTest test)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(failedTestPath, true))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, test);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedTestPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedTestPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateFailedData(EcgRawData data)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(failedDataPath, true))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedDataPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedDataPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ReplaceFailedTest(List<EcgTest> testLs)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(failedTestPath, false))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, testLs);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedTestPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedTestPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ReplaceFailedData(List<EcgRawData> dataLs)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(failedDataPath, false))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, dataLs);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedDataPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + failedDataPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveTestInfo(EcgTest test, string path)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(folderName + path, false))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, test);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + folderName + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + folderName + path + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SavePatientInfo(PatientInfo patient, string path)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(folderName + path, false))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, patient);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + folderName + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + folderName + path + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveDataInfo(EcgRawData data, string path)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(folderName + path, false))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                }
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + folderName + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + folderName + path + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<EcgTest> ReadFailedTestLs()
        {
            try
            {
                List<EcgTest> ls = new List<EcgTest>();
                using (StreamReader file = new StreamReader(failedTestPath))
                {
                    string json = file.ReadToEnd();
                    ls = JsonConvert.DeserializeObject<List<EcgTest>>(json);
                }
                return ls;
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedTestPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (OutOfMemoryException omex)
            {
                string msg = rootMsg + omex.Message;
                throw new FileRelatedException(msg, omex);
            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<EcgRawData> ReadFailedDataLs()
        {
            try
            {
                List<EcgRawData> ls = new List<EcgRawData>();
                using (StreamReader file = new StreamReader(failedDataPath))
                {
                    string json = file.ReadToEnd();
                    ls = JsonConvert.DeserializeObject<List<EcgRawData>>(json);
                }
                return ls;
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + failedDataPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (OutOfMemoryException omex)
            {
                string msg = rootMsg + omex.Message;
                throw new FileRelatedException(msg, omex);
            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static EcgTest ReadTestJson(string path)
        {
            try
            {
                EcgTest t;
                using (StreamReader file = new StreamReader(path))
                {
                    string json = file.ReadToEnd();
                    t = JsonConvert.DeserializeObject<EcgTest>(json);
                }
                return t;
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (OutOfMemoryException omex)
            {
                string msg = rootMsg + omex.Message;
                throw new FileRelatedException(msg, omex);
            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static PatientInfo ReadPatientInfo(string path)
        {
            try
            {
                PatientInfo p;
                using (StreamReader file = new StreamReader(path))
                {
                    string json = file.ReadToEnd();
                    p = JsonConvert.DeserializeObject<PatientInfo>(json);
                }
                return p;
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (OutOfMemoryException omex)
            {
                string msg = rootMsg + omex.Message;
                throw new FileRelatedException(msg, omex);
            }
            catch (JsonException jex)
            {
                string msg = rootMsg + jex.Message;
                throw new FileRelatedException(msg, jex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveData(EcgRawData data, string path)
        {
            try
            {
                string stringOfData = data.RawData;
                byte[] dataByte = Convert.FromBase64String(stringOfData);
                File.WriteAllBytes(folderName + path, dataByte);
            }
            catch (FormatException fex)
            {
                string msg = rootMsg + fex.Message;
                throw new FileRelatedException(msg, fex);

            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + folderName + path + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + folderName + path + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ReadLastReqTime()
        {
            try
            {
                string time = File.ReadAllText(lastRequestPath);
                return time;
            }
            catch (IOException iex)
            {
                // iex message will be “cannot create directory at <path>, because same name file exists”
                string msg = rootMsg + iex.Message;
                throw new FileRelatedException(msg, iex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (ArgumentException aex)
            {
                // aex message will be “path is not of a legal form”
                string msg = rootMsg + lastRequestPath + "\n" + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (NotSupportedException nsex)
            {
                string msg = rootMsg + lastRequestPath + "\n" + nsex.Message;

            }
            catch (SecurityException sex)
            {
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public static bool CheckLastReqest()
        {
            if (File.Exists(lastRequestPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Dictionary<int, FileInfo> GetLocalDataDict(DirectoryInfo testDir)
        {
            Dictionary<int, FileInfo> dict = new Dictionary<int, FileInfo>();
            List<DirectoryInfo> rawDataDirs = new List<DirectoryInfo>();
            EcgRawData rawDataJson;
            FileInfo[] rawDataJsons;
            FileInfo[] datas;
            List<Exception> exls = new List<Exception>();
            try
            {
                rawDataDirs = testDir.GetDirectories().ToList();
            }
            catch (DirectoryNotFoundException dnfex)
            {
                // dnfex message will be “Could not find a part of path <path>”
                string msg = rootMsg + dnfex.Message;
                throw new FileRelatedException(msg, dnfex);
            }
            catch (SecurityException sex)
            {
                // The caller does not have the required permission.
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (DirectoryInfo rawDataDir in rawDataDirs)
            {
                try
                {
                    rawDataJsons = rawDataDir.GetFiles(dataInfoName);
                    datas = rawDataDir.GetFiles(dataPattern);
                    if (rawDataJsons != null && datas != null)
                    {
                        foreach (FileInfo jfi in rawDataJsons)
                        {
                            using (StreamReader file = jfi.OpenText())
                            {
                                string json = file.ReadToEnd();
                                rawDataJson = JsonConvert.DeserializeObject<EcgRawData>(json);
                            }
                            dict.Add(rawDataJson.EcgRawDataId, datas[0]);
                        }
                    }
                }
                catch (ArgumentException aex)
                {
                    string msg = rootMsg + aex.Message;
                    exls.Add(new FileRelatedException(msg, aex));
                }
                catch (IOException iex)
                {
                    string msg = rootMsg + iex.Message;
                    exls.Add(new FileRelatedException(msg, iex));
                }
                catch (SecurityException sex)
                {
                    // The caller does not have the required permission.
                    string msg = rootMsg + sex.Message;
                    exls.Add(new FileRelatedException(msg, sex));
                }
                catch (UnauthorizedAccessException uaex)
                {
                    // uaex message will be “Access to path <path> is denied”
                    string msg = rootMsg + uaex.Message;
                    exls.Add(new FileRelatedException(msg, uaex));

                }
                catch (OutOfMemoryException omex)
                {
                    string msg = rootMsg + omex.Message;
                    exls.Add(new FileRelatedException(msg, omex));
                }
                catch (JsonException jex)
                {
                    string msg = rootMsg + jex.Message;
                    exls.Add(new FileRelatedException(msg, jex));
                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                throw new AggregateException(exls);
            }
            return dict;
        }
        public static List<string> GetLocalDataPathLs(DirectoryInfo testDir)
        {
            List<string> pathLs = new List<string>();
            FileInfo[] datas;
            List<DirectoryInfo> rawDataDirs = new List<DirectoryInfo>();
            List<Exception> exls = new List<Exception>();
            try
            {
                rawDataDirs = testDir.GetDirectories().ToList();
            }
            catch (DirectoryNotFoundException dnfex)
            {
                // dnfex message will be “Could not find a part of path <path>”
                string msg = rootMsg + dnfex.Message;
	            throw new FileRelatedException(msg, dnfex);
            }
            catch (SecurityException sex)
            {
                // The caller does not have the required permission.
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (DirectoryInfo rawDataDir in rawDataDirs)
            {
                try
                {
                    datas = rawDataDir.GetFiles(dataPattern);
                    if (datas != null)
                    {
                        pathLs.Add(rawDataDir.FullName + dataName);
                    }
                }
                catch (ArgumentException aex)
                {
                    string msg = rootMsg + aex.Message;
                    exls.Add(new FileRelatedException(msg, aex));
                }
                catch (DirectoryNotFoundException dnfex)
                {
                    // dnfex message will be “Could not find a part of path <path>”
                    string msg = rootMsg + dnfex.Message;
                    exls.Add(new FileRelatedException(msg, dnfex));
                }
                catch (SecurityException sex)
                {
                    // The caller does not have the required permission.
                    string msg = rootMsg + sex.Message;
                    exls.Add(new FileRelatedException(msg, sex));
                }
                catch (UnauthorizedAccessException uaex)
                {
                    // uaex message will be “Access to path <path> is denied”
                    string msg = rootMsg + uaex.Message;
                    exls.Add(new FileRelatedException(msg, uaex));

                }
                catch (Exception ex)
                {
                    exls.Add(ex);
                }
            }
            if (exls.Count > 0)
            {
                throw new AggregateException(exls);
            }
            return pathLs;
        }
        public static List<DirectoryInfo> GetDirectoryInfos()
        {
            List<DirectoryInfo> testDirs = new List<DirectoryInfo>();
            try
            {
                DirectoryInfo root = new DirectoryInfo(folderName);
                testDirs = root.GetDirectories().ToList();
            }
            catch (ArgumentException aex)
            {
                string msg = rootMsg + aex.Message;
                throw new FileRelatedException(msg, aex);
            }
            catch (DirectoryNotFoundException dnfex)
            {
                // dnfex message will be “Could not find a part of path <path>”
                string msg = rootMsg + dnfex.Message;
                throw new FileRelatedException(msg, dnfex);
            }
            catch (SecurityException sex)
            {
                // The caller does not have the required permission.
                string msg = rootMsg + sex.Message;
                throw new FileRelatedException(msg, sex);
            }
            catch (UnauthorizedAccessException uaex)
            {
                // uaex message will be “Access to path <path> is denied”
                string msg = rootMsg + uaex.Message;
                throw new FileRelatedException(msg, uaex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return testDirs;
        }
        public static bool CheckIsne(DirectoryInfo dir)
        {
            return File.Exists(dir.FullName + ishneName);
        }
        public static bool CheckPatient(DirectoryInfo dir)
        {
            return File.Exists(dir.FullName + patientInfoName);
        }
    }
}
