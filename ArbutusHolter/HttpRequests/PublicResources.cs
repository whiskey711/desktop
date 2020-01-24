using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class PublicResources
    {
        RestModel<ResultJson> restModel;
        RestModel<Nurse> nurseModel;
        private Requests<ResultJson> requests = new Requests<ResultJson>();
        private Requests<Nurse> nurseRequests = new Requests<Nurse>();
        public RestModel<ResultJson> ForgetPassword(String email, Client client)
        {
            StringContent emailContent = new StringContent(email);
            restModel = requests.Post("public/forgetPassword", emailContent, client);
            return restModel;
        }
        public RestModel<Nurse> VeriFycode(String code, Client client)
        {
            StringContent Verificationcode = new StringContent(code);
            nurseModel = nurseRequests.Post("public/verify", Verificationcode, client);
            return nurseModel;
        }
        public RestModel<ResultJson> ResetPassword(Nurse updatednurse, Client client)
        {
            string json = JsonConvert.SerializeObject(updatednurse);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel = requests.Post("public/password-management", content, client);
            return restModel;
        }
        public RestModel<ResultJson> Registration(string email, Client client)
        {
            StringContent emailContent = new StringContent(email);
            restModel = requests.Post("public/registerEmail", emailContent, client);
            return restModel;
        }
    }
}
