using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class Nurse
    {
        [JsonProperty] private int nurseId;
        [JsonProperty] private string nurseLastName;
        [JsonProperty] private string nurseMidName;
        [JsonProperty] private string nurseFirstName;
        [JsonProperty] private string nursePhoneNumber;
        [JsonProperty] private string nurseEmail;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string password;
        [JsonProperty] private bool deleted;
        public Nurse(string nLastname,
                     string nMidname,
                     string nFirstName,
                     string nPhoneNum,
                     string nEmail,
                     int cId,
                     string pass,
                     bool delete)
        {
            nurseLastName = nLastname;
            nurseMidName = nMidname;
            nurseFirstName = nFirstName;
            nursePhoneNumber = nPhoneNum;
            nurseEmail = nEmail;
            clinicId = cId;
            password = pass;
            deleted = delete;
        }
        public Nurse(string mail)
        {
            nurseEmail = mail;
        }
        
        public Nurse(int nid,
                     string nLastname,
                     string nMidname,
                     string nFirstName,
                     string nPhoneNum,
                     string nEmail
                     )
        {
            NurseId = nid;
            nurseLastName = nLastname;
            nurseMidName = nMidname;
            nurseFirstName = nFirstName;
            nursePhoneNumber = nPhoneNum;
            nurseEmail = nEmail;
        }
        [JsonConstructor]
        public Nurse(int nid)
        {
            NurseId = nid;
        }
        public int NurseId { get => nurseId; set => nurseId = value; }
        public string NurseLastName { get => nurseLastName; set => nurseLastName = value; }
        public string NurseMidName { get => nurseMidName; set => nurseMidName = value; }
        public string NurseFirstName { get => nurseFirstName; set => nurseFirstName = value; }
        public string NursePhoneNumber { get => nursePhoneNumber; set => nursePhoneNumber = value; }
        public string NurseEmail { get => nurseEmail; set => nurseEmail = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string Password { get => password; set => password = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
