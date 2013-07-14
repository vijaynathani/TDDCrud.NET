using System.Linq;
using Data.Common;

namespace Data.Tables
{
    public class CustomerRepository : ICustomerRepository
    {
        private Customer _c;
        private readonly IUnitOfWork _uow;
        public CustomerRepository(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        private Entities2 ContextForDb
        {
            get { return _uow.Current; }
        }

        public Customer LastCustomer
        {
            get { return _c; }
        }

        public bool CheckCustomerNumber(long number)
        {
             _c = (from cu in ContextForDb.Customers where cu.number == number select cu).SingleOrDefault();
            return _c != null;
        }

        public void DeleteCustomer()
        {
            ContextForDb.Customers.Remove(_c);
        }

        public void AddCustomer(Customer c)
        {
            ContextForDb.Customers.Add(c);
        }
    }
}
