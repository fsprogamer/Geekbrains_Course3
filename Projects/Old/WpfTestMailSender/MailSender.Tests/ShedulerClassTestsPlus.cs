using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace MailSender.Tests
{
    [TestClass]
    public class ShedulerClassTestsPlus
    {
        private static SchedulerClass sc;
        private static TimeSpan ts;

        // Запускается перед стартом каждого тестирующего метода. 
        [ClassInitialize]
        public static void TestInitialize(TestContext context)
        {
            Debug.WriteLine("Test Initialize");
            sc = new SchedulerClass();
            ts = new TimeSpan(); // возвращаем в случае ошибочно введенного времени
            sc.DatesEmailTexts = new Dictionary<DateTime, string>()
            {
                { new DateTime(2016, 12, 24, 22, 0, 0), "text1" },
                { new DateTime(2016, 12, 24, 22, 30, 0), "text2" },
                { new DateTime(2016, 12, 24, 23, 0, 0), "text3" }
            };
        }

        [TestMethod()]
        public void TimeTick_Dictionare_correct()
        {
            DateTime dt1 = new DateTime(2016, 12, 24, 22, 0, 0);
            DateTime dt2 = new DateTime(2016, 12, 24, 22, 30, 0);
            DateTime dt3 = new DateTime(2016, 12, 24, 23, 0, 0);

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt1.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt2.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt3.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            Assert.AreEqual(0, sc.DatesEmailTexts.Count);
        }


    }
}
