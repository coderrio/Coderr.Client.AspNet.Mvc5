using System.Collections.Specialized;
using System.Web;
using OneTrueError.Client.ContextProviders;
using OneTrueError.Client.Contracts;
using OneTrueError.Client.Reporters;

namespace OneTrueError.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     assembles all HTTP headers from the request.
    /// </summary>
    /// <remarks>They will be added to a collection called "HttpHeaders".</remarks>
    public class HttpHeadersProvider : IContextInfoProvider
    {
        /// <summary>Collect information</summary>
        /// <param name="context">Context information provided by the class which reported the error.</param>
        /// <returns>Collection. Items with multiple values are joined using <c>";;"</c></returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetContext;
            if (aspNetContext?.HttpContext == null)
                return null;

            var myHeaders =
                new NameValueCollection(aspNetContext.HttpContext.Request.Headers)
                {
                    ["Url"] = aspNetContext.HttpContext.Request.Url?.ToString()
                };
            return new ContextCollectionDTO("HttpHeaders", myHeaders);
        }

        /// <summary>"HttpHeaders"</summary>
        public string Name => "HttpHeaders";
    }
}