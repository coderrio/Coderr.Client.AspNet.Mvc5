ASP.NET MVC v5.0 integration package
====================================

Welcome to codeRR! 

This library is the client library of codeRR. CodeRR detects and reports different types of errors both in the ASP pipeline and in the MVC pipeline.

However, the library doesn’t process the generated information. Information processing is done by the codeRR server which you will need to install.

For a server with full functionality, we recommend you to use our hosted service at https://coderrapp.com/live. But you can also use and install the open source server version from https://github.com/coderrapp/coderr.server.

For any questions that you might have, please use our forum at http://discuss.coderrapp.com. At the forum, we will try to answer questions as fast as we can and post answers to questions that have already been asked. Don't hesitate to use it! 


Configuration
=============

Start by configuring the connection to the codeRR server. The code below is typically added in your global.asax or Startup.cs. The configuration settings is found either in codeRR Live or in your installed codeRR server.

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // codeRR configuration
            var uri = new Uri("https://report.coderrapp.com/");
            Err.Configuration.Credentials(uri,
                "yourAppKey",
                "yourSharedSecret");

            // catch unhandled exceptions
            Err.Configuration.CatchMvcExceptions();

            // the usual stuff
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }


Reporting exceptions
====================

All unhandled exceptions are reported directly by the client library. Here you have the option of using try/catch for custom error handling or to display pretty error messages.

When using the controller, use "this.ReportException(ex)" to get all the bells and whistles from codeRR attached to the exception. If you do not have access to the controller, you can use "httpContext.ReportException(ex)". "Err.Report(ex)" can be used as last resort as it do not include context information.

Sample code of how you can report an exception:

    public ActionResult Post(PostViewModel model)
    {
        try
        {
            _somService.Execute(model);
        }
        catch (Exception ex)
        {
            this.ReportException(ex, model);

            //some custom handling
        }

        return View();
    }

Again for questions, go to http://discuss.coderrapp.com
Additional documentation can be found at https://coderrapp.com/documentation/client/libraries/aspnet-mvc5/
