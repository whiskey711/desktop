using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class MailResource
    {
        RestModel<ResultJson> restmodel;
        private Requests<ResultJson> request = new Requests<ResultJson>();
        HttpContent content;
        public async Task<RestModel<ResultJson>> GetTemplate(Client client)
        {
            restmodel = await request.GetAll("mail-template", client);
            return restmodel;
        }
        public async Task<RestModel<ResultJson>> SendMail(Client client, AppointmentMail mail)
        {
            string json = JsonConvert.SerializeObject(mail);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            restmodel = await request.Post("appoint-mail", content, client);
            return restmodel;
        }
    }
}
