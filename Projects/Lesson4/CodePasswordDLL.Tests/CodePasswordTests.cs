using CodePasswordDLL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodePasswordDLL.Tests
{
    [TestClass()]
    public class CodePasswordTests
    {
        [TestMethod]
        //[Ignore]
        [Description("Test getCodPassword_abc_bcd method")]
        public void getCodPassword_abc_bcd()
        {
            // arrange
            string strIn = "abc";
            string strExpected = "bcd";
            // act
            string strActual = CodePassword.getCodPassword(strIn);
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
            string strActual = CodePassword.getCodPassword(strIn);
            //assert
            Assert.AreEqual(strExpected, strActual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException/*IndexOutOfRangeException*/))]
        public void TryMakingException()
        {
            string str = null;
            string res = str.ToString();
        }

        [TestMethod]        
        public void checkIsNull()
        {
            string str = null;
            Assert.IsNull(str);
        }
        [TestMethod]
        public void checkIsTrue()
        {
            Assert.IsTrue(1==1);
            checkIsFalse();
        }
        [TestMethod]
        public void checkIsFalse()
        {            
            Assert.IsFalse(1==0);
        }
    }
}
