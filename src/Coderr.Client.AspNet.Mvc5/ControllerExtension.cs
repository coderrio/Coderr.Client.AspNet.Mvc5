using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Coderr.Client.AspNet.Mvc5.Handlers;
using Coderr.Client.Contracts;
using Coderr.Client.Converters;

// ReSharper disable CheckNamespace

namespace Coderr.Client
{
    /// <summary>
    ///     To be able to pickup ASP.NET MVC context collections
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="controller">controller used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <param name="contextData">extra collections</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this ControllerBase controller, Exception exception,
            IEnumerable<ContextCollectionDTO> contextData)
        {
            return CoderrFilter.Invoke(controller, controller.ControllerContext, exception, contextData);
        }

        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="controller">controller used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <param name="contextData">extra context data</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this ControllerBase controller, Exception exception,
            object contextData)
        {
            var converter = new ObjectToContextCollectionConverter();
            var collection = converter.Convert(contextData);
            return CoderrFilter.Invoke(controller, controller.ControllerContext, exception, new[] {collection});
        }

        /// <summary>
        ///     Report exception through codeRR
        /// </summary>
        /// <param name="controller">controller used to report exception (used to be able to collect context data)</param>
        /// <param name="exception">exception to report</param>
        /// <returns>sent report (can be used for instance for <c>Err.LeaveFeedback</c>)</returns>
        public static ErrorReportDTO ReportException(this ControllerBase controller, Exception exception)
        {
            return CoderrFilter.Invoke(controller, controller.ControllerContext, exception,
                new ContextCollectionDTO[0]);
        }
    }
}