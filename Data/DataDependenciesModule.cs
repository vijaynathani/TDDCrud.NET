using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common;
using Data.Tables;
using Ninject.Modules;

namespace Data
{
    public class DataDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerRepository>().To<CustomerRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
