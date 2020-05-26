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
        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (userEmail.Text == "" || password.Text == "")
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                errorInfo = client.Login(userEmail.Text, password.Text);
                if (ErrorInfo.OK.ErrorMessage.Equals(errorInfo.ErrorMessage))
                {
                    Hide();
                    AppointmentForm appForm = new AppointmentForm(client);
                    appForm.FormClosed += (s, args) => Close();
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

        private void LoginForm_Load(object sender, EventArgs e)
        {
            /*
            password.Left = (this.ClientSize.Width - password.Width) / 2;
            password.Top = (this.ClientSize.Height - password.Height) / 2;
            userEmail.Left = password.Left;
            userEmail.Top = password.Top - userEmail.Height - 2;
            welcomeLabel.Left = (this.ClientSize.Width - welcomeLabel.Width) / 2;
            welcomeLabel.Top = userEmail.Top - welcomeLabel.Height - 15;
            forgetPWButton.Left = (this.ClientSize.Width - forgetPWButton.Width) / 2;
            forgetPWButton.Top = password.Bottom + forgetPWButton.Height + 15;
            loginButton.Left = forgetPWButton.Left;
            loginButton.Top = forgetPWButton.Top - loginButton.Height - 2;
            registerButton.Top = loginButton.Top;
            registerButton.Left = loginButton.Right + 10;
            */
        }
    }
}
