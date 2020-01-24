using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class EcgTest
    {
        [JsonProperty] private int ecgTestId;
        [JsonProperty] private DateTime startTime;
        [JsonProperty] private DateTime scheduledEndTime;
        [JsonProperty] private DateTime? actualEndTime = null;
        [JsonProperty] private int patientId;
        [JsonProperty] private int nurseId;
        [JsonProperty] private int phoneId;
        [JsonProperty] private int deviceId;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string comment;
        [JsonProperty] private bool deleted;
        [JsonProperty] private bool hookupStatus;
        [JsonProperty] private bool recordingStatus;
        public int EcgTestId { get => ecgTestId; set => ecgTestId = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime ScheduledEndTime { get => scheduledEndTime; set => scheduledEndTime = value; }
        public DateTime? ActualEndTime { get => actualEndTime; set => actualEndTime = value; }
        public int PatientId { get => patientId; set => patientId = value; }
        public int NurseId { get => nurseId; set => nurseId = value; }
        public int PhoneId { get => phoneId; set => phoneId = value; }
        public int DeviceId { get => deviceId; set => deviceId = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string Comment { get => comment; set => comment = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public bool HookupStatus { get => hookupStatus; set => hookupStatus = value; }
        public bool RecordingStatus { get => recordingStatus; set => recordingStatus = value; }
        public EcgTest(DateTime startTimei,
                       DateTime scheduledEndTimei,
                       DateTime? actualEndTimei,
                       int patientIdi,
                       int nurseIdi,
                       int phoneIdi,
                       int deviceIdi,
                       int clinicIdi,
                       string commenti,
                       bool deletedi,
                       bool hookup,
                       bool record)
        {
            StartTime = startTimei;
            ScheduledEndTime = scheduledEndTimei;
            ActualEndTime = actualEndTimei;
            PatientId = patientIdi;
            NurseId = nurseIdi;
            PhoneId = phoneIdi;
            DeviceId = deviceIdi;
            ClinicId = clinicIdi;
            Comment = commenti;
            Deleted = deletedi;
            HookupStatus = hookup;
            RecordingStatus = record;
        }
        public EcgTest() { }
    }
}
