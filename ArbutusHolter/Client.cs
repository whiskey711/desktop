using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public class Client
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage result;
        private HttpContent content;
        private string url = "http://ecg.uvic.ca:8080/v1/test/";
        private IEnumerable<string> tokenObj;
        private string token;
        private string fullToken;
        private ErrorInfo errorInfo;
        public HttpResponseMessage Result { get => result; set => result = value; }
        public HttpClient HttpClient { get => httpClient; set => httpClient = value; }
        public HttpContent Content { get => content; set => content = value; }
        public ErrorInfo ErrorInfo { get => errorInfo; set => errorInfo = value; }
        public string Token { get => token; set => token = value; }
        public string getUrl()
        {
            return this.url;
        }
        public void seturl(string newUrl)
        {
            this.url = newUrl;
        }
        public ErrorInfo Login(string inputUserName, string inputPassword)
        {
            User user = new User
            {
                username = inputUserName,
                password = inputPassword
            };
            string json = JsonConvert.SerializeObject(user);
            Content = new StringContent(json, Encoding.UTF8, "application/json");
            Result = HttpClient.PostAsync(getUrl() + "login", Content).Result;
            string fullerrorMsg = Result.Content.ReadAsStringAsync().Result;
            if (Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (Result.Headers.TryGetValues("Authorization", out tokenObj))
                {
                    fullToken = tokenObj.First();
                    //Get the index after 'Berear'
                    int beginIndex = fullToken.IndexOf('r', fullToken.IndexOf('r') + 1);
                    //store the token in the 'token' string
                    Token = fullToken.Substring(beginIndex + 1);
                    errorInfo = ErrorInfo.OK;
                }
                //else return cannot find token
            }
            else
            {
                errorInfo = ErrorInfo.Failed;
            }
            return errorInfo;
        }
        public bool Register(Nurse newNurse)
        {
            string json = JsonConvert.SerializeObject(newNurse);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            result = httpClient.PostAsync(getUrl() + "nurses", content).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
    public class User
    {
        public string username;
        public string password;
    }
}
