using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    public class Requests<T>
    {
        private RestModel<T> restModel;
        private ErrorInfo errorInfo;
        private readonly string rootUrl = "http://ecg.uvic.ca:8080/v1/test/";
        public async Task<RestModel<T>> Put(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.PutAsync(rootUrl + url, content);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
        public async Task<RestModel<T>> Post(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.PostAsync(rootUrl + url, content);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
        public async Task<RestModel<T>> GetAll(string url, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.GetAsync(rootUrl + url);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
        public async Task<RestModel<T>> Delete(string url, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.DeleteAsync(rootUrl + url);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            return restModel;
        }
    }
}
