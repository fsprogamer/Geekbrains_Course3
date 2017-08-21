using EmailSenDerviceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    { ComboBox cbSenderSelect;
        ComboBox cbSmtpSelect;
        List<List<string>> EMAILS = new List<List<string>>();
        public WpfMailSender()
        {
            InitializeComponent();
            cbSenderSelect  =  (ComboBox)SenderTools.FindName("cbSelect");
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
             DataContext = this;
            cbSmtpSelect = (ComboBox)SmtpTools.FindName("cbSelect");
            cbSmtpSelect.ItemsSource = VariablesClass.SmtpServers;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";

            //Чтобы база данных пока не мешала работе и запускалась на всех компах, объявлю далее класс
            //ConnectDB();

            ConnectClass();
          
            cbSenderSelect.SelectedIndex = 1;
            cbSmtpSelect.SelectedIndex = 1;
        }

        public void ConnectDB()
        {
            DBClass db = new DBClass();
            dgEmails.ItemsSource = db.Emails;
            foreach (Emails email in (IQueryable<Emails>)dgEmails.ItemsSource)
            {
                EMAILS.Add(new List<string>() { email.Email, email.Name });
            }
        }

        public void ConnectClass()
        {
            List<EmailsClass> emails = new List<EmailsClass>() { new EmailsClass() { Name = "Proger", Email = "ya.superproger@yandex.ru" }, new EmailsClass() { Name = "alex", Email = "ax@ya.ru" } };
            foreach (EmailsClass email in emails)
            {
                EMAILS.Add(new List<string>() { email.Email, email.Name });
            }
            dgEmails.ItemsSource = emails;
      
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            string server = cbSmtpSelect.Text;
            int port = int.Parse(cbSmtpSelect.SelectedValue.ToString());
            string strBody = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            if((strBody == "") || (strBody == "\r\n"))
            {
                MessageBox.Show("Напишите текст письма");
                return;
            }
            if (string.IsNullOrEmpty(strLogin) || string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Выберете отправителя");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword, server, port, strBody);
            emailSender.SendMails(EMAILS);

        }

        private void btnSendPlanned_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            string server = cbSmtpSelect.Text;
            int port = int.Parse(cbSmtpSelect.SelectedValue.ToString());
            string strBody = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword, server, port, strBody);
            sc.SendEmails(dtSendDateTime, emailSender, EMAILS);

        }

        private void btnGoToPlanner_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemPlanner;
        }


        private void tsbTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex += 1;
        }

        private void tsbTabSwitcher_btnPreviosClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex -= 1;
        }


    }
}
