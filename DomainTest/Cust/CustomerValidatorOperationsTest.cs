using Data;
using Domain.Cust;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest.Cust
{
    [TestClass]
    public class CustomerValidatorOperationsTest : CustomerValidatorBase
    {
        private readonly Customer _cust = new Customer();

        [TestInitialize]
        public void SetUp()
        {
            _cust.number = 5555;
            _cust.name = "n5555";
            _cust.address = "a5555";
            _cust.mobile = "555555";
            _mock.LastCustomer = _cust;
        }

        [TestMethod]
        public void ViewOperationForRecordAbsent()
        {
            _cv.Button = CustomerCRUDService.ViewOp;
            EnsureMessageReceived(CustomerCRUDService.CustomerNumberAbsent);
        }

        [TestMethod]
        public void ViewOperationForRecordPresent()
        {
            _cv.Button = CustomerCRUDService.ViewOp;
            _mock.CheckCustomer = true;
            EnsureMessageReceived(CustomerCRUDService.InformationRetrieved);
            EnsureCustomerMatchesView();
        }

        [TestMethod]
        public void LastRecordNotMatching()
        {
            _cv.Button = CustomerCRUDService.DelOp;
            _mock.CheckCustomer = true;
            new CustomerCRUDService(_cv, _cv.Number + 1, _mock).Process();
            Assert.AreEqual(CustomerCRUDService.ViewFirst, _cv.Message);
        }

        [TestMethod]
        public void DeleteSuccess()
        {
            _cv.Button = CustomerCRUDService.DelOp;
            _mock.CheckCustomer = true;
            EnsureMessageReceived(CustomerCRUDService.CustomerDeleted);
        }
        [TestMethod]
        public void ChangeOpForRecordAbsent()
        {
            _cv.Button = CustomerCRUDService.ChgOp;
            EnsureMessageReceived(CustomerCRUDService.CustomerNumberAbsent);
        }
        [TestMethod]
        public void ChangeOpSuccess()
        {
            _cv.Button = CustomerCRUDService.ChgOp;
            _mock.CheckCustomer = true;
            EnsureMessageReceived(CustomerCRUDService.InformationUpdated);
            EnsureCustomerMatchesView();
        }
        [TestMethod]
        public void AddOpFail()
        {
            _cv.Button = CustomerCRUDService.AddOp;
            _mock.CheckCustomer = true;
            EnsureMessageReceived(CustomerCRUDService.CustomerNumberPresent);
        }
        [TestMethod]
        public void AddOpSuccess()
        {
            _cv.Button = CustomerCRUDService.AddOp;
            EnsureMessageReceived(CustomerCRUDService.CustomerAdded);
        }
        private void EnsureCustomerMatchesView()
        {
            Assert.AreEqual(_cust.number, _cv.Number);
            Assert.AreEqual(_cust.name, _cv.Name);
            Assert.AreEqual(_cust.address, _cv.Address);
            Assert.AreEqual(_cust.mobile, _cv.Mobile);
        }
    }
}