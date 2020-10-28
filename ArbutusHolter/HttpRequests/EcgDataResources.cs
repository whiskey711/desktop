using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class EcgDataResources
    {
        RestModel<EcgRawData> restModel;
        RestModel<ResultJson> restModel2;
        RestModel<ResultJson> testRestMod;
        RestModel<EcgTest> ecgTestRestMod;
        private Requests<EcgRawData> requests = new Requests<EcgRawData>();
        private Requests<ResultJson> requests2 = new Requests<ResultJson>();
        private Requests<ResultJson> testReq = new Requests<ResultJson>();
        private Requests<EcgTest> ecgTestReq = new Requests<EcgTest>();
        HttpContent content;
        public RestModel<EcgRawData> GetEcgData(Client client, String status, int pid, int tId)
        {
            string url = "patient/" + pid + "/" + tId + "/ecg-raw-data/" + status;
            restModel = requests.GetAll(url, client);
            return restModel;
        }
        public RestModel<ResultJson> SetHookup(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = requests2.Put("patient/ecg-test/"+tId+"/start-hookup/"+dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> Terminated(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = requests2.Put("patient/ecg-test/" + tId + "/stop/" + dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> SetRecord(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent dummy = new StringContent("");
            restModel2 = requests2.Put("patient/ecg-test/" + tId + "/start-record/" + dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> CreateEcgtest(Client client, EcgTest test)
        {
            string json = JsonConvert.SerializeObject(test, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
            content = new StringContent(json, Encoding.UTF8, "application/json");
            testRestMod = testReq.Post("patient/ecg-tests", content, client);
            return testRestMod;
        }
        public RestModel<ResultJson> UpdateEcgTest(Client client, EcgTest upEcgTest)
        {
            string json = JsonConvert.SerializeObject(upEcgTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            testRestMod = testReq.Put("patient/ecg-tests", content, client);
            return testRestMod;
        }
        public RestModel<EcgTest> GetRunningTest(Client client)
        {
            ecgTestRestMod = ecgTestReq.GetAll("patient/running-tests", client);
            return ecgTestRestMod;
        }
        public RestModel<EcgTest> GetFinishedTest(Client client, DateTime start, DateTime end)
        {
            ecgTestRestMod = ecgTestReq.GetAll("patient/ecg-test?period-start-time=" + start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                               + "-0000&period-end-time=" + end.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000", client);
            return ecgTestRestMod;
        }
        public RestModel<EcgRawData> GetRawDataLs(Client client, int testid)
        {
            restModel = requests.GetAll("patient/ecg-test/" + testid, client);
            return restModel;
        }
        public RestModel<EcgRawData> GetData(Client client, int dataid)
        {
            restModel = requests.GetAll("patient/ecg-test/ecg-raw-data/" + dataid, client);
            return restModel;
        }
        public EcgTest GetTestById(int ecgTestId, int patientId, Client client)
        {
            ecgTestRestMod = ecgTestReq.GetAll("patient/" + patientId + "/ecg-test/" + ecgTestId, client);
            return ecgTestRestMod.Entity.Model;
        }
    }
}
