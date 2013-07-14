using System.Data.Entity.Infrastructure;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.CustomerTableCheck
{
    [TestClass]
    public class IndexTest : CustomerBase
    {
        private long _lastId;
        [TestInitialize]
        public void SetupLastId()
        {
            _lastId = GetLastCustomerId();            
        }
        [TestCleanup]
        public void DeleteRecordsCreated()
        {
            DeleteRecordsAfter(_lastId);
        }

        [TestMethod]
        public void CheckPrimaryKeyIncrementedForAdd()
        {
            DbCtx.Customers.Add(CreateNewCustomer());
            DbCtx.SaveChanges();
            Assert.IsTrue(GetLastCustomerId() > _lastId);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void CheckThatNumberUnique()
        {
            var c = CreateNewCustomer();
            var duplicateCustomer = new Customer()
            {
                number = c.number,
                name = Name1 + "a",
                address = Address1,
                mobile = Mobile1
            };
            DbCtx.Customers.Add(c);
            DbCtx.Customers.Add(duplicateCustomer);
            DbCtx.SaveChanges();
        }
    }
}