using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Windows;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();            
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            List<string> listStrMails = new List<string> { "11111@gmail.com", "11111@yandex.ru" };  // Список email'ов //кому мы отправляем письмо
            string strPassword = passwordBox.Password;  // для WinForms - string strPassword = passwordBox.Text;

            //add reference System.Configuration
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string networkCredentialUserName = smtpSection.Network.UserName;
            string networkCredentialPassword = smtpSection.Network.Password;

            foreach (string mail in listStrMails)
            {
                // Используем using, что бы гарантированно удалить объект MailMessage после использования
                using (MailMessage mm = new MailMessage(networkCredentialUserName, mail))
                {
                    // Формируем письмо
                    mm.Subject = "Привет из C#"; // Заголовок письма
                    mm.Body = "Hello, world!"; // Тело письма
                    mm.IsBodyHtml = false; // Не используем html в теле письма

                    // Авторизуемся на smtp сервере и отправляем письмо
                    // Оператор using гарантирует вызов метода Dispose, даже если при вызове 
                    // методов в объекте происходит исключение.                    

                    using (SmtpClient sc = new SmtpClient()) ///*"smtp.yandex.ru", 25*/"smtp.gmail.com", 587
                    {
                        sc.Credentials = new NetworkCredential(networkCredentialUserName, networkCredentialPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                        }
                    }
                }//using (MailMessage mm = new MailMessage("sender@yandex.ru", mail))
            }
            //MessageBox.Show("Работа завершена.");

            SendEndWindow sew = new SendEndWindow();
            sew.Show();
        }
    }
}
