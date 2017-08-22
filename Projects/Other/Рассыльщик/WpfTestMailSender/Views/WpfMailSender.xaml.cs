using EmailSenDerviceDLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {       
      public ObservableCollection<SmtpServer> SmtpServers { get; set; }

       public Repository rep;
        public WpfMailSender()
        {
            
            InitializeComponent();
           
            rep = new Repository(true);

            Initialize();            
        }

        public void Initialize()
        {
            Loaded += (a, b) => SendPlannedMailings();
            Closed += (a, b) => { rep.Context.SaveChanges(); };
            cbxEmails.ItemsSource = rep.Context.Emails.Local;
            cbxEmails.SelectedIndex = 0;
            cbSenders.ItemsSource = rep.Context.SendersEmails.Local;
            cbSenders.SelectedIndex = 0;
            cbxSmtpServer.ItemsSource = rep.Context.SmtpServers.Local;
            cbxSmtpServer.SelectedIndex = 0;
            SmtpServers = rep.Context.SmtpServers.Local;

            dgMailings.ItemsSource = rep.MailingsPlanned;
            dgMailingsStatistics.ItemsSource = rep.MailingsDone;

            dgEmails.ItemsSource = rep.Context.Emails.Local;
            listBoxEmails.ItemsSource = rep.Context.Emails.Local;
            dgSenders.ItemsSource = rep.Context.SendersEmails.Local;
            dgSmtpServer.ItemsSource = rep.Context.SmtpServers.Local;
            DataContext = this;
        }
        

        public void SendPlannedMailings()
        {
 
            int i = 0;
            foreach (Mailing m in rep.Context.Mailings)
            {
                if (m.DateAndTime > DateTime.Now)
                {
                    i++;
                    SendPlanned((EmailSender)cbSenders.SelectedItem, m);
                }
            }
            MessageBox.Show("Рассылки (" + i.ToString()+ " шт.) добавлены в очередь");
        }


        private void button_ClickModeratorDeleteRowMailing(object sender, RoutedEventArgs e)
        {
            Mailing em = (Mailing)dgMailings.SelectedItem;
            rep.DeleteMailing(em);
        }


       

        public static void SendNow(EmailSender sender, Mailing m)
        {
            EmailSendServiceClass emailSender = new EmailSendServiceClass(sender, m.Text);
            emailSender.SendMails(m);

        }

        public static void SendPlanned(EmailSender sender, Mailing mailing)
        {
            EmailSendServiceClass emailSender = new EmailSendServiceClass(sender, mailing.Text);
            SchedulerClass sc = new SchedulerClass();
            if (emailSender != null)
            {
                sc.SendEmails(emailSender, mailing);
            }

        }

        private void MiClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckSending()
        {
            try
            {
                string strBody = txBoxTextMail.Text;
                if ((strBody == "") || (strBody == "\r\n"))
                {
                    throw new Exception("Напишите текст письма");
                }
                if(listBoxEmails.SelectedItems.Count==0)
                {
                    throw new Exception("Выберите получателей");
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckSending();
                List<Email> mails = listBoxEmails.SelectedItems.Cast<Email>().ToList();
                string text = txBoxTextMail.Text;
                EmailSender esender = cbSenders.SelectedItem as EmailSender;
                Task t = new Task(() =>
                {                
                Mailing m = new Mailing() { Recipients = new ObservableCollection<Email>(mails), DateAndTime = DateTime.Now, Text =text  };
                SendNow(esender, m);
                Dispatcher.Invoke(() => { rep.AddMailing(m); });
                });
                t.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void BtnSendPlanned_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckSending();
                var mails = listBoxEmails.SelectedItems.Cast<Email>().ToList();
                TimeSpan tsSendTime = SchedulerClass.GetSendTime(tbTimePicker.Text);
                if (tsSendTime == new TimeSpan())
                {
                   throw new Exception("Некорректный формат даты");
                    
                }
                DateTime dtSendDateTime = Convert.ToDateTime(tbTimePicker.Text);
                if (dtSendDateTime < DateTime.Now)
                {
                    throw new Exception("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                }
                Mailing m = new Mailing() { Recipients = new ObservableCollection<Email>(mails), DateAndTime = tbTimePicker.Value.Value, Text = txBoxTextMail.Text };
                SendPlanned(cbSenders.SelectedItem as EmailSender, m);
                rep.AddMailing(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        




        private void BtnGoToPlanner_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemPlanner;
        }


        private void BtnAddAddress_Click(object sender, RoutedEventArgs e)
        {
            Email em = new Email() { EmailAdress = txblAdress.Text, Name = txblName.Text };
            if (rep.IfEmailIsNew(em))
            {
                rep.AddMail(em);
                dgEmails.ItemsSource = rep.Context.Emails.Local;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rep.GenerateExcelForEmails();
        }

        private void BtnAddSmtp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string smtp = tbxSmtp.Text;
                string port = tbxPort.Text;
                int n = 0;
                bool ok = int.TryParse(port, out n);

                if (smtp == "" || port == "" || !ok)
                {
                    throw new Exception("Введите корректные данные");
                }

                bool c = rep.Context.SmtpServers.Any(b => b.Smtp == smtp);
                if (c)
                {
                    throw new Exception("Такой smtp уже существует");
                }
                rep.AddSmtp(new SmtpServer() { Smtp = smtp, Port = n });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAddSender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txblName.Text;
                string adress = txblAdress.Text;
                string passwords = psbox.Password;

                if (name == "" || adress == "" || passwords == "")
                {
                    throw new Exception("Введите данные");
                }

                bool c = rep.Context.SendersEmails.Any(b => b.EmailAdress == adress);
                if (c)
                {
                    throw new Exception("Такой адрес уже существует");
                }
                rep.AddSender(new EmailSender() { Name = name, EmailAdress = adress, Password = passwords , SmtpServer = cbxSmtpServer.SelectedItem as SmtpServer});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button_ClickDeleteSender(object sender, RoutedEventArgs e)
        {
            try
            {
                rep.DeleteSender((EmailSender)dgSenders.SelectedItem);
            }

            catch (Exception) { }
}
        private void button_ClickDeleteSmtp(object sender, RoutedEventArgs e)
        {
            try
            {
                rep.DeleteSmtp((SmtpServer)dgSmtpServer.SelectedItem);
            }
            catch (Exception) { }

        }

        private void button_ClickDeleteMail(object sender, RoutedEventArgs e)
        {
            try
            { 
                rep.DeleteMail((Email)dgEmails.SelectedItem);
            }
            catch (Exception) { }
        }

        private void button_ClickDeleteMailing(object sender, RoutedEventArgs e)
        {
            rep.DeleteMailing((Mailing)dgMailings.SelectedItem);
        }


        private void button_dgMailingsMailNow(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            (sender as Button).Visibility = Visibility.Hidden;
            SendNowFromDataGrid(dgMailings);
        }

        private void button_dgMailingsStatisticsMailNow(object sender, RoutedEventArgs e)
        {
            SendNowFromDataGrid(dgMailingsStatistics);
        }

        public void SendNowFromDataGrid(DataGrid grid)
        {
            Mailing m = grid.SelectedItem as Mailing;
            Task t = new Task(() =>
            {
                EmailSender c = rep.DefaultSender();
                EmailSendServiceClass esss = new EmailSendServiceClass(c, m.Text);
                esss.SendMails(m);
                m.DateAndTime = DateTime.Now;
                m.IsSent = true;
                Dispatcher.Invoke(() =>
                { 
                    rep.AddMailing(m);
                    RefreshDataGridsMailing();
                });
            });
            t.Start();
        }

        public void RefreshDataGridsMailing()
        {
            rep.DevideMailings();
            dgMailings.ItemsSource = rep.MailingsPlanned;
            dgMailingsStatistics.ItemsSource = rep.MailingsDone;
        }

        private void button_ClickDeleteMailingRecipient(object sender, RoutedEventArgs e)
        {
            //Email em = listBoxMailing.SelectedItem as Email;
            rep.DeleteRecipient(dgMailings.SelectedItem as Mailing, (sender as Button).Tag as Email);          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Mailing m = dgMailings.SelectedItem as Mailing;
                Email em = cbxEmails.SelectedItem as Email;
                if (m.Recipients.Contains(em))
                {
                    throw new Exception("Выбранный е-мэйл уже занесен в список получаталей");
                }
                m.Recipients.Add(em);
                    }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
