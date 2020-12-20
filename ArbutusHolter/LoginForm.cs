using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class LoginForm : Form
    {
        Client client = new Client();
        public ErrorInfo errorInfo;
        public LoginForm()
        {
            InitializeComponent();
            Console.Read();
        }
        // after user clicked registerButton, they will be directed to registerForm
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterForm regForm = new RegisterForm();
                regForm.Show();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        // after user clicked forgetPWbutton, they will be directed to forgetPWForm
        private void ForgetPWButton_Click(object sender, EventArgs e)
        {
            try
            {
                ForgetPwForm forPForm = new ForgetPwForm(client);
                forPForm.Show();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        // after user clicked login button, they will be directed to maininterface
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (string.IsNullOrEmpty(userEmail.Text)|| string.IsNullOrEmpty(password.Text))
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                errorInfo = await client.Login(userEmail.Text, password.Text);
                if (ErrorInfo.OK.ErrorMessage.Equals(errorInfo.ErrorMessage))
                {
                    Hide();
                    AppointmentForm appForm = new AppointmentForm(client);
                    appForm.FormClosed += (s, args) => Show();
                    appForm.Show();
                }
                else
                {
                    MessageBox.Show(errorInfo.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException || ex.InnerException is WebException)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                    using (StreamWriter w = File.AppendText(FileName.Log.Name))
                    {
                        LogHandle.Log(ex.InnerException.ToString(), ex.StackTrace, w);
                    }
                }
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                System.Diagnostics.Process.Start(FileName.Log.Name);
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
    }
}
