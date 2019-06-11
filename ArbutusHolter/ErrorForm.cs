using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(string erroMsg)
        {
            InitializeComponent();
            label1.Text = erroMsg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lForm = new LoginForm();
            //lForm.Closed += (s, args) => this.Close();
            lForm.Show(this);
        }
    }
}
