using MailSender;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Tests
{
    [TestClass()]
    public class SchedulerClassTests
    {
        public SchedulerClass sc;
        public TimeSpan ts;

        // Запускается перед стартом каждого тестирующего метода. 
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
            sc = new SchedulerClass();
            ts = new TimeSpan(); // возвращаем в случае ошибочно введенного времени
        }

        [TestMethod()]
        public void GetSendTime_empty_ts()
        {
            string strTimeTest = "";
            TimeSpan tsTest = sc.GetSendTime(strTimeTest);
            Assert.AreEqual(ts, tsTest);
        }

        [TestMethod()]
        public void GetSendTime_sdf_ts()
        {
            string strTimeTest = "sdf";
            TimeSpan tsTest = sc.GetSendTime(strTimeTest);
            Assert.AreEqual(ts, tsTest);
        }

        [TestMethod()]
        public void GetSendTime_correctTime_Equal()
        {
            string strTimeTest = "12:12";
            TimeSpan tsCorrect = new TimeSpan(12, 12, 0);
            TimeSpan tsTest = sc.GetSendTime(strTimeTest);
            Assert.AreEqual(tsCorrect, tsTest);
        }

        [TestMethod()]
        public void GetSendTime_inCorrectHour_ts()
        {
            string strTimeTest = "25:12";
            TimeSpan tsTest = sc.GetSendTime(strTimeTest);
            Assert.AreEqual(ts, tsTest);
        }

        [TestMethod()]
        public void GetSendTime_inCorrectMin_ts()
        {
            string strTimeTest = "12:65";
            TimeSpan tsTest = sc.GetSendTime(strTimeTest);
            Assert.AreEqual(ts, tsTest);
        }

        [TestMethod]
        public void GetInterest_ReturnsExpectedInterest()
        {
            int i = 1000;
            string i_str = "1000";

            var privateObject = new PrivateObject(sc);
            var result = privateObject.Invoke("PrivateMethod", new object[] { i_str });

            Assert.AreEqual(result, i);
        }
    }

}
