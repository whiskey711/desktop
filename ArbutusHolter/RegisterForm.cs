using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class RegisterForm : Form
    {
        Client client = new Client();
        private PublicResources publicResources = new PublicResources();
        private Client registrationClient = new Client();
        RestModel<Nurse> nRestMod;
        Nurse newNurse;
        int minLen = 10;
        public RegisterForm()
        {
            InitializeComponent();
        }
        // After user click registerButton, the finishPanel is visible
        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (lastN.Text != "" && firstN.Text != "" && password.Text != "" && confirmPass.Text != "" && RegexUtilities.IsValidEmail(email.Text))
                {
                    if (password.Text.Length >= minLen)
                    {
                        if (password.Text == confirmPass.Text) {
                            newNurse = new Nurse(email.Text);
                            string errorMsg = await publicResources.Registration(newNurse, registrationClient);                           
                            if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                            {
                                finishPanel.Visible = true;
                                newNurse.NurseLastName = lastN.Text;
                                newNurse.NurseFirstName = firstN.Text;
                                newNurse.ClinicId = Config.ClinicId;
                                newNurse.Password = password.Text;
                            }
                            else
                            {
                                MessageBox.Show(errorMsg);
                                email.Text = "";
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show(ErrorInfo.Confirm.ErrorMessage);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorInfo.PinTooShort.ErrorMessage);
                    }
                }
                else
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private void Password_TextChanged(object sender, EventArgs e)
        {
            if (password.Text != "")
            {
                password.PasswordChar = '*';
            }
            else
            {
                password.PasswordChar = '\0';
            }
        }
        private void ConfirmPass_TextChanged(object sender, EventArgs e)
        {
            if (confirmPass.Text != "")
            {
                confirmPass.PasswordChar = '*';
            }
            else
            {
                confirmPass.PasswordChar = '\0';
            }
        }
        private async void Submit_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                nRestMod = await publicResources.VeriFycode(regVerifyTextBox.Text, client);
                if (ErrorInfo.OK.ErrorMessage.Equals(nRestMod.ErrorMessage))
                {
                    bool b = await client.Register(newNurse);
                    if (b)
                    {
                        DialogResult result = MessageBox.Show(ErrorInfo.RegistrationComlete.ErrorMessage);
                        if (result == DialogResult.OK)
                        {
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ErrorInfo.Incorrect.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
    }
}
