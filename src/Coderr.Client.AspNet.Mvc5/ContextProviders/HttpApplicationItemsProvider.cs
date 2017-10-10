using System.Collections.Generic;
using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Name: "HttpContext.Application"
    /// </summary>
    public class HttpApplicationItemsProvider : IContextInfoProvider
    {
        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetContext;
            if (aspNetContext?.HttpContext == null)
                return null;

            if (aspNetContext.HttpContext.Application.Count == 0)
                return null;

            var dict = new Dictionary<string,string>();
            for (var i = 0; i < aspNetContext.HttpContext.Application.Count; i++)
            {
                var value = aspNetContext.HttpContext.Application[i];
                var key = aspNetContext.HttpContext.Application.GetKey(i);
                dict[key] = value?.ToString() ?? "null";
            }
            return new ContextCollectionDTO(Name, dict);
        }

        /// <summary>
        /// "HttpContext.Application"
        /// </summary>
        public string Name => "HttpContext.Application";
    }
}