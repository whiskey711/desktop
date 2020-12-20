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
        [JsonProperty] private int? appointmentId = null;
        [JsonProperty] private int clinicId;
        [JsonProperty] private int patientId;
        [JsonProperty] private int nurseId;
        [JsonProperty] private int deviceId;
        [JsonProperty] private string comment;

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime ScheduledEndTime { get => scheduledEndTime; set => scheduledEndTime = value; }
        public DateTime? ActualEndTime { get => actualEndTime; set => actualEndTime = value; }
        public int PatientId { get => patientId; set => patientId = value; }
        public int NurseId { get => nurseId; set => nurseId = value; }
        public int DeviceId { get => deviceId; set => deviceId = value; }
        public string Comment { get => comment; set => comment = value; }
        public int EcgTestId { get => ecgTestId; set => ecgTestId = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public int? AppointmentId { get => appointmentId; set => appointmentId = value; }

        public EcgTest(DateTime startTimei,
                       DateTime scheduledEndTimei,
                       DateTime? actualEndTimei,
                       int? appointId,
                       int patientIdi,
                       int nurseIdi,
                       int deviceIdi,
                       string commenti,
                       int clinici)
        {
            StartTime = startTimei;
            ScheduledEndTime = scheduledEndTimei;
            ActualEndTime = actualEndTimei;
            AppointmentId = appointId;
            PatientId = patientIdi;
            NurseId = nurseIdi;
            DeviceId = deviceIdi;
            Comment = commenti;
            ClinicId = clinici;
        }

        public EcgTest() { }

        [JsonConstructor]
        public EcgTest(int eid)
        {
            EcgTestId = eid;
        }
        public EcgTest(int ecgtestid,
                       DateTime startTimei,
                       DateTime scheduledEndTimei,
                       DateTime? actualEndTimei,
                       string commenti,
                       int? appointId,
                       int clinici)
        {
            EcgTestId = ecgtestid;
            StartTime = startTimei;
            ScheduledEndTime = scheduledEndTimei;
            ActualEndTime = actualEndTimei;
            Comment = commenti;
            ClinicId = clinici;
        }
        public EcgTest(int testId,
                       DateTime start,
                       DateTime scheduledEnd,
                       int clinicI)
        {
            EcgTestId = testId;
            StartTime = start;
            ScheduledEndTime = scheduledEnd;
            ClinicId = clinicI;
        }
    }
}
