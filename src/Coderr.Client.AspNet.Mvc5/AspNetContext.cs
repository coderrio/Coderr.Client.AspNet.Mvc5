using System;
using System.Collections.Generic;
using System.Web;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5
{
    /// <summary>
    ///     Reporter context with information about ASP.NET.
    /// </summary>
    public class AspNetContext : IErrorReporterContext2
    {
        /// <summary>
        ///     Creates a new instance of <see cref="AspNetContext" />.
        /// </summary>
        /// <param name="reporter">object triggering the collection</param>
        /// <param name="exception">caught exception</param>
        /// <param name="httpContext">context that the exception was thrown on</param>
        /// <exception cref="ArgumentNullException">reporter;exception</exception>
        public AspNetContext(object reporter, Exception exception, HttpContextBase httpContext)
        {
            if (reporter == null) throw new ArgumentNullException("reporter");
            if (exception == null) throw new ArgumentNullException("exception");
            ContextCollections = new List<ContextCollectionDTO>();
            Exception = exception;
            HttpContext = httpContext;
            Reporter = reporter;
        }

        /// <summary>
        ///     The ASP.NET application
        /// </summary>
        public HttpApplication HttpApplication { get; set; }

        /// <summary>
        ///     Http context for the request that the exception occurred for.
        /// </summary>
        public HttpContextBase HttpContext { get; private set; }

        /// <inheritdoc />
        public Exception Exception { get; private set; }

        /// <inheritdoc />
        public object Reporter { get; private set; }

        /// <inheritdoc />
        public IList<ContextCollectionDTO> ContextCollections { get; }
    }
}