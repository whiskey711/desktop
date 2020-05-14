using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Uvic_Ecg_Model
{
    public class Appointment
    {
        [JsonProperty] private int appointmentRecordId;
        [JsonProperty] private int nurseId;
        [JsonProperty] private int patientId;
        [JsonProperty] private int deviceId;
        [JsonProperty] private DateTime appointmentStartTime;
        [JsonProperty] private DateTime appointmentEndTime;
        [JsonProperty] private DateTime? reservationTime = null;
        [JsonProperty] private DateTime? pickupDate = null;
        [JsonProperty] private DateTime? deviceReturnDate = null;
        [JsonProperty] private string deviceLocation;
        [JsonProperty] private string instruction;
        [JsonProperty] private bool deleted;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string firstName;
        [JsonProperty] private string lastName;
        [JsonProperty] private int? ecgTestId = null; 
        public Appointment(int nId,
                           int pId,
                           int dId,
                           DateTime startTime,
                           DateTime endTime,
                           DateTime reserveTime,
                           DateTime devPickupDate,
                           DateTime returnTime,
                           string devLocation,
                           string instruct,
                           bool delete,
                           int cId,
                           string pFirstName,
                           string pLastName,
                           int? ecgTestid)
        {
            NurseId = nId;
            PatientId = pId;
            DeviceId = dId;
            AppointmentStartTime = startTime;
            AppointmentEndTime = endTime;
            ReservationTime = reserveTime;
            PickupDate = devPickupDate;
            DeviceReturnDate = returnTime;
            DeviceLocation = devLocation;
            Instruction = instruct;
            Deleted = delete;
            ClinicId = cId;
            FirstName = pFirstName;
            LastName = pLastName;
            EcgTestId = ecgTestid;
        }
        [JsonConstructor] public Appointment(int appRecId,
                                             int nId,
                                             int pId,
                                             int dId,
                                             DateTime startTime,
                                             DateTime endTime,
                                             DateTime reserveTime,
                                             DateTime devPickupDate,
                                             DateTime returnTime,
                                             string devLocation,
                                             string instruct,
                                             bool delete,
                                             int cId,
                                             string pFirstName,
                                             string pLastName,
                                             int? ecgTestid)
        {
            AppointmentRecordId = appRecId;
            NurseId = nId;
            PatientId = pId;
            DeviceId = dId;
            AppointmentStartTime = startTime;
            AppointmentEndTime = endTime;
            ReservationTime = reserveTime;
            PickupDate = devPickupDate;
            DeviceReturnDate = returnTime;
            DeviceLocation = devLocation;
            Instruction = instruct;
            Deleted = delete;
            ClinicId = cId;
            FirstName = pFirstName;
            LastName = pLastName;
            EcgTestId = ecgTestid;
        }
        public int AppointmentRecordId { get => appointmentRecordId; set => appointmentRecordId = value; }
        public int NurseId { get => nurseId; set => nurseId = value; }
        public int PatientId { get => patientId; set => patientId = value; }
        public int DeviceId { get => deviceId; set => deviceId = value; }
        public DateTime AppointmentStartTime { get => appointmentStartTime; set => appointmentStartTime = value; }
        public DateTime AppointmentEndTime { get => appointmentEndTime; set => appointmentEndTime = value; }
        public DateTime? ReservationTime { get => reservationTime; set => reservationTime = value; }
        public DateTime? PickupDate { get => pickupDate; set => pickupDate = value; }
        public DateTime? DeviceReturnDate { get => deviceReturnDate; set => deviceReturnDate = value; }
        public string DeviceLocation { get => deviceLocation; set => deviceLocation = value; }
        public string Instruction { get => instruction; set => instruction = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int? EcgTestId { get => ecgTestId; set => ecgTestId = value; }
    }
}
