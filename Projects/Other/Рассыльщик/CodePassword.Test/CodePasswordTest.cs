using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePasswordDLL;
namespace CodePassword.Test
{
    [TestClass]
    public class CodePasswordTest
    {
        [TestMethod]
        public void getCodPassword_abc_bcd()
        {
            //arrange
            string strIn = "abc";
            string strExpected = "bcd";

            //act
            string strActual = CodePasswordDLL.CodePassword.getCodPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
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
    }
}
