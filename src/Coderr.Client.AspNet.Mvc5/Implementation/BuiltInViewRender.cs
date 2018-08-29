using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;
using Coderr.Client;

namespace Coderr.Client.AspNet.Mvc5.Implementation
{
    /// <summary>
    ///     Renders embedded views.
    /// </summary>
    public class BuiltInViewRender
    {
        private static bool CanSubmit
        {
            get
            {
                return Err.Configuration.UserInteraction.AskUserForDetails ||
                       Err.Configuration.UserInteraction.AskUserForPermission ||
                       Err.Configuration.UserInteraction.AskForEmailAddress;
            }
        }

        /// <summary>
        ///     Build an ASPX or HTML file to be used as our error page.
        /// </summary>
        /// <param name="context">Context for codeRR</param>
        /// <returns>Complete string</returns>
        public static void Render(HttpErrorReporterContext context)
        {
            if (context.HttpContext.Request.Url == null)
                return;

            var url = new Uri(string.Format("{0}://{1}{2}",
                context.HttpContext.Request.Url.Scheme ?? "https",
                context.HttpContext.Request.Url.Authority,
                context.HttpContext.Request.Url.AbsolutePath));

            var virtualPathOrCompleteErrorPageHtml = LoadDefaultErrorPage(context.HttpStatusCodeName);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            if (virtualPathOrCompleteErrorPageHtml.EndsWith(".aspx"))
            {
                var request = new HttpRequest(null, url.ToString(), "");
                var response = new HttpResponse(sw);
                var httpContext = new HttpContext(request, response);
                httpContext.Items["ErrorReportContext"] = context;
                httpContext.Items["Exception"] = context.Exception;

                var pageType = BuildManager.GetCompiledType(virtualPathOrCompleteErrorPageHtml);
                var pageObj = Activator.CreateInstance(pageType);


                var executeMethod = pageType.GetMethod("Execute", new Type[0]);

                // support for web pages in the future?
                if (executeMethod != null && false)
                {
                    executeMethod.Invoke(pageObj, new object[0]);
                }
                var page = (Page)pageObj;
                page.ProcessRequest(httpContext);
            }
            else if (virtualPathOrCompleteErrorPageHtml.EndsWith(".html"))
            {
                //VirtualPathUtility.ToAbsolute is really required if you do not want ASP.NET to complain about the path.
                using (
                    var stream =
                        VirtualPathProvider.OpenFile(
                            VirtualPathUtility.ToAbsolute(virtualPathOrCompleteErrorPageHtml)))
                {
                    stream.CopyTo(context.HttpContext.Response.OutputStream);
                }
            }
            else if (virtualPathOrCompleteErrorPageHtml.StartsWith("<"))
            {
                var page = virtualPathOrCompleteErrorPageHtml;
                page = page.Replace("$reportId$", context.ErrorId)
                    .Replace("$URL$", VirtualPathUtility.ToAbsolute("~/coderr/Submit/"));
                page = page.Replace("$ButtonText$", CanSubmit ? "Send and proceed" : "Back to homepage.");

                // Should not ask for permission, but we want to get email or details.
                if (!Err.Configuration.UserInteraction.AskUserForPermission &&
                    (Err.Configuration.UserInteraction.AskForEmailAddress ||
                     Err.Configuration.UserInteraction.AskUserForDetails))
                {
                    page = page.Replace("$AllSendReport$", "checked");
                }
                else
                {
                    page = page.Replace("$AllSendReport$", "");
                }

                if (context.HttpContext.Request.AcceptTypes != null)
                {
                    var htmlIndex = GetAcceptTypeIndex(context.HttpContext, "text/html");
                    var jsonIndex = GetAcceptTypeIndex(context.HttpContext, "/json");
                    var xmlIndex = GetAcceptTypeIndex(context.HttpContext, "application/xml");
                    if (jsonIndex < htmlIndex && jsonIndex < xmlIndex)
                    {
                        page =
                            string.Format(
                                @"{{""error"": {{ ""msg"": ""{0}"", ""reportId"": ""{1}""}}, hint: ""Use the report id when contacting us if you need further assistance."" }}",
                                context.Exception.Message, context.ErrorId);
                        context.HttpContext.Response.ContentType = "application/json";
                    }
                    else if (xmlIndex < jsonIndex && xmlIndex < htmlIndex)
                    {
                        page =
                            string.Format(
                                @"<Error ReportId=""{0}"" hint=""Use the report id when contacting us if you need further assistance"">{1}</Error>",
                                context.ErrorId, context.Exception.Message);
                        context.HttpContext.Response.ContentType = "application/xml";
                    }
                }
                context.HttpContext.Response.Write(page);
            }
            else
            {
                context.HttpContext.Response.Write(
                    string.Format("Unsupported virtual uri: {0}, must be a .aspx (Page) or a .html",
                        virtualPathOrCompleteErrorPageHtml));
            }
        }

        private static int GetAcceptTypeIndex(HttpContextBase app, string headerName)
        {
            if (app.Request.AcceptTypes == null)
                return int.MaxValue;

            var htmlIndex = 0;
            foreach (var type in app.Request.AcceptTypes)
            {
                if (type.Contains(headerName))
                    return htmlIndex;

                ++htmlIndex;
            }

            return int.MaxValue;
        }

        private static string LoadDefaultErrorPage(string httpCodeName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var fileNameSpace = typeof(AspNetMvcContext).Namespace + ".Views";
            var file = asm.GetManifestResourceStream(string.Format("{0}.{1}.html", fileNameSpace, httpCodeName))
                       ?? asm.GetManifestResourceStream(string.Format("{0}.Error.html", fileNameSpace));

            using (file)
            {
                var styles = "";
                if (!Err.Configuration.UserInteraction.AskUserForPermission)
                {
                    styles += ".AllowSubmissionStyle { display: none; }\r\n";
                }
                if (!Err.Configuration.UserInteraction.AskUserForDetails)
                {
                    styles += ".AllowFeedbackStyle { display: none; }\r\n";
                }
                if (!Err.Configuration.UserInteraction.AskForEmailAddress)
                {
                    styles += ".AskForEmailAddress { display: none; }\r\n";
                }


                var reader = new StreamReader(file);
                var txt = reader.ReadToEnd();
                txt = txt
                    .Replace("/*CssStyles*/", styles);


                return txt;
            }
        }
    }
}