using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Uvic_Ecg_Model
{
    public class PatientInfo
    {
        [JsonProperty] private int patientId;
        [JsonProperty] private string patientLastName;
        [JsonProperty] private string patientMidName;
        [JsonProperty] private string patientFirstName;
        [JsonProperty] private string birthdate;
        [JsonProperty] private string address1;
        [JsonProperty] private string address2;
        [JsonProperty] private string province;
        [JsonProperty] private string city;
        [JsonProperty] private string email;
        [JsonProperty] private string phn;
        [JsonProperty] private string phoneNumber;
        [JsonProperty] private string workNumber;
        [JsonProperty] private string homeNumber;
        [JsonProperty] private string gender;
        [JsonProperty] private string postCode;
        [JsonProperty] private bool deleted;
        [JsonProperty] private int clinicId;
        [JsonProperty] private string pacemaker;
        [JsonProperty] private string superPhysician;
        [JsonProperty] private string weight;
        [JsonProperty] private string height;
        [JsonProperty] private string indications;
        [JsonProperty] private string medications;
        [JsonProperty] private string referPhysician;
        [JsonProperty] private string remark;
        [JsonProperty] private string age;
        public int PatientId { get => patientId; set => patientId = value; }
        public string PatientLastName { get => patientLastName; set => patientLastName = value; }
        public string PatientMidName { get => patientMidName; set => patientMidName = value; }
        public string PatientFirstName { get => patientFirstName; set => patientFirstName = value; }
        public string Birthdate { get => birthdate; set => birthdate = value; }
        public string Address1 { get => address1; set => address1 = value; }
        public string Address2 { get => address2; set => address2 = value; }
        public string Province { get => province; set => province = value; }
        public string City { get => city; set => city = value; }
        public string Email { get => email; set => email = value; }
        public string Phn { get => phn; set => phn = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string WorkNumber { get => workNumber; set => workNumber = value; }
        public string HomeNumber { get => homeNumber; set => homeNumber = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PostCode { get => postCode; set => postCode = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int ClinicId { get => clinicId; set => clinicId = value; }
        public string Pacemaker { get => pacemaker; set => pacemaker = value; }
        public string SuperPhysician { get => superPhysician; set => superPhysician = value; }
        public string Weight { get => weight; set => weight = value; }
        public string Height { get => height; set => height = value; }
        public string Indications { get => indications; set => indications = value; }
        public string Medications { get => medications; set => medications = value; }
        public string ReferPhysician { get => referPhysician; set => referPhysician = value; }
        public string Remark { get => remark; set => remark = value; }
        public string Age { get => age; set => age = value; }
        [JsonConstructor]
        public PatientInfo(int pId,
                           string pLastName,
                           string pMidName,
                           string pFirstname,
                           string birth,
                           string addres1,
                           string addres2,
                           string provin,
                           string City,
                           string mail,
                           string phnum,
                           string phoneNum,
                           string workNum,
                           string homeNum,
                           string sex,
                           string poscode,
                           bool delete,
                           int cId,
                           string paceMaker,
                           string superPhysi,
                           string Weight,
                           string Height,
                           string Indications,
                           string Medications,
                           string referPhysi,
                           string Remark,
                           string Age)
        {
            patientId = pId;
            patientLastName = pLastName;
            patientMidName = pMidName;
            patientFirstName = pFirstname;
            birthdate = birth;
            address1 = addres1;
            address2 = addres2;
            province = provin;
            city = City;
            email = mail;
            phn = phnum;
            phoneNumber = phoneNum;
            workNumber = workNum;
            homeNumber = homeNum;
            gender = sex;
            postCode = poscode;
            deleted = delete;
            clinicId = cId;
            pacemaker = paceMaker;
            superPhysician = superPhysi;
            weight = Weight;
            height = Height;
            indications = Indications;
            medications = Medications;
            referPhysician = referPhysi;
            remark = Remark;
            age = Age;
        }
        public PatientInfo(string pLastName,
                           string pMidName,
                           string pFirstname,
                           string birth,
                           string addres1,
                           string addres2,
                           string provin,
                           string City,
                           string mail,
                           string phnum,
                           string phoneNum,
                           string workNum,
                           string homeNum,
                           string sex,
                           string poscode,
                           bool delete,
                           int cId,
                           string paceMaker,
                           string superPhysi,
                           string Weight,
                           string Height,
                           string Indications,
                           string Medications,
                           string referPhysi,
                           string Remark,
                           string Age)
        {
            patientLastName = pLastName;
            patientMidName = pMidName;
            patientFirstName = pFirstname;
            birthdate = birth;
            address1 = addres1;
            address2 = addres2;
            province = provin;
            city = City;
            email = mail;
            phn = phnum;
            phoneNumber = phoneNum;
            workNumber = workNum;
            homeNumber = homeNum;
            gender = sex;
            postCode = poscode;
            deleted = delete;
            clinicId = cId;
            pacemaker = paceMaker;
            superPhysician = superPhysi;
            weight = Weight;
            height = Height;
            indications = Indications;
            medications = Medications;
            referPhysician = referPhysi;
            remark = Remark;
            age = Age;
        }
        public PatientInfo(int pid,
                           string fname,
                           string lname)
        {
            PatientId = pid;
            PatientFirstName = fname;
            PatientLastName = lname;
        }
    }
}
