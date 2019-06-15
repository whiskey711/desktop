using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ArbutusHolter.Model;

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
            Client client = new Client();
            if (lastN.Text != "" && firstN.Text != "" && password.Text != "" && confirmPass.Text != "" && email.Text.Contains("@"))
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
                    string errorMsg = client.Register(newNurse);
                    if (errorMsg.Equals("ok"))
                    {
                        finishPanel.Visible = true;
                    }
                    else
                    {
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

        
    }
}
