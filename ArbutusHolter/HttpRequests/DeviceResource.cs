using System;
using System.Threading.Tasks;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class DeviceResource
    {
        RestModel<Device> restModel;
        RestModel<ResultJson> jsonRestMod;
        private Requests<Device> requests = new Requests<Device>();
        private Requests<ResultJson> jsonRequest = new Requests<ResultJson>();
        public async Task<RestModel<Device>> GetAvailableDevices(Client client, DateTime pickup, DateTime returnT, string loc)
        {
            restModel = await requests.GetAll("devices?pickupDate=" + pickup.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                        + "-0000&deviceReturnDate=" + returnT.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                        + "-0000&deviceLocation=" + loc, client);
            return restModel;
        }
        public async Task<RestModel<Device>> GetAllDevice(Client client)
        {
            restModel = await requests.GetAll("Alldevice", client);
            return restModel;
        }

        public async Task<RestModel<ResultJson>> ReturnPhoneAndDevice(Client client, int deviceId)
        {
            jsonRestMod = await jsonRequest.Post("return-status/" + deviceId, null, client);
            return jsonRestMod;
        }

    }
}
