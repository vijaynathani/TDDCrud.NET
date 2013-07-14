using System.Web.Mvc;
using Data.Common;
using Domain;
using Domain.Cust;
using Ninject;

namespace UI.Controllers
{
    public class CustomerController : Controller
    {
        private const string LastNumberDisplayed = "Customer/Crud";
        private readonly IKernel _r = Kernel.GetRegistry();
        //
        // GET: /Customer/
        [HttpGet]
        public ActionResult Crud()
        {
            Session[LastNumberDisplayed] = 0L;
            return View(CView.BlankView());
        }

        [HttpPost]
        public ActionResult Crud(CView model)
        {
            var oldNumber = (long) (Session[LastNumberDisplayed] ?? 0);
            CustomerCRUDService cv = _r.Get<CustomerCRUDServiceBuilder>().GetInstance(model, oldNumber);
            _r.Get<IUnitOfWork>().Perform(cv.Process);
            //UnitOfWork.Instance.Perform(() => new CustomerCRUDService(model, oldNumber, new CustomerRepository(UnitOfWork.Instance)).Process());
            Session[LastNumberDisplayed] = model.Number;
            return View(model);
        }
    }
}