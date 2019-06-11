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
            User user = new User
            {
                username = userEmail.Text,
                password = password.Text
            };
            string json = JsonConvert.SerializeObject(user);
            HttpContent Jsoncontent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            //if you want to add token in the header, use this statement.
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            var result = httpClient.PostAsync(url, Jsoncontent).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Headers.TryGetValues("Authorization", out tokenObj))
                {
                    fullToken = tokenObj.First();
                    //Get the index after 'Berear'
                    int beginIndex = fullToken.IndexOf('r', fullToken.IndexOf('r') + 1);
                    //store the token in the 'token' string
                    token = fullToken.Substring(beginIndex + 1);
                }
                //Test
                //userEmail.Text = token;
                this.Hide();
                homeForm hForm = new homeForm();
                hForm.Closed += (s, args) => this.Close();
                hForm.Show();
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


        public class User
        {
            public string username;
            public string password;
        }
    }
}
