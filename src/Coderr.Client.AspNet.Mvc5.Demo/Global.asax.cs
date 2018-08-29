using System;
using System.Web.Mvc;
using System.Web.Routing;
using Coderr.Client.Uploaders;

namespace Coderr.Client.AspNet.Mvc5.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // replace with URI for your own account/coderrServer.
            var uri = new Uri("http://localhost:50473/");
            Err.Configuration.Credentials(uri,
                "2b42024bcb21499daed400e13b6338f3",
                "237e6bb0345f45b281a35c5b8a8e5bdb");

            Err.Configuration.UserInteraction.AskUserForDetails = false;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.CatchMvcExceptions();
            Err.Configuration.DisplayErrorPages();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        
    }
}
