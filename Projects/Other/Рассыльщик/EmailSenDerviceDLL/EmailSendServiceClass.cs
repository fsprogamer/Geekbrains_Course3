using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfTestMailSender.Classes;

namespace EmailSenDerviceDLL
{
    public class EmailSendServiceClass
    {
        #region vars
        private string strLogin; // email c которого будет рассылаться почта
        private string _strPassword; // пароль к email с которого будет рассылаться почта
        private string _smtpServer;
        private int smtpPort;
        private string strBody; // текст письма для отправки
        private string strSubject; // тема письма для отправки
        #endregion

        public string Body
        {
            get { return strBody; }
            set { strBody = value; }
        }
        public string Subject
        {
            get { return strSubject; }
            set { strSubject = value; }
        }
        public EmailSendServiceClass(EmailSender sender, string strBody)
        {
            strLogin = sender.EmailAdress;
            _strPassword = sender.Password;
            smtpPort = sender.SmtpServer.Port;
            _smtpServer = sender.SmtpServer.Smtp;
            this.strBody = strBody;
        }

        private void SendMail(string mail, string name, string body) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(_smtpServer, smtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, CodePasswordDLL.CodePassword.getCodPassword(_strPassword));
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }

            }
        }

        public void SendMails(Mailing m)
        {
            Parallel.ForEach(m.Recipients, mail => { SendMail(mail.EmailAdress, mail.Name, m.Text); });
        }
    }

}


