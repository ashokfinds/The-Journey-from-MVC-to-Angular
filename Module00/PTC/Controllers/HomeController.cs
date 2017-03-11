using System.Collections.Generic;
using System.Web.Mvc;

namespace PTC.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      PTCViewModel vm = new PTCViewModel();

      vm.HandleRequest();

      return View(vm);
    }
    
    [HttpPost]
    public ActionResult Index(PTCViewModel vm)
    {
      vm.HandleRequest();

      // If everything is OK, update the model state on the page
      if (vm.IsValid) {
        ModelState.Clear();
      }
      else {
        ModelState.Merge(vm.Messages);
      }

      return View(vm);
    }
  }
}