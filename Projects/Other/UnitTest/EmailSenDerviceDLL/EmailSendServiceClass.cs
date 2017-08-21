﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmailSenDerviceDLL
{
   public class EmailSendServiceClass
    {
        #region vars
        private string strLogin; // email c которого будет рассылаться почта
        private string strPassword; // пароль к email с которого будет рассылаться почта
        private string smtpServer;
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
        public EmailSendServiceClass(string sLogin, string sPassword, string smtpServer, int smtpPort, string strBody)
        {
            strLogin = sLogin;
            strPassword = sPassword;
            this.smtpPort = smtpPort;
            this.smtpServer = smtpServer;
            this.strBody = strBody;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(smtpServer, smtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, CodePasswordDLL.CodePassword.getCodPassword(strPassword));
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

        public void SendMails(List<List<string>> Emails)
        {
            foreach (List<string> email in Emails)
            {
                SendMail(email[0], email[1]);
            }
        }
    }

}
