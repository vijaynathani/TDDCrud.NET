using Domain.Cust;
using DataTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.CustomerTableCheck
{
    [TestClass]
    public class LengthTest : DbBaseTest
    {
        [TestMethod]
        public void CheckLengthOfId()
        {
            var length = GetMaxLengthOfNumericColumnFromDatabase("id", "Customers");
            Assert.IsTrue(length >= CView.IdLength);
        }

        [TestMethod]
        public void CheckLengthOfNumber()
        {
            var length = GetMaxLengthOfNumericColumnFromDatabase("number", "Customers");
            Assert.IsTrue(length >= CView.NumberLength);
        }

        [TestMethod]
        public void CheckLengthOfName()
        {
            var length = GetMaxLengthOfAlphanumericColumnFromDatabase("name", "Customers");
            Assert.IsTrue(length >= CView.NameLength);
        }

        [TestMethod]
        public void CheckLengthOfAddress()
        {
            var length = GetMaxLengthOfAlphanumericColumnFromDatabase("address", "Customers");
            Assert.IsTrue(length >= CView.AddressLength);
        }

        [TestMethod]
        public void CheckLengthOfMobile()
        {
            var length = GetMaxLengthOfAlphanumericColumnFromDatabase("mobile", "Customers");
            Assert.IsTrue(length >= CView.MobileLength);
        }
    }
}