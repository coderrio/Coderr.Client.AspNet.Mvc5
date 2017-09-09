using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using OneTrueError.Client.ContextProviders;
using OneTrueError.Client.Contracts;
using OneTrueError.Client.Reporters;

namespace OneTrueError.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Adds a HTTP request query string collection.
    /// </summary>
    /// <remarks>
    ///     The name of the collection is "HttpSession".
    ///     <para>Session objects are serialized as JSON, strings are added as-is.</para>
    /// </remarks>
    public class SessionProvider : IContextInfoProvider
    {
        /// <summary>
        ///     Collect information
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Collection</returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetContext;
            if (aspNetContext?.HttpContext.Session == null)
                return new ContextCollectionDTO("HttpSession", new NameValueCollection());

            var items = new NameValueCollection();
            foreach (string key in aspNetContext.HttpContext.Session)
            {
                var item = aspNetContext.HttpContext.Session[key];
                if (item == null)
                {
                    items.Add(key, "null");
                    continue;
                }

                if (item is string || item.GetType().IsPrimitive)
                {
                    items.Add(key, item.ToString());
                }
                else
                {
                    var json = JsonConvert.SerializeObject(item);
                    items.Add(key, json);
                }
            }

            return new ContextCollectionDTO("HttpSession", items);
        }

        /// <summary>
        ///     "HttpSession"
        /// </summary>
        public string Name => "HttpSession";
    }
}