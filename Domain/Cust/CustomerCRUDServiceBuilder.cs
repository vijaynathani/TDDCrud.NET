using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Tables;

namespace Domain.Cust
{
    public class CustomerCRUDServiceBuilder
    {
        private readonly ICustomerRepository dbOps;
        public CustomerCRUDServiceBuilder(ICustomerRepository dbOps)
        {
            this.dbOps = dbOps;
        }
        public CustomerCRUDService GetInstance(CView c, long oldNumber)
        {
                return new CustomerCRUDService(c, oldNumber, dbOps);
        }
    }
}
