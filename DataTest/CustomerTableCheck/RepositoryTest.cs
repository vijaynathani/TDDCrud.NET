using System;
using System.Linq;
using Data;
using Data.Common;
using Data.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.CustomerTableCheck
{
    [TestClass]
    public class RepositoryTest : CustomerBase
    {
        private readonly Mock _mock = new Mock();
        private long _lastId = long.MaxValue;
        private CustomerRepository _sut;

        [TestInitialize]
        public void SetUp()
        {
            _mock.c = DbCtx;
            _sut = new CustomerRepository(_mock);
        }

        [TestMethod]
        public void AddRecord()
        {
            Customer lastCustomer = GetLastCustomer();
            _lastId = lastCustomer.id;
            Customer c = CreateNewCustomer();
            _sut.AddCustomer(c);
            DbCtx.SaveChanges();
            DbCtx = new Entities2();
            Customer newLast = GetLastCustomer();
            Assert.IsFalse(IsCustomerContentSame(lastCustomer, newLast));
            Assert.IsTrue(IsCustomerContentSame(c, newLast));
        }

        [TestMethod]
        public void CheckSelectByNumber()
        {
            var c1 = GetLastCustomer();
            Assert.IsTrue(_sut.CheckCustomerNumber(c1.number));
            Assert.IsTrue(IsCustomerContentSame(c1, _sut.LastCustomer));
        }

        [TestCleanup]
        public void TearDown()
        {
            DeleteRecordsAfter(_lastId);
        }

        public static bool IsCustomerContentSame(Customer a, Customer b)
        {
            return a.number == b.number &&
                   a.name == b.name &&
                   a.address == b.address &&
                   a.mobile == b.mobile;
        }

        private Customer GetLastCustomer()
        {
            long id1 = GetLastCustomerId();
            return (from cu in DbCtx.Customers where cu.id == id1 select cu)
                .Single<Customer>();
        }

        private class Mock : IUnitOfWork
        {
            public Entities2 c;

            public Entities2 Current
            {
                get { return c; }
            }

            public void Perform(Action execute)
            {
            }
        }
    }
}