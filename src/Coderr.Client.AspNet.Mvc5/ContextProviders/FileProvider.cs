using System.Collections.Generic;
using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Attaches information about files uploaded in the request
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Provides filename (property name), content type and file size (combined as property value)
    ///     </para>
    /// </remarks>
    public class FileProvider : IContextInfoProvider
    {
        /// <summary>Collect information</summary>
        /// <param name="context">Context information provided by the class which reported the error.</param>
        /// <returns>Collection. Items with multiple values are joined using <c>";;"</c></returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspContext = context as AspNetContext;
            if (aspContext == null || aspContext.HttpContext.Request.Files.Count == 0)
                return null;

            var files = new Dictionary<string, string>();
            for (var i = 0; i < aspContext.HttpContext.Request.Files.Count; i++)
            {
                var file = aspContext.HttpContext.Request.Files.Get(i);
                files[file.FileName] = string.Format(file.ContentType + ";length=" + file.ContentLength);
            }

            return new ContextCollectionDTO("HttpRequestFiles", files);
        }

        /// <summary>
        ///     "HttpRequestFiles"
        /// </summary>
        public string Name => "HttpRequestFiles";
    }
}