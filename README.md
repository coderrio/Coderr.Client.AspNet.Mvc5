Integration library for ASP.NET MVC5 applications
==========================

This library will detect all unhandled exceptions in ASP.NET MVC5 applications and report them to your OneTrueError server (or your account at https://onetrueerror.com).

To report exceptions manually in your controller, use `this.ReportError(exception)` to allow OneTrueError to include RouteData, ViewBag, TempData etc when your exception is reported.

# Context collections

This library includes the following context collections for every reported exceptions:

* All in the [core library](https://github.com/onetrueerror/onetrueerror.client)
* All in the [asp.net library](https://github.com/onetrueerror/onetrueerror.client.aspnet)
* Action parameters
* Controller information
* ModelState
* Query stirng
* RouteData
* Session data
* TempData
* Uploaded files
* ViewBag

# Getting started

1. Download and install the [OneTrueError server](https://github.com/onetrueerror/onetrueerror.server) or create an account at [OneTrueError.com](https://onetrueerror.com)
2. Install this client library (using nuget `onetrueerror.client.aspnet.mvc5`)
3. Configure the credentials from your OneTrueError account in your `Startup.cs` or `Global.asax`
4. Add `OneTrue.Configuration.CatchMvcExceptions()` in your `Startup.cs` or `Global.asax`

Done.