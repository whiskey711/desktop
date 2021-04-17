using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<RestModel<Nurse>> GetallNurse(Client client)
        {
            restModel = await requests.GetAll("getnurses", client);
            return restModel;
        }
        public async Task<RestModel<Appointment>> GetAppointments(Client client, DateTime start, DateTime end, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                appointRestMod = await appointRequest.GetAll("appointment-records?start-time=" + start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                                           + "-0000&end-time=" + end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000", client);
            }
            else
            {
                appointRestMod = await appointRequest.GetAll("appointment-records?start-time=" + start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                                                           + "-0000&end-time=" + end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "-0000" +
                                                           "&location=" + location, client);
            }
            return appointRestMod;
        }
        public async Task<string> CreateAppointment(Appointment newAppoint, Client client)
        {
            string json = JsonConvert.SerializeObject(newAppoint, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local,
                                                                                               NullValueHandling = NullValueHandling.Ignore,
                                                                                               DefaultValueHandling = DefaultValueHandling.Ignore});
            content = new StringContent(json, Encoding.UTF8, "application/json");
            appointRestMod = await appointRequest.Post("appointment-records", content, client);
            return appointRestMod.ErrorMessage;
        }
        public async Task<string> UpdateAppointment(Appointment updateAppoint, Client client)
        {
            string json = JsonConvert.SerializeObject(updateAppoint, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local,
                                                                                                  NullValueHandling = NullValueHandling.Ignore,
                                                                                                  DefaultValueHandling = DefaultValueHandling.Ignore});
            content = new StringContent(json, Encoding.UTF8, "application/json");
            appointRestMod = await appointRequest.Put("appointment-record/" + updateAppoint.AppointmentRecordId, content, client);
            return appointRestMod.ErrorMessage;
        }
        public async Task<RestModel<Appointment>> GetPatientAppoint(int patientId, Client client)
        {
            appointRestMod = await appointRequest.GetAll("appointment-records/" + patientId, client);
            return appointRestMod;
        }
        public async Task<string> DeleteAppointment(Appointment deleteAppoint, Client client)
        {
            appointRestMod = await appointRequest.Delete("appointment-record/" + deleteAppoint.AppointmentRecordId, client);
            return appointRestMod.ErrorMessage;
        }
    }
}
