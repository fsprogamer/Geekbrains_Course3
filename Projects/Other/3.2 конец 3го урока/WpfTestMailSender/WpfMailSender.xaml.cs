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
    {
        public WpfMailSender()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            cbSenderSelect.SelectedIndex = 0;
            cbSmtpSelect.ItemsSource = VariablesClass.SmtpServers;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";
            cbSmtpSelect.SelectedIndex = 0;
            DBClass db = new DBClass();
            dgEmails.ItemsSource = db.Emails;


        }
        public event RoutedEventHandler btnNextClick;
        public event RoutedEventHandler btnPreviosClick;

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNextClick?.Invoke(sender, e);
        }


        private void btnPrevios_Click(object sender, RoutedEventArgs e)
        {
            btnPreviosClick?.Invoke(sender, e);
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
            emailSender.SendMails((IQueryable<Emails>)dgEmails.ItemsSource);

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
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Emails>)dgEmails.ItemsSource);

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
            tabControl.SelectedIndex -= 0;
        }
    }
}
