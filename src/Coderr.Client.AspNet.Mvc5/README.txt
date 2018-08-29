ASP.NET MVC v5.0 integration package
====================================

This library is the client library of Coderr. 
It detects and reports different types of errors both in the ASP pipeline and in the MVC pipeline.

You will also need to install a Coderr Server to be able to analyze and manage the errors.

https://coderr.io/documentation/getting-started/


Configuration
=============

Start by configuring the connection to the Coderr server. 
The code below is typically added in your global.asax or Startup.cs. 
The configuration settings is found either in your chosen Coderr Server.

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // codeRR configuration
            var uri = new Uri("https://report.coderr.io/");
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

All unhandled exceptions are reported directly by the client library. 

When using the controller, use "this.ReportException(ex)" to get full functionality from Coderr. 
If you do not have access to the controller, you can use "httpContext.ReportException(ex)". 
"Err.Report(ex)" can be used as last resort as it do not include context information from ASP.NET.

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


ASP.NET MVC5 documentation
https://coderr.io/documentation/client/libraries/aspnet-mvc5/

Guides and support
https://coderr.io/guides-and-support/
