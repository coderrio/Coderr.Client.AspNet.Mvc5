using System;
using System.Web.Mvc;
using System.Web.Routing;
using codeRR.Client.Uploaders;

namespace codeRR.Client.AspNet.Mvc5.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // replace with URI for your own account/coderrServer.
            var uri = new Uri("http://localhost:50473/");
            Err.Configuration.Credentials(uri,
                "5f219f356daa40b3b31dfc67514df6d6",
                "22612e4444f347d1bb3d841d64c9750a");


            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;
            Err.Configuration.CatchMvcExceptions();
            Err.Configuration.DisplayErrorPages();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        
    }
}
