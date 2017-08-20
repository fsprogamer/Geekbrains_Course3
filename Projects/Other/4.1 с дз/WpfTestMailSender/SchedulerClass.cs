using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using EmailSenDerviceDLL;
namespace WpfTestMailSender
{

    /// <summary>
    /// Класс планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Так же помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    class SchedulerClass
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер 
        EmailSendServiceClass emailSender; // экземпляр класса отвечающего за отправку писем
        DateTime dtSend; // дата и время отправки
        List<List<string>> emails; // коллекция email'ов адресатов

        /// <summary>
        /// Методе который превращаем строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }

            return tsSendTime; 
        }

        /// <summary>
        ////Непостредственно отправка писем
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(DateTime dtSend, EmailSenDerviceDLL.EmailSendServiceClass emailSender, List<List<string>> emails)
        {
            this.emailSender = emailSender; // Экземпляр класса отвечающего за отправку писем присваиваем 
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                emailSender.SendMails(emails);
                timer.Stop();
                MessageBox.Show("Письма отправлены.");
            }
        }
    }

}

