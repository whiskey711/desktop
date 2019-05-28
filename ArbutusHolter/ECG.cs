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
    public partial class ECG : Form
    {
        public ECG()
        {
            InitializeComponent();
            ecgView.plotAll();
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            ecgView.moveRight();
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            ecgView.moveLeft();
        }
    }
}
