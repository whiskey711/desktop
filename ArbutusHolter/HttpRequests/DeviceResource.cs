using System;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class DeviceResource
    {
        RestModel<Device> restModel;
        private Requests<Device> requests = new Requests<Device>();
        public RestModel<Device> GetAvailableDevices(Client client, DateTime pickup, DateTime returnT, string loc)
        {
            restModel = requests.GetAll("test/devices?pickupDate=" + pickup.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                        + "-0000&deviceReturnDate=" + returnT.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") 
                                        + "-0000&deviceLocation=" + loc, client);
            return restModel;
        }
        public RestModel<Device> GetAllDevice(Client client)
        {
            restModel = requests.GetAll("test/Alldevice", client);
            return restModel;
        }
    }
}
