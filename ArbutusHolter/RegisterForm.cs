using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ArbutusHolter
{
    public partial class RegisterForm : Form
    {
        private string registerUrl = "http://ecg.uvic.ca:443/v1/test/nurses";
        public RegisterForm()
        {
            InitializeComponent();
        }


        // after user click registerButton, the finishPanel is visible
        private void registerButton_Click(object sender, EventArgs e)
        {
            if (lastN.Text != "" && firstN.Text != "" && password.Text != "" && confirmPass.Text != "" && email.Text != "")
            {
                if (password.Text == confirmPass.Text)
                {
                    Nurse newNurse = new Nurse(2,
                                               lastN.Text,
                                               null,
                                               firstN.Text,
                                               null,
                                               email.Text,
                                               1,
                                               password.Text,
                                               false);
                    string json = JsonConvert.SerializeObject(newNurse);
                    HttpContent Jsoncontent = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpClient httpClient = new HttpClient();
                    var result = httpClient.PostAsync(registerUrl, Jsoncontent).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        finishPanel.Visible = true;
                    }
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        string fullerrorMsg = result.Content.ReadAsStringAsync().Result;
                        string errorMsg = fullerrorMsg.Substring(fullerrorMsg.IndexOf('"') + 4);
                        Hide();
                        ErrorForm errorForm = new ErrorForm(errorMsg);
                        errorForm.Show(this);

                    }
                }
                else
                {
                    string confirmPassMessage = "Please confirm your password";
                    MessageBoxButtons okBtn = MessageBoxButtons.OK;
                    MessageBox.Show(confirmPassMessage, null, okBtn);
                }

            }
            else
            {
                string fillAllMessage = "Please fill all text field";
                MessageBoxButtons okBtn = MessageBoxButtons.OK;
                MessageBox.Show(fillAllMessage, null, okBtn);
            }
        }

        // after user clicked back button, they will be directed to loginForm
        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginF = new LoginForm(); 
            loginF.Show();
        }


        public class Nurse
        {
            public int nurseId { get; set; }
            public string nurseLastName { get; set; }
            public string nurseMidName { get; set; }
            public string nurseFirstName { get; set; }
            public string nursePhoneNumber { get; set; }
            public string nurseEmail { get; set; }
            public int clinicId { get; set; }
            public string password { get; set; }
            public bool deleted { get; set; }

            public Nurse(int NurseId,
                         string NurseLastName,
                         string NurseMidName,
                         string NurseFirstName,
                         string NursePhoneNumber,
                         string NurseEmail,
                         int ClinicId,
                         string Password,
                         bool Deleted)
            {
                nurseId = NurseId;
                nurseLastName = NurseLastName;
                nurseMidName = NurseMidName;
                nurseFirstName = NurseFirstName;
                nurseMidName = NurseMidName;
                nursePhoneNumber = NursePhoneNumber;
                nurseEmail = NurseEmail;
                clinicId = ClinicId;
                password = Password;
                deleted = Deleted;
            }
        }
    }
}
