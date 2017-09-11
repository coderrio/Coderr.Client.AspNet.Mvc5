using System.Collections.Generic;
using OneTrueError.Client.ContextProviders;
using OneTrueError.Client.Contracts;
using OneTrueError.Client.Converters;
using OneTrueError.Client.Reporters;

namespace OneTrueError.Client.AspNet.Mvc5.ContextProviders
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