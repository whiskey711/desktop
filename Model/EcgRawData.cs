using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace Uvic_Ecg_Model
{
    public class EcgRawData
    {
        [JsonProperty] private int ecgRawDataId;
        [JsonProperty] private int ecgTestId;
        [JsonProperty] private string receivedTime;
        [JsonProperty] private string ecgRawData;
        [JsonProperty] private bool deleted;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string startTime;
        [JsonProperty] private string endTime;
        [JsonProperty] private bool statusFlag;
        [JsonProperty] private long size;
        public EcgRawData(int ecgRawDataId, 
                          int ecgTestId, 
                          string receivedTime, 
                          string rawData,
                          bool deleted, 
                          int clinicId, 
                          string startTime, 
                          string endTime, 
                          bool statusFlag)
        {
            this.EcgRawDataId = ecgRawDataId;
            this.EcgTestId = ecgTestId;
            this.ReceivedTime = receivedTime;
            this.ecgRawData = rawData;
            this.Deleted = deleted;
            this.ClinicId = clinicId;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.StatusFlag = statusFlag;
        }
        public int EcgRawDataId { get => ecgRawDataId; set => ecgRawDataId = value; }
        public int EcgTestId { get => ecgTestId; set => ecgTestId = value; }
        public string ReceivedTime { get => receivedTime; set => receivedTime = value; }
        public string RawData { get => ecgRawData; set => ecgRawData = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }
        public bool StatusFlag { get => statusFlag; set => statusFlag = value; }
        public long Size { get => size; }
    }
}
