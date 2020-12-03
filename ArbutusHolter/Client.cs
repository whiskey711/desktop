using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public class Client
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage result;
        private HttpContent content;
        private string url = "http://ecg.uvic.ca:8080/v1/test/";
        private string publicUrl = "http://ecg.uvic.ca:8080/v1/";
        private IEnumerable<string> tokenObj;
        private string token;
        private string fullToken;
        private int nurseId;
        public HttpResponseMessage Result { get => result; set => result = value; }
        public HttpClient HttpClient { get => httpClient; set => httpClient = value; }
        public HttpContent Content { get => content; set => content = value; }
        public string Token { get => token; set => token = value; }
        public int NurseId { get => nurseId; }
        public string PublicUrl { get => publicUrl; set => publicUrl = value; }

        public string getUrl()
        {
            return this.url;
        }
        public void seturl(string newUrl)
        {
            this.url = newUrl;
        }
        public async Task<ErrorInfo> Login(string inputUserName, string inputPassword)
        {
            User user = new User
            {
                username = inputUserName,
                password = inputPassword
            };
            string json = JsonConvert.SerializeObject(user);
            Content = new StringContent(json, Encoding.UTF8, "application/json");
            Result = await HttpClient.PostAsync(getUrl() + "login", Content);
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
                    string s = fullerrorMsg.Split(':').Last().Trim('"', ' ', '{', '}');
                    nurseId = int.Parse(s);
                    return ErrorInfo.OK;
                }
                //else return cannot find token
                return ErrorInfo.Failed;
         
            }
            else
            {
                nurseId = -1;
                return ErrorInfo.Failed;
            }
        }
        public async Task<bool> Register(Nurse newNurse)
        {
            string json = JsonConvert.SerializeObject(newNurse);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            result = await httpClient.PostAsync(getUrl() + "nurses", content);
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
