using System.Collections.Specialized;
using Newtonsoft.Json;
using Coderr.Client.ContextProviders;
using Coderr.Client.Contracts;
using Coderr.Client.Reporters;

namespace Coderr.Client.AspNet.Mvc5.ContextProviders
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
                return null;

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
            if (items.Count == 0)
                return null;

            return new ContextCollectionDTO("HttpSession", items);
        }

        /// <summary>
        ///     "HttpSession"
        /// </summary>
        public string Name => "HttpSession";
    }
}