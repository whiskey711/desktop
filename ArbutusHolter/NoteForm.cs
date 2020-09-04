using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uvic_Ecg_Model;

namespace Uvic_Ecg_ArbutusHolter
{
    public partial class NoteForm : Form
    {
        private EcgTest theTest;
        public NoteForm(EcgTest test)
        {
            InitializeComponent();
            theTest = test;
            LoadAllComments();
        }
        private void CommentLs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadAllComments()
        {
            string allComments = theTest.Comment;
            if (string.IsNullOrEmpty(allComments))
            {
                return;
            }
            List<string[]> commentArrLs = RegexUtilities.MatchComment(allComments);
            foreach (string[] commentArr in commentArrLs)
            {
                ListViewItem lsItem = new ListViewItem(commentArr);
                lsItem.ToolTipText = commentArr[2];
                commentLs.Items.Add(lsItem);
            }
        }
    }
}
