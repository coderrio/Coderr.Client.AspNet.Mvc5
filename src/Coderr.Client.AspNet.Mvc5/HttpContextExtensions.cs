using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeRR.Client.AspNet.Mvc5.Handlers;
using codeRR.Client.Contracts;
using codeRR.Client.Converters;

namespace codeRR.Client.AspNet.Mvc5
{
    /// <summary>
    /// Extensions for the HTTP context
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="httpContext">httpContext used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <param name="contextData">extra collections</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this HttpContextBase httpContext, Exception exception,
            IEnumerable<ContextCollectionDTO> contextData)
        {
            return ErrorHttpModule.ExecutePipeline(httpContext, exception, httpContext, contextData.ToArray());
        }

        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="httpContext">httpContext used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <param name="contextData">extra context data</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this HttpContextBase httpContext, Exception exception,
            object contextData)
        {
            var converter = new ObjectToContextCollectionConverter();
            var collection = converter.Convert(contextData);
            return ErrorHttpModule.ExecutePipeline(httpContext, exception, httpContext, collection);
        }

        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="httpContext">httpContext used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this HttpContextBase httpContext, Exception exception)
        {
            return ErrorHttpModule.ExecutePipeline(httpContext, exception, httpContext);
        }
    }
}
