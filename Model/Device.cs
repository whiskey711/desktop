using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace Uvic_Ecg_Model
{
    public class Device
    {
        [JsonProperty] private int deviceId;
        [JsonProperty] private int phoneId;
        [JsonProperty] private string deviceName;
        [JsonProperty] private bool occupied;
        [JsonProperty] private bool deleted;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string deviceLocation;
        [JsonConstructor]
        public Device(int deviceIdi,
                      int phoneIdi,
                      string deviceNamei,
                      bool occupiedi,
                      bool deletedi,
                      int clinicIdi)
        {
            DeviceId = deviceIdi;
            PhoneId = phoneIdi;
            DeviceName = deviceNamei;
            Occupied = occupiedi;
            Deleted = deletedi;
            ClinicId = clinicIdi;
        }
        public Device(int did)
        {
            DeviceId = did;
        }
        public int DeviceId { get => deviceId; set => deviceId = value; }
        public int PhoneId { get => phoneId; set => phoneId = value; }
        public string DeviceName { get => deviceName; set => deviceName = value; }
        public bool Occupied { get => occupied; set => occupied = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string DeviceLocation { get => deviceLocation; set => deviceLocation = value; }
    }
}
