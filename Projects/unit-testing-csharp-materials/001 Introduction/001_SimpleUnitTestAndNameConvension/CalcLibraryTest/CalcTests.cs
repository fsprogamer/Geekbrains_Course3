using CalcLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcLibraryTest
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Sum_1plus2_3returned()
        {
            // arrange
            double x = 1;
            double y = 2;
            double expected = 3;

            // act
            double actual = Calc.Sum(x, y);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
