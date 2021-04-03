using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;

namespace Uvic_Ecg_ArbutusHolter
{
    public partial class CreatePatientForm : Form
    {
        long num;
        Client cpFormClient;
        PatientResource patientResource = new PatientResource();
        public CreatePatientForm(Client client)
        {
            InitializeComponent();
            cpFormClient = client;
            foreach (var gen in Enum.GetValues(typeof(Config.Gender)))
            {
                genderCB.Items.Add(gen);
            }
        }
        private async void CreateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lastNameTB.Text) &&
                !string.IsNullOrWhiteSpace(firstNameTB.Text) &&
                !string.IsNullOrWhiteSpace(birthDateTB.Text) &&
                !string.IsNullOrWhiteSpace(address1TB.Text) &&
                !string.IsNullOrWhiteSpace(provinceTB.Text) &&
                !string.IsNullOrWhiteSpace(phnTB.Text) &&
                !string.IsNullOrWhiteSpace(genderCB.Text) &&
                !string.IsNullOrWhiteSpace(cityTB.Text) &&
                !string.IsNullOrWhiteSpace(homeNumTB.Text) &&
                !string.IsNullOrWhiteSpace(mailTB.Text))
            {
                if (phnTB.Text.Length < 10 && !long.TryParse(phnTB.Text, out num))
                {
                    MessageBox.Show(ErrorInfo.WrongPhn.ErrorMessage);
                    return;
                }
                if (!DateFormat(birthDateTB.Text, "-"))
                {
                    MessageBox.Show(ErrorInfo.WrongDate.ErrorMessage);
                    return;
                }
                string replaceDate = ChangeFormat(birthDateTB.Text);
                if (!string.IsNullOrWhiteSpace(mailTB.Text) && !RegexUtilities.IsValidEmail(mailTB.Text))
                {
                    MessageBox.Show(ErrorInfo.WrongMail.ErrorMessage);
                    return;
                }
                PatientInfo newPatient = new PatientInfo(lastNameTB.Text, midNameTB.Text, firstNameTB.Text, replaceDate, address1TB.Text, null,
                                                                     provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                                     genderCB.Text, postCodeTB.Text, false, Config.ClinicId, pacemakerTB.Text, superPhyTB.Text,
                                                                     null, null, null, null, null, null, ageTB.Text);
                UseWaitCursor = true;
                try
                {
                    string errorMsg = await patientResource.CreatePatient(newPatient, cpFormClient);
                    if (errorMsg == ErrorInfo.OK.ErrorMessage)
                    {
                        MessageBox.Show(ErrorInfo.Created.ErrorMessage);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMsg);
                    }
                }
                catch (TokenExpiredException teex)
                {
                    throw teex;
                }
                catch (Exception ex)
                {
                    using (StreamWriter w = File.AppendText(FileName.Log.Name))
                    {
                        LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                    }
                    MessageBox.Show(ex.ToString());
                }
                UseWaitCursor = false;
            }
        }
        private bool DateFormat(string date, string type)
        {
            DateTime result;
            if (DateTime.TryParseExact(
                date,
                "MM/dd/yyyy",
                null,
                DateTimeStyles.AssumeUniversal,
                out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string ChangeFormat(string originDate)
        {
            string[] ls;
            string posChange;
            if (originDate.Contains('/'))
            {
                ls = originDate.Split('/');
                posChange = ls[2] + "-" + ls[0] + "-" + ls[1];
            }
            else
            {
                ls = originDate.Split('-');
                posChange = ls[1] + "/" + ls[2] + "/" + ls[0];
            }
            return posChange;
        }
    }
}
