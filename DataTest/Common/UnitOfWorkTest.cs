using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Common;
using DataTest.CustomerTableCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.Common
{
    [TestClass]
    public class UnitOfWorkTest : CustomerBase
    {
        private const int ParallelCount = 5;
        private readonly UnitOfWork _sut = new UnitOfWork();
        private long _lastId;

        [TestInitialize]
        public void SetUp()
        {
            _lastId = GetLastCustomerId();
        }

        [TestMethod]
        public void VerifyUpdateOccurs()
        {
            _sut.Perform(AddRecord);
            Assert.AreNotEqual(GetLastCustomerId(), _lastId);
        }

        [TestMethod]
        public void VerifyNoUpdateForExecption()
        {
            try
            {
                AddAndAbort();
            }
            catch (Exception ignore)
            {
            }
            Assert.AreEqual(GetLastCustomerId(), _lastId);
        }

        [TestMethod]
        public void ParallelUpdateTest()
        {
            long originalCount = NumberOfCustomers();
            Parallel.ForEach(CreateCustomerRecords(), AddThisRecord);
            Assert.AreEqual(originalCount + ParallelCount, NumberOfCustomers());
        }

        private long NumberOfCustomers()
        {
            return (from c in DbCtx.Customers select c).Count();
        }

        private IEnumerable<Customer> CreateCustomerRecords()
        {
            var c = CreateNewCustomer();
            var customers = new List<Customer>();
            for (var i = 0; i < ParallelCount; ++i)
            {
                var n = CreateNewCustomer();
                n.number = c.number + i;
                customers.Add(n);
            }
            return customers;
        }

        [TestCleanup]
        public void TearDown()
        {
            DeleteRecordsAfter(_lastId);
        }

        private void AddAndAbort()
        {
            _sut.Perform(() =>
                {
                    AddRecord();
                    throw new Exception("Abort");
                });
        }

        private void AddThisRecord(Customer c)
        {
            Console.WriteLine("adding {0}", c);
            _sut.Perform(() => _sut.Current.Customers.Add(c));
        }

        private void AddRecord()
        {
            _sut.Current.Customers.Add(CreateNewCustomer());
        }
    }
}