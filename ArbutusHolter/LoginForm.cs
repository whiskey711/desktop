using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        
        // when user type in their password into password textbox, it should display as * to protect it
        /*
        private void password_TextChanged(object sender, EventArgs e)
        {
            password.PasswordChar = '*';
        }
        */
        

        // after user clicked registerButton, they will be directed to registerForm
        private void registerButton_Click(object sender, EventArgs e)
        {
            Hide();
            RegisterForm regForm = new RegisterForm();
            regForm.Show();
        }

        // after user clicked forgetPWbutton, they will be directed to forgetPWForm
        private void forgetPWButton_Click(object sender, EventArgs e)
        {
            Hide();
            ForgetPwForm forPForm = new ForgetPwForm();
            forPForm.Show();
        }

        // after user clicked login button, they will be directed to maininterface
        private void loginButton_Click(object sender, EventArgs e)
        {
            homeForm hForm = new homeForm();
            hForm.Show();
            Hide();
        }

        
    }
}
