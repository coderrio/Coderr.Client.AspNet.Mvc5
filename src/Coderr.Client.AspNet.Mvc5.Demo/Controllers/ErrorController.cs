using System.Web.Mvc;
using codeRR.Client.AspNet.Mvc5;

namespace codeRR.Client.AspNet.Mvc5.Demo.Controllers
{
public class ErrorNoController : Controller
{
    public ActionResult Index(OneTrueViewModel model)
    {
        return View("Error", model);
    }

    public ActionResult NotFound(OneTrueViewModel model)
    {
        return View(model);
    }

    public ActionResult InternalServerError(OneTrueViewModel model)
    {
        return View(model);
    }

}
}