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
    public partial class homeForm : Form
    {
        public homeForm()
        {
            InitializeComponent();
        }

        private void toMain_Click(object sender, EventArgs e)
        {
            MainInterface mainForm = new MainInterface();
            mainForm.Show();
            //Close();
        }

        private void toCheck_Click(object sender, EventArgs e)
        {
            Record rForm = new Record();
            rForm.Show();
            //Close();
        }
    }
}
