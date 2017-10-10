ASP.NET MVC v5.0 integration package
====================================

Welcome to codeRR! 

We try to answer questions as fast as we can at our forum: http://discuss.coderrapp.com. 
If you have any trouble at all, don't hesitate to post a message there.

This library is the client library of codeRR. What it does is to pick different kinds of
errors both in the ASP pipeline and in MVC pipeline.

However, this library do not process the information but require a codeRR server for that.
You can either install the open source server from https://github.com/coderrapp/coderr.server, or
use our hosted service at https://coderrapp.com/live.


Configuration
=============

To start with, you need to configure the connection to the codeRR server, 
this code is typically added in your Global.Asax or Startup.cs. This information is found either
in our hosted service or in your installed codeRR server.

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

All unhandled exceptions are reported directly by this library. However, sometimes you'll want to use try/catch
for some custom handling (or being able to display pretty error messages).

If you do so in the controller, use "this.ReportException(ex)" to get all the goodies from codeRR attached to the
exception.

You can also use "httpContext.ReportException(ex)" if you do not have access to the controller. Last resort
is to use "Err.Report(ex)" as no context information is included then.

When doing so, simply report the exception like this:

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

Questions? http://discuss.coderrapp.com
Documentation: https://coderrapp.com/documentation/client/libraries/aspnet/mvc5/
