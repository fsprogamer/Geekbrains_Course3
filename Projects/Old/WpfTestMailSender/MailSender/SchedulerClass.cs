using System.Windows.Threading;
using System.Windows;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MailSender
{
    /// <summary>
    /// Класс планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Так же помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    public class SchedulerClass
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер 
        EmailSendServiceClass emailSender; // экземпляр класса отвечающего за отправку писем
        DateTime dtSend; // дата и время отправки
        IQueryable<Email> emails; // коллекция email'ов адресатов

        Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, string> DatesEmailTexts
        {
            get { return dicDates; }
            set
            {
                dicDates = value;
                dicDates = dicDates.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

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
        public void SendEmails(DateTime dtSend, EmailSendServiceClass emailSender, IQueryable<Email> emails)
        {
            this.emailSender = emailSender; // Экземпляр класса отвечающего за отправку писем присваиваем 
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private int PrivateMethod(string str)
        {
            return Int32.Parse(str);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dicDates.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Письма отправлены.");
            }
            else if (dicDates.Keys.First<DateTime>().ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                emailSender.strBody = dicDates[dicDates.Keys.First<DateTime>()];
                emailSender.strSubject = $"Рассылка от {dicDates.Keys.First<DateTime>().ToShortTimeString()} ";
                emailSender.SendMails(emails);
                dicDates.Remove(dicDates.Keys.First<DateTime>());
            }
        }//private void Timer_Tick(object sender, EventArgs e)

    }

}
