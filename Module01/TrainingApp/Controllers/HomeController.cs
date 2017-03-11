using System.Web.Mvc;
using TrainApp.Domain;
using TrainingApp.ViewModels;

namespace TrainingApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var vm = new ProductsViewModel();
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(ProductsViewModel vm)
        {
            vm.IsEntityValid = ModelState.IsValid;
            vm.HandleRequest();
            if (vm.IsEntityValid) {
                ModelState.Clear();
            } else {
                foreach(var error in vm.ValidationErrors) {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            return View(vm);
        }
    }
}