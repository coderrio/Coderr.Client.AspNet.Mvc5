using System.Collections.Generic;
using System.Web.Mvc;

namespace Coderr.Client.AspNet.Mvc5
{
    /// <summary>
    ///     Validates model when it arrives and adds exceptions as validation failures.
    /// </summary>
    public class CoderrValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CoderrValidationAttribute" />.
        /// </summary>
        public CoderrValidationAttribute()
        {
            DisplayExceptionAsValidationFailure = true;
        }

        /// <summary>
        ///     Display the exception message as a validation failure in the view (if any).
        /// </summary>
        public bool DisplayExceptionAsValidationFailure { get; set; }

        ///<inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("", filterContext.Exception.Message);
                filterContext.ExceptionHandled = true;

                var model = filterContext.Controller.ViewData.Model
                            ?? filterContext.HttpContext.Items["AttachedCoderrModel"];
                if (model is IDictionary<string, object>)
                {
                    model = model.ToContextCollection("Model");
                }
                if (model != null)
                    Err.Report(filterContext.Exception, model);
                else
                    Err.Report(filterContext.Exception);

                filterContext.Result = CreateView(filterContext);
                return;
            }
            base.OnActionExecuted(filterContext);
        }

        ///<inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items["AttachedCoderrModel"] = filterContext.ActionParameters;
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.Result = CreateView(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private static ViewResult CreateView(ControllerContext filterContext)
        {
            return new ViewResult
            {
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData,
                ViewName = filterContext.RouteData.Values["action"].ToString()
            };
        }
    }
}