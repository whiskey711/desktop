using Newtonsoft.Json;
using System;
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
        private int maxHttpExCount = 3;
        public async Task<RestModel<T>> Put(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.PutAsync(client.Url + url, content);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            else if (client.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new TokenExpiredException(ErrorInfo.TokenExpired.ErrorMessage);
            }
            return restModel;
        }
        public async Task<RestModel<T>> PublicPut(string url, HttpContent content, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.PutAsync(client.PublicUrl + url, content);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            else if (client.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new TokenExpiredException(ErrorInfo.TokenExpired.ErrorMessage);
            }
            return restModel;
        }
        // Post() for httpcontent
        public async Task<RestModel<T>> Post(string url, HttpContent content, Client client)
        {
            return await Post(url, content, client, null);
        }
        // Post() for multipartformdatacontent
        public async Task<RestModel<T>> Post(string url, 
                                             HttpContent content, 
                                             Client client, 
                                             MultipartFormDataContent multipartFormDataContent)
        {
            bool httpFailure;
            int exCount = 0;
            do
            {
                try
                {
                    client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                    if (multipartFormDataContent == null)
                    {
                        client.Result = await client.HttpClient.PostAsync(client.Url + url, content);
                    }
                    else
                    {
                        client.Result = await client.HttpClient.PostAsync(client.Url + url, multipartFormDataContent);
                    }
                    httpFailure = false;
                }
                catch (HttpRequestException hrex)
                {
                    exCount++;
                    if (exCount > maxHttpExCount)
                    {
                        throw hrex;
                    }
                    httpFailure = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } while (httpFailure);
            try
            {
                restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
                if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
                }
                else if (client.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new TokenExpiredException(ErrorInfo.TokenExpired.ErrorMessage);
                }
                return restModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RestModel<T>> PublicPost(string url, HttpContent content, Client client)
        {
            bool httpFailure;
            int exCount = 0;
            do
            {
                try
                {
                    client.Result = await client.HttpClient.PostAsync(client.PublicUrl + url, content);
                    httpFailure = false;
                }
                catch (HttpRequestException hrex)
                {
                    exCount++;
                    if (exCount > maxHttpExCount)
                    {
                        throw hrex;
                    }
                    httpFailure = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } while (httpFailure);
            try
            {
                restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
                if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
                }
                return restModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RestModel<T>> GetAll(string url, Client client)
        {
            bool httpFailure;
            int exCount = 0;
            do
            {
                try
                {
                    client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                    client.Result = await client.HttpClient.GetAsync(client.Url + url);
                    httpFailure = false;
                }
                catch (InvalidOperationException invoex)
                {
                    throw invoex;
                }
                catch (HttpRequestException hrex)
                {
                    exCount++;
                    if (exCount > maxHttpExCount)
                    {
                        throw hrex;
                    }
                    httpFailure = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } while (httpFailure);
            try
            {
                Task<string> str = client.Result.Content.ReadAsStringAsync();
                restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
                if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
                }
                else if (client.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new TokenExpiredException(ErrorInfo.TokenExpired.ErrorMessage);
                }
                return restModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RestModel<T>> Delete(string url, Client client)
        {
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            client.Result = await client.HttpClient.DeleteAsync(client.Url + url);
            restModel = JsonConvert.DeserializeObject<RestModel<T>>(client.Result.Content.ReadAsStringAsync().Result);
            if (client.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                restModel.ErrorMessage = ErrorInfo.OK.ErrorMessage;
            }
            else if (client.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new TokenExpiredException(ErrorInfo.TokenExpired.ErrorMessage);
            }
            return restModel;
        }
    }
}
