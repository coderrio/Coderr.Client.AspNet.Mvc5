using System;
using System.IO;
using System.Web.Mvc;
using codeRR.Client.Contracts;

namespace codeRR.Client.AspNet.Mvc5.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimulatedFailure()
        {
            ViewBag.Title = "Hello";
            ViewBag.Model = new
            {
                state = "Running",
                Collected = true
            };

            TempData["DemoKey"] = new
            {
                Amount = 20000,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            throw new UnauthorizedAccessException("Check the codeRR website to see this exception");
        }

        public ActionResult Return()
        {
            throw new InvalidDataException("Unhandled data ex!");
        }
    }
}