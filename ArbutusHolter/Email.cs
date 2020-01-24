using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class Email : Form
    {
        string to, subject, content;
        PdfDocument mailPdf;
        PdfPage p;
        XGraphics xgf;
        XTextFormatter xtf;
        XFont font = new XFont("Calibri", 14, XFontStyle.Regular);
        XRect rect;
        Client mailClient = new Client();
        RestModel<ResultJson> restmodel;
        MailResource mRespurce = new MailResource();
        AppointmentMail mail;
        public Email(Client client)
        {
            try
            {
                InitializeComponent();
                mailClient = client;
                restmodel = mRespurce.GetTemplate(mailClient);
                if (restmodel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    mailContentRichTB.Text = restmodel.Entity.Model.Message;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void SendMailBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (RegexUtilities.IsValidEmail(toTextBox.Text))
                {
                    MessageBox.Show(ErrorInfo.WrongMail.ErrorMessage);
                    return;
                }
                if (string.IsNullOrWhiteSpace(toTextBox.Text) || string.IsNullOrWhiteSpace(subjectTextBox.Text))
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                mail = new AppointmentMail(toTextBox.Text, mailContentRichTB.Text);
                restmodel = mRespurce.SendMail(mailClient, mail);
                if (ErrorInfo.OK.ErrorMessage.Equals(restmodel.ErrorMessage))
                {
                    MessageBox.Show("Email has been sent");
                }
                else
                {
                    MessageBox.Show(restmodel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void SavePdfBtn_Click(object sender, EventArgs e)
        {
            try
            {
                mailPdf = new PdfDocument();
                mailPdf.Info.Title = "Mail";
                p = mailPdf.AddPage();
                xgf = XGraphics.FromPdfPage(p);
                xtf = new XTextFormatter(xgf);
                to = "To: " + toTextBox.Text;
                subject = "Subject: " + subjectTextBox.Text;
                content = mailContentRichTB.Text;
                rect = new XRect(80, 80, p.Width, 50);
                xtf.DrawString(to, font, XBrushes.Black, rect, XStringFormats.TopLeft);
                rect = new XRect(80, 140, p.Width, 50);
                xtf.DrawString(subject, font, XBrushes.Black, rect, XStringFormats.TopLeft);
                rect = new XRect(80, 200, p.Width, p.Height);
                xtf.DrawString(content, font, XBrushes.Black, rect, XStringFormats.TopLeft);
                mailPdf.Save(FileName.MailPdf.Name);
                System.Diagnostics.Process.Start(FileName.MailPdf.Name);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
    }
}
