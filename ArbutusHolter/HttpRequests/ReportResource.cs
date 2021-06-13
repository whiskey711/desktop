using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Uvic_Ecg_Model;

namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class ReportResource
    {
        RestModel<ResultJson> restModel;
        private Requests<ResultJson> request = new Requests<ResultJson>();
        MultipartFormDataContent content = new MultipartFormDataContent();
        HttpContent fileContent;
        HttpContent stringContent;
        string json;
        public async Task<RestModel<ResultJson>> UploadReport(Client client, Reports report, Stream fileStream)
        {
            fileContent = new StreamContent(fileStream);
            json = JsonConvert.SerializeObject(report, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            stringContent = new StringContent(json);
            content.Add(fileContent, "file", "file.pdf");
            content.Add(stringContent, "report");
            restModel = await request.Post("report", null, client, content);
            return restModel;
        }
    }
}