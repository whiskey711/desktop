using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class MailResource
    {
        RestModel<ResultJson> restmodel;
        private Requests<ResultJson> request = new Requests<ResultJson>();
        HttpContent content;
        public RestModel<ResultJson> GetTemplate(Client client)
        {
            restmodel = request.GetAll("mail-template", client);
            return restmodel;
        }
        public RestModel<ResultJson> SendMail(Client client, AppointmentMail mail)
        {
            string json = JsonConvert.SerializeObject(mail);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restmodel = request.Post("appoint-mail", content, client);
            return restmodel;
        }
    }
}
