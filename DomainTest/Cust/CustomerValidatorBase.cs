using Data;
using Data.Tables;
using Domain.Cust;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTest.Cust
{
    public class CustomerValidatorBase
    {
        protected CView _cv = new CView();
        protected MockDb _mock = new MockDb();

        [TestInitialize]
        public void BaseSetUp()
        {
            _cv.Number = 12;
            _cv.Name = "abc";
            _cv.Address = "addr";
            _cv.Mobile = "123";
            _cv.Button = CustomerCRUDService.AddOp;
        }

        protected void EnsureMessageReceived(string mesg)
        {
            new CustomerCRUDService(_cv, _cv.Number, _mock).Process();
            Assert.AreEqual(mesg, _cv.Message);
        }
    }

    public class MockDb : ICustomerRepository
    {
        public bool CheckCustomer;
        public Customer LastCustomer { get; set; }

        public bool CheckCustomerNumber(long number)
        {
            return CheckCustomer;
        }

        public void DeleteCustomer()
        {
        }

        public void AddCustomer(Customer c)
        {
        }
    }
}