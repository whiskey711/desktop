using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    public class Requests<T>
    {
        private RestModel<T> restModel;
        private ErrorInfo errorInfo;
        private readonly string rootUrl = "http://ecg.uvic.ca:8080/v1/test/";
        public RestModel<T> Put(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = client.HttpClient.PutAsync(rootUrl + url, content).Result;
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
        public RestModel<T> Post(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = client.HttpClient.PostAsync(rootUrl + url, content).Result;
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
        public RestModel<T> GetAll(string url, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = client.HttpClient.GetAsync(rootUrl + url).Result;
            string testJson = client.Result.Content.ReadAsStringAsync().Result;
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
    }
}
