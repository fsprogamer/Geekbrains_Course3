using CodePasswordDLL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodePasswordDLL.Tests
{
    [TestClass]
    public class CodePasswordTests
    {
        [TestMethod]
        public void getCodPassword_abc_bcd()
        {
            // arrange
            string strIn = "abc";
            string strExpected = "bcd";

            // act
            string strActual = PasswordClass.getCodPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }

        [TestMethod()]
        public void getCodPassword_empty_empty()
        {
            // arrange 
            string strIn = "";
            string strExpected = "";

            // act 
            string strActual = PasswordClass.getCodPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }
    }
}
