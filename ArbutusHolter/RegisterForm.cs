using System;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class RegisterForm : Form
    {
        Client client = new Client();
        private PublicResources publicResources = new PublicResources();
        private Client registrationClient = new Client();
        Nurse newNurse;
        public RegisterForm()
        {
            InitializeComponent();
        }
        // After user click registerButton, the finishPanel is visible
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (lastN.Text != "" && firstN.Text != "" && password.Text != "" && confirmPass.Text != "" && RegexUtilities.IsValidEmail(email.Text))
                {
                    if (password.Text == confirmPass.Text)
                    {
                        newNurse = new Nurse(4,
                                             lastN.Text,
                                             null,
                                             firstN.Text,
                                             null,
                                             email.Text,
                                             1,
                                             password.Text,
                                             false);
                        string errorMsg = publicResources.Registration(email.Text, registrationClient).ErrorMessage;
                        if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                        {
                            finishPanel.Visible = true;
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
        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMsg = publicResources.VeriFycode(regVerifyTextBox.Text, client).ErrorMessage;
                if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                {
                    if (client.Register(newNurse))
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
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
    }
}
