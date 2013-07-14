using System;
using Domain.Cust;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest.Cust
{
    [TestClass]
    public class CustomerValidatorIndividualItemsTest : CustomerValidatorBase
    {
        [TestMethod]
        public void NumberTooLong()
        {
            CheckForNumber(Int64.Parse(CommonUtils.GetRandomStringNumeric(CView.NumberLength + 1)));
        }

        [TestMethod]
        public void NumberZero()
        {
            CheckForNumber(0);
        }

        [TestMethod]
        public void NumberNegative()
        {
            CheckForNumber(-1);
        }

        [TestMethod]
        public void NameTooLong()
        {
            CheckForName(CommonUtils.GetRandomString(CView.NameLength + 1));
        }

        [TestMethod]
        public void NameNull()
        {
            CheckForName(null);
        }

        [TestMethod]
        public void NameEmpty()
        {
            CheckForName("");
        }

        [TestMethod]
        public void AddressTooLong()
        {
            CheckForAddress(CommonUtils.GetRandomString(CView.AddressLength + 1));
        }

        [TestMethod]
        public void AddressNull()
        {
            CheckForAddress(null);
        }

        [TestMethod]
        public void AddressEmpty()
        {
            CheckForAddress("");
        }

        [TestMethod]
        public void MobileTooLong()
        {
            CheckForMobile(CommonUtils.GetRandomString(CView.MobileLength + 1));
        }

        [TestMethod]
        public void MobileNull()
        {
            CheckForMobile(null);
        }

        [TestMethod]
        public void MobileWithAlphaCharacters()
        {
            CheckForMobile("12a3");
        }

        [TestMethod]
        public void MobileEmpty()
        {
            CheckForMobile("");
        }

        [TestMethod]
        public void InvaidOperation()
        {
            _cv.Button = "junkValue";
            EnsureMessageReceived(CustomerCRUDService.InvalidOperation);
        }

        private void CheckForMobile(string mobile)
        {
            _cv.Mobile = mobile;
            EnsureMessageReceived(CustomerCRUDService.MobileInvalid);
        }

        private void CheckForAddress(string addr)
        {
            _cv.Address = addr;
            EnsureMessageReceived(CustomerCRUDService.AddressInvalid);
        }

        private void CheckForName(string name)
        {
            _cv.Name = name;
            EnsureMessageReceived(CustomerCRUDService.NameInvalid);
        }

        private void CheckForNumber(long n)
        {
            _cv.Number = n;
            EnsureMessageReceived(CustomerCRUDService.CustomerNumberInvalid);
        }
    }
}