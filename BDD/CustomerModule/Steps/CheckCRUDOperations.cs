using System.Linq;
using BDD.Common;
using Data;
using Data.Common;
using Domain;
using Domain.Cust;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using TechTalk.SpecFlow;

namespace BDD.CustomerModule.Steps
{
    [Binding]
    public class CheckCRUDOperations
    {
        private readonly IKernel _r = Kernel.GetRegistry();
        private readonly CView _model = new CView();
        private long _customerNumberToBeDeleted;
        private CustomerCRUDService _cv;

        [Given(@"A non-existant Customer Number (.*)")]
        public void GivenANon_ExistantCustomerNumber(long number)
        {
            ViewCustomer(number);
            Assert.AreEqual(CustomerCRUDService.CustomerNumberAbsent, _model.Message);
        }

        [When(@"I try to create a new Customer with number (.*), name '(.*)', address '(.*)' and Mobile (.*)")]
        public void WhenITryToCreateANewCustomerWithNumberNameAddressAndMobile(long number, string name, string address,
                                                                               string mobile)
        {
            ForEveryScenario.AtEndExecute(DeleteCustomer);
            _customerNumberToBeDeleted = number;
            _model.Button = "Add";
            _model.Number = number;
            _model.Name = name;
            _model.Address = address;
            _model.Mobile = mobile;
            _cv = _r.Get<CustomerCRUDServiceBuilder>().GetInstance(_model, number - 1);
            _r.Get<IUnitOfWork>().Perform(_cv.Process);
        }

        [Then(@"the Customer should be added successfully")]
        public void ThenTheCustomerShouldBeAddedSuccessfully()
        {
            Assert.AreEqual(CustomerCRUDService.CustomerAdded, _model.Message);
        }
        [Then(@"I should be able to View the same customer")]
        public void ThenIShouldBeAbleToViewTheSameCustomer()
        {
            ViewCustomer(_model.Number);
            Assert.AreEqual(CustomerCRUDService.InformationRetrieved, _model.Message);
        }


        private void ViewCustomer(long number)
        {
            _model.Button = "View";
            _model.Number = number;
            _cv = _r.Get<CustomerCRUDServiceBuilder>().GetInstance(_model, number - 1);
            _r.Get<IUnitOfWork>().Perform(_cv.Process);
        }

        private void DeleteCustomer()
        {
            if (_customerNumberToBeDeleted == 0) return;
            var uow = _r.Get<IUnitOfWork>();
            uow.Perform(() => DelCustomer(uow));
        }

        private void DelCustomer(IUnitOfWork uow)
        {
            Customer cb = uow.Current.Customers.First(cu => cu.number == _customerNumberToBeDeleted);
            uow.Current.Customers.Remove(cb);
        }
    }
}