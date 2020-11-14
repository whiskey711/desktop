﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class PublicResources
    {
        RestModel<ResultJson> restModel;
        RestModel<Nurse> nurseModel;
        private Requests<ResultJson> requests = new Requests<ResultJson>();
        private Requests<Nurse> nurseRequests = new Requests<Nurse>();
        public async Task<string> ForgetPassword(string email, Client client)
        {
            StringContent emailContent = new StringContent(email);
            restModel = await requests.Post("public/forgetPassword", emailContent, client);
            return restModel.ErrorMessage;
        }
        public async Task<RestModel<Nurse>> VeriFycode(string code, Client client)
        {
            StringContent Verificationcode = new StringContent(code);
            nurseModel = await nurseRequests.Post("public/verify", Verificationcode, client);
            return nurseModel;
        }
        public async Task<string> ResetPassword(Nurse updatednurse, Client client)
        {
            string json = JsonConvert.SerializeObject(updatednurse);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel = await requests.Post("public/password-management", content, client);
            return restModel.ErrorMessage;
        }
        public async Task<string> Registration(string email, Client client)
        {
            StringContent emailContent = new StringContent(email);
            restModel = await requests.Post("public/registerEmail", emailContent, client);
            return restModel.ErrorMessage;
        }
    }
}
