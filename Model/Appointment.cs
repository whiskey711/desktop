using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Uvic_Ecg_Model
{
    public class Appointment
    {
        [JsonProperty] private int appointmentRecordId;
        [JsonProperty] private Nurse nurse;
        [JsonProperty] private PatientInfo patient;
        [JsonProperty] private Device device;
        [JsonProperty] private DateTime appointmentStartTime;
        [JsonProperty] private DateTime appointmentEndTime;
        [JsonProperty] private DateTime? reservationTime = null;
        [JsonProperty] private DateTime pickupDate;
        [JsonProperty] private DateTime deviceReturnDate;
        [JsonProperty] private DateTime? delayDeviceReturnTime = null;
        [JsonProperty] private DateTime? deviceActualReturnTime = null;
        [JsonProperty] private string deviceLocation;
        [JsonProperty] private string instruction;
        [JsonProperty] private EcgTest ecgTest;
        public Appointment(Nurse n,
                           PatientInfo p,
                           Device d,
                           DateTime startTime,
                           DateTime endTime,
                           DateTime reserveTime,
                           DateTime devPickupDate,
                           DateTime returnTime,
                           string devLocation,
                           string instruct,
                           EcgTest ecgTest)
        {
            Nurse = n;
            Patient = p;
            Device = d;
            AppointmentStartTime = startTime;
            AppointmentEndTime = endTime;
            ReservationTime = reserveTime;
            PickupDate = devPickupDate;
            DeviceReturnDate = returnTime;
            DeviceLocation = devLocation;
            Instruction = instruct;
            EcgTest = ecgTest;
        }
        [JsonConstructor]
        public Appointment(int appRecId,
                                             Nurse n,
                                             PatientInfo p,
                                             Device d,
                                             DateTime startTime,
                                             DateTime endTime,
                                             DateTime reserveTime,
                                             DateTime devPickupDate,
                                             DateTime returnTime,
                                             DateTime? devActualReturnTime,
                                             string devLocation,
                                             string instruct,
                                             EcgTest ecgTest)
        {
            AppointmentRecordId = appRecId;
            Nurse = n;
            Patient = p;
            Device = d;
            AppointmentStartTime = startTime;
            AppointmentEndTime = endTime;
            ReservationTime = reserveTime;
            PickupDate = devPickupDate;
            DeviceReturnDate = returnTime;
            DeviceActualReturnTime = devActualReturnTime;
            DeviceLocation = devLocation;
            Instruction = instruct;
            EcgTest = ecgTest;
        }
        public int AppointmentRecordId { get => appointmentRecordId; set => appointmentRecordId = value; }
        public Nurse Nurse { get => nurse; set => nurse = value; }
        public PatientInfo Patient { get => patient; set => patient = value; }
        public Device Device { get => device; set => device = value; }
        public DateTime AppointmentStartTime { get => appointmentStartTime; set => appointmentStartTime = value; }
        public DateTime AppointmentEndTime { get => appointmentEndTime; set => appointmentEndTime = value; }
        public DateTime? ReservationTime { get => reservationTime; set => reservationTime = value; }
        public DateTime PickupDate { get => pickupDate; set => pickupDate = value; }
        public DateTime DeviceReturnDate { get => deviceReturnDate; set => deviceReturnDate = value; }
        public string DeviceLocation { get => deviceLocation; set => deviceLocation = value; }
        public string Instruction { get => instruction; set => instruction = value; }
        public EcgTest EcgTest { get => ecgTest; set => ecgTest = value; }
        public DateTime? DeviceActualReturnTime { get => deviceActualReturnTime; set => deviceActualReturnTime = value; }
        public DateTime? DeferReturnTime { get => delayDeviceReturnTime; set => delayDeviceReturnTime = value; }
    }
}
