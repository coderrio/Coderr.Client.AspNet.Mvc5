using System;

namespace Coderr.Client.AspNet.Mvc5
{
    /// <summary>
    ///     View model for all error pages used by this plugin.
    /// </summary>
    public class CoderrViewModel
    {
        /// <summary>
        ///     codeRR error id
        /// </summary>
        public string ReportId { get; set; }

        /// <summary>
        ///     Caught exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     Http status code. Typically 404 or 500
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        ///     Name of HTTP Status Code, like "InternalServerError"
        /// </summary>
        public string HttpStatusCodeName { get; set; }

        /// <summary>
        ///     Let the user decide if reports can be uploaded or not.
        /// </summary>
        public bool? UserAllowedReporting { get; set; }

        /// <summary>
        ///     Let the user describe what they did when the error happened.
        /// </summary>
        public string UserErrorDescription { get; set; }

        /// <summary>
        ///     Let the user enter their email address to receive status updates.
        /// </summary>
        public string UserEmailAddress { get; set; }
    }
}