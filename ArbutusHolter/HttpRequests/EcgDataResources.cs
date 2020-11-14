using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<RestModel<EcgRawData>> GetEcgData(Client client, string status, int pid, int tId)
        {
            string url = "patient/" + pid + "/" + tId + "/ecg-raw-data/" + status;
            restModel = await requests.GetAll(url, client);
            return restModel;
        }
        public async Task<RestModel<ResultJson>> SetHookup(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = await requests2.Put("patient/ecg-test/"+tId+"/start-hookup/"+dId, content, client);
            return restModel2;
        }
        public async Task<RestModel<ResultJson>> Terminated(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = await requests2.Put("patient/ecg-test/" + tId + "/stop/" + dId, content, client);
            return restModel2;
        }
        public async Task<RestModel<ResultJson>> SetRecord(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent dummy = new StringContent("");
            restModel2 = await requests2.Put("patient/ecg-test/" + tId + "/start-record/" + dId, content, client);
            return restModel2;
        }
        public async Task<RestModel<ResultJson>> CreateEcgtest(Client client, EcgTest test)
        {
            string json = JsonConvert.SerializeObject(test, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
            content = new StringContent(json, Encoding.UTF8, "application/json");
            testRestMod = await testReq.Post("patient/ecg-tests", content, client);
            return testRestMod;
        }
        public async Task<RestModel<ResultJson>> UpdateEcgTest(Client client, EcgTest upEcgTest)
        {
            string json = JsonConvert.SerializeObject(upEcgTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            testRestMod = await testReq.Put("patient/ecg-tests", content, client);
            return testRestMod;
        }
        public async Task<RestModel<EcgTest>> GetRunningTest(Client client)
        {
            ecgTestRestMod = await ecgTestReq.GetAll("patient/running-tests", client);
            return ecgTestRestMod;
        }
        public async Task<RestModel<EcgTest>> GetFinishedTest(Client client, DateTime start, DateTime end)
        {
            ecgTestRestMod = await ecgTestReq.GetAll("patient/ecg-test?period-start-time=" + start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                               + "-0000&period-end-time=" + end.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000", client);
            return ecgTestRestMod;
        }
        public async Task<RestModel<EcgRawData>> GetRawDataLs(Client client, int testid)
        {
            restModel = await requests.GetAll("patient/ecg-test/" + testid, client);
            return restModel;
        }
        public async Task<RestModel<EcgRawData>> GetData(Client client, int dataid)
        {
            restModel = await requests.GetAll("patient/ecg-test/ecg-raw-data/" + dataid, client);
            return restModel;
        }
        public async Task<RestModel<EcgTest>> GetTestById(int ecgTestId, int patientId, Client client)
        {
            ecgTestRestMod = await ecgTestReq.GetAll("patient/" + patientId + "/ecg-test/" + ecgTestId, client);
            return ecgTestRestMod;
        }
    }
}
