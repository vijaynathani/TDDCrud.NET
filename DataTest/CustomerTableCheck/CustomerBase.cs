using System;
using System.Linq;
using Data;
using DataTest.Common;

namespace DataTest.CustomerTableCheck
{
    public class CustomerBase : DbBaseTest
    {
        protected const String Name1 = "n1", Address1 = "a1", Mobile1 = "12";

        protected long GetLastCustomerId()
        {
            return (from cu in DbCtx.Customers select cu.id).Max();
        }
        protected Customer CreateNewCustomer()
        {
            return new Customer
            {
                number = GetUniqueNumber(),
                name = Name1,
                address = Address1,
                mobile = Mobile1
            };
        }

        private long GetUniqueNumber()
        {
            return (from cu in DbCtx.Customers select cu.number).Max() + 1;
        }
        protected void DeleteRecordsAfter(long id)
        {
            DbCtx.Database.ExecuteSqlCommand("delete from Customers where id > {0}", id);
        }
    }
}
