using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class DeviceResource
    {
        RestModel<Device> restModel;
        RestModel<ResultJson> jsonRestMod;
        private Requests<Device> requests = new Requests<Device>();
        private Requests<ResultJson> jsonRequest = new Requests<ResultJson>();
        HttpContent content;
        public async Task<RestModel<Device>> GetAvailableDevices(Client client, DateTime pickup, DateTime returnT, string loc)
        {
            restModel = await requests.GetAll("devices?pickupDate=" + pickup.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                        + "-0000&deviceReturnDate=" + returnT.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                        + "-0000&deviceLocation=" + loc, client);
            return restModel;
        }
        public async Task<RestModel<Device>> GetAllDevice(Client client)
        {
            restModel = await requests.GetAll("Alldevice", client);
            return restModel;
        }

        public async Task<RestModel<ResultJson>> ReturnPhoneAndDevice(Client client, Appointment theAppoint)
        {
            string json = JsonConvert.SerializeObject(theAppoint, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            jsonRestMod = await jsonRequest.Put("return-status/" + theAppoint.Device.DeviceId, content, client);
            return jsonRestMod;
        }

    }
}
