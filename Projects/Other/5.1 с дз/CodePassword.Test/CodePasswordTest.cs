using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePasswordDLL;
using System.Diagnostics;

namespace CodePassword.Test
{
    [TestClass]
    public class CodePasswordTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Debug.WriteLine("Test Cleanup");
        }

        [TestMethod]
        public void getCodPassword_abc_bcd()
        {
            //arrange
            string strIn = "adc";
            string strExpected = "bcd";

            //act
            string strActual = CodePasswordDLL.CodePassword.getCodPassword(strIn);
            Debug.WriteLine(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual, "Переменные отличаются: выходит "+ strActual +" а должно " + strExpected);
            StringAssert.Equals(strExpected, strActual);
        }
        [TestMethod]
        public void getCodPassword_empty_empty()
        {
            //arrange
            string strIn = "";
            string strExpected = "";

            //act
            string strActual = CodePasswordDLL.CodePassword.getCodPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }

        [TestMethod]
        public void getPassword_abc_bcd()
        {
            //arrange
            string strExpected = "abc";
            string strIn = "bcd";

            //act
            string strActual = CodePasswordDLL.CodePassword.getPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }
        [TestMethod]
        public void getPassword_empty_empty()
        {
            //arrange
            string strIn = "";
            string strExpected = "";

            //act
            string strActual = CodePasswordDLL.CodePassword.getPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }
    }
}
