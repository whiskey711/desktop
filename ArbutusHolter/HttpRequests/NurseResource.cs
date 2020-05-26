using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter.HttpRequests
{
    class NurseResource
    {
        RestModel<Nurse> restModel;
        private Requests<Nurse> requests = new Requests<Nurse>();
        HttpContent content;
        RestModel<Appointment> appointRestMod;
        private Requests<Appointment> appointRequest = new Requests<Appointment>();
        public RestModel<Nurse> GetallNurse(Client client)
        {
            restModel = requests.GetAll("test/getnurses", client);
            return restModel;
        }
        public RestModel<Appointment> GetAppointments(Client client, DateTime start, DateTime end, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                appointRestMod = appointRequest.GetAll("test/appointment-records?start-time=" + start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                                           + "-0000&end-time=" + end.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000", client);
            }
            else
            {
                appointRestMod = appointRequest.GetAll("test/appointment-records?start-time=" + start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                                           + "-0000&end-time=" + end.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000" +
                                                           "&location=" + location, client);
            }
            return appointRestMod;
        }
        public String CreateAppointment(Appointment newAppoint, Client client)
        {
            string json = JsonConvert.SerializeObject(newAppoint, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            appointRestMod = appointRequest.Post("test/appointment-records", content, client);
            return appointRestMod.ErrorMessage;
        }
        public String UpdateAppointment(Appointment updateAppoint, Client client)
        {
            string json = JsonConvert.SerializeObject(updateAppoint, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            content = new StringContent(json, Encoding.UTF8, "application/json");
            appointRestMod = appointRequest.Put("test/appointment-record/" + updateAppoint.AppointmentRecordId, content, client);
            return appointRestMod.ErrorMessage;
        }
        public RestModel<Appointment> GetPatientAppoint(int patientId, Client client)
        {
            appointRestMod = appointRequest.GetAll("test/appointment-records/" + patientId, client);
            return appointRestMod;
        }
    }
}
