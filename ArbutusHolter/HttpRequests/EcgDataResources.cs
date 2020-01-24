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
        private Requests<EcgRawData> requests = new Requests<EcgRawData>();
        private Requests<ResultJson> requests2 = new Requests<ResultJson>();
        private Requests<ResultJson> testReq = new Requests<ResultJson>();
        HttpContent content;
        public RestModel<EcgRawData> GetEcgData(Client client, String status, int pid, int tId)
        {
            String url = "test/patient/" + pid + "/" + tId + "/ecg-raw-data/" + status;
            restModel = requests.GetAll(url, client);
            return restModel;
        }
        public RestModel<ResultJson> SetHookup(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = requests2.Put("test/patient/ecg-test/"+tId+"/start-hookup/"+dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> Terminated(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel2 = requests2.Put("test/patient/ecg-test/" + tId + "/stop/" + dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> SetRecord(Client client, int tId, int dId)
        {
            EcgTest dummyTest = new EcgTest();
            string json = JsonConvert.SerializeObject(dummyTest, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent dummy = new StringContent("");
            restModel2 = requests2.Put("test/patient/ecg-test/" + tId + "/start-record/" + dId, content, client);
            return restModel2;
        }
        public RestModel<ResultJson> CreateEcgtest(Client client, EcgTest test)
        {
            string json = JsonConvert.SerializeObject(test, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
            content = new StringContent(json, Encoding.UTF8, "application/json");
            testRestMod = testReq.Post("test/patient/ecg-tests", content, client);
            return testRestMod;
        }
    }
}
