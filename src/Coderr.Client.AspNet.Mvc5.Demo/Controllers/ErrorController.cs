using System.Web.Mvc;
using Coderr.Client.AspNet.Mvc5;

namespace Coderr.Client.AspNet.Mvc5.Demo.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(CoderrViewModel model)
        {
            return View("Error", model);
        }

        public ActionResult NotFound(CoderrViewModel model)
        {
            return View(model);
        }

        public ActionResult InternalServerError(CoderrViewModel model)
        {
            return View(model);
        }

    }
}