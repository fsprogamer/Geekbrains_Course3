using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailSender
{
    class EmailSendServiceClass
    {
        #region vars
        private string strLogin; // email c которого будет рассылаться почта
        private string strPassword; // пароль к email с которого будет рассылаться почта
        private string strSmtp; // smtp - server
        private int iSmtpPort; // порт для smtp-server
        private string strBody; // текст письма для отправки
        private string strSubject; // тема письма для отправки
        #endregion

        public EmailSendServiceClass(string sLogin, string sPassword, string sSmtp, int iPort)
        {
            strLogin = sLogin;
            strPassword = sPassword;

            strSmtp = sSmtp;
            iSmtpPort = iPort;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = "Hello world!";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }//private void SendMail(string mail, string name)

        public void SendMails(IQueryable<Emails> emails)
        {
            foreach (Emails email in emails)
            {
                SendMail(email.Value, email.Name);
            }
        }
    }//private void SendMail(string mail, string name)

}
