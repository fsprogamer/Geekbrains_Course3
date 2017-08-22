using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using EmailSenDerviceDLL;
using WpfTestMailSender.Classes;

namespace WpfTestMailSender
{

    /// <summary>
    /// Класс планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Так же помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    public  class SchedulerClass
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер 
        EmailSendServiceClass emailSender; // экземпляр класса отвечающего за отправку писем
        List<Mailing> m = new List<Mailing>();

        /// <summary>
        /// Методе который превращаем строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public static TimeSpan GetSendTime(string strSendTime)
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
        public void SendEmails(EmailSenDerviceDLL.EmailSendServiceClass emailSender, Mailing m)
        {
            this.emailSender = emailSender; // Экземпляр класса отвечающего за отправку писем присваиваем 
            this.m.Add(m);
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (m.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Письма отправлены.");
            }
            else if (m.Any(mm=>mm.DateAndTime <= DateTime.Now))
            {
                Mailing mailing = m.First(mm => mm.DateAndTime <= DateTime.Now);
                emailSender.Body = mailing.Text;
                emailSender.Subject = $"Рассылка от {mailing.DateAndTime.ToShortTimeString()} ";
                emailSender.SendMails(mailing);
                mailing.IsSent = true;
                m.Remove(mailing);
            }

        }

      

    }

}

