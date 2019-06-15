using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArbutusHolter.Model;

namespace ArbutusHolter
{
    public class Client
    {
        private HttpClient httpClient = new HttpClient();
        private HttpResponseMessage result;
        private HttpContent content;
        private string url;
        private IEnumerable<string> tokenObj;
        private string token;
        private string fullToken;
        private string errorMsg;

        public string getUrl()
        {
            return this.url;
        }
        public void seturl(string newUrl)
        {
            this.url = newUrl;
        }
        public string Login(string inputUserName, string inputPassword)
        {
            User user = new User
            {
                username = inputUserName,
                password = inputPassword
            };
            
            string json = JsonConvert.SerializeObject(user);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            seturl("http://ecg.uvic.ca:443/v1/clinic/login");
            result = httpClient.PostAsync(getUrl(), content).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Headers.TryGetValues("Authorization", out tokenObj))
                {
                    fullToken = tokenObj.First();
                    //Get the index after 'Berear'
                    int beginIndex = fullToken.IndexOf('r', fullToken.IndexOf('r') + 1);
                    //store the token in the 'token' string
                    token = fullToken.Substring(beginIndex + 1);
                    errorMsg = "ok";
                }
                //else return cannot find token
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string fullerrorMsg = result.Content.ReadAsStringAsync().Result;
                errorMsg = fullerrorMsg.Substring(fullerrorMsg.IndexOf('"') + 4);
            }
            return errorMsg;
        }

        public string Register(Nurse newNurse)
        {
            string json = JsonConvert.SerializeObject(newNurse);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            seturl("http://ecg.uvic.ca:443/v1/test/nurses");
            result = httpClient.PostAsync(getUrl(), content).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                errorMsg = "ok";
            }
            else
            {
                string fullerrorMsg = result.Content.ReadAsStringAsync().Result;
                errorMsg = fullerrorMsg.Substring(fullerrorMsg.IndexOf('"') + 4);
            }
            return errorMsg;
        }
        
    }

    public class User
    {
        public string username;
        public string password;
    }

}
