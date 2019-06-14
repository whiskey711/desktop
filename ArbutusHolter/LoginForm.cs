using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;

namespace ArbutusHolter
{
    public partial class LoginForm : Form
    {
        private string url = "http://ecg.uvic.ca:443/v1/clinic/login";
        public IEnumerable<string> tokenObj;
        public string token;
        public string fullToken;
        public LoginForm()
        {
            InitializeComponent();
            Console.Read();
        }


        // after user clicked registerButton, they will be directed to registerForm
        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm regForm = new RegisterForm();
            //if regForm closed, close this hiden form as well
            regForm.Closed += (s, args) => this.Close();
            regForm.Show();
        }

        // after user clicked forgetPWbutton, they will be directed to forgetPWForm
        private void forgetPWButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgetPwForm forPForm = new ForgetPwForm();
            forPForm.Closed += (s, args) => this.Close();
            forPForm.Show();
        }

        // after user clicked login button, they will be directed to maininterface
        private void loginButton_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            string errorMsg = client.Login(userEmail.Text, password.Text);
            if (errorMsg.Equals("ok"))
            {
                this.Hide();
                homeForm hForm = new homeForm();
                hForm.Closed += (s, args) => this.Close();
                hForm.Show();
            }
            else
            {
                Hide();
                ErrorForm errorForm = new ErrorForm(errorMsg);
                errorForm.Show(this);
            }
        }

    }
}
