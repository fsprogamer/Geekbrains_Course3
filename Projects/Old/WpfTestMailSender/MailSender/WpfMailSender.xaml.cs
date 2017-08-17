using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MailSender
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

            //DBclass db = new DBclass();
            //dgEmails.ItemsSource = db.Emails;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //public static class VariablesClass
        //{
        //    public static Dictionary<string, string> Senders
        //    {
        //        get { return dicSenders; }
        //    }
        //    private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        //    {
        //        { "79257443993@yandex.ru",PasswordClass.getPassword("1234l;i") },
        //        { "sok74@yandex.ru",PasswordClass.getPassword(";liq34tjk") }
        //    };
        //}
        ///// <summary>
        ///// На вход подаем зашифрованный пароль, на выходе получаем пароль для email
        ///// </summary>
        //public static class PasswordClass
        //{
        //    public static string getPassword(string p_sPassw)
        //    {
        //        string password = "";
        //        foreach (char a in p_sPassw)
        //        {
        //            char ch = a;
        //            ch--;
        //            password += ch;
        //        }

        //        return password;
        //    }

        //    /// <summary>
        //    /// На вход подаем пароль, на выходе получаем зашифрованный пароль
        //    /// </summary>
        //    /// <param name="p_sPassword"></param>
        //    /// <returns></returns>
        //    public static string getCodPassword(string p_sPassword)
        //    {
        //        string sCode = "";
        //        foreach (char a in p_sPassword)
        //        {
        //            char ch = a;
        //            ch++;
        //            sCode += ch;
        //        }
        //        return sCode;
        //    }
        //}

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strLogin) || string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword);
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
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
            EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void btnPlaner_Click(object sender, RoutedEventArgs e)
        {
            tPlanner.IsSelected = true;
        }

        private void tscTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void tscTabSwitcher_btnPreviosClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
