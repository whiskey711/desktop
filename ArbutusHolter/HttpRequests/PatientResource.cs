using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class PatientResource
    {
        RestModel<PatientInfo> restModel;
        private Requests<PatientInfo> requests = new Requests<PatientInfo>();
        HttpContent content;
        public async Task<RestModel<PatientInfo>> GetPatient(string lastname, string firstname, string birth, string phn, Client client)
        {
            string subUrl;
            if (string.IsNullOrWhiteSpace(birth))
            {
                subUrl = "patient/information?lastname=" + lastname + "&firstname=" + firstname + "&phn=" + phn;
            }
            else
            {
                subUrl = "patient/information?lastname=" + lastname + "&firstname=" + firstname + "&birthday=" + birth + "&phn=" + phn;
            }
            restModel = await requests.GetAll(subUrl, client);
            return restModel;
        }
        public async Task<string> CreatePatient(PatientInfo newPatient, Client client)
        {
            string json = JsonConvert.SerializeObject(newPatient);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel = await requests.Post("test/patient/information", content, client);
            return restModel.ErrorMessage;
        }
        public async Task<string> UpdatePatient(PatientInfo updatedPatient, Client client)
        {
            string json = JsonConvert.SerializeObject(updatedPatient);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restModel = await requests.Put("patient/information/" + updatedPatient.PatientId, content, client);
            return restModel.ErrorMessage;
        }
        public async Task<RestModel<PatientInfo>> GetPatientById(int pid, Client client)
        {
            restModel = await requests.GetAll("patient/information/" + pid, client);
            return restModel;
        }

    }
}
