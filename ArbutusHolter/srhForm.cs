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
    public partial class srhForm : Form
    {
        public srhForm()
        {
            InitializeComponent();
            
        }

        public void AddName(string name)
        {
            matchList.Items.Add(name);
        }
        
        private void matchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Close();
        }
    }
}
