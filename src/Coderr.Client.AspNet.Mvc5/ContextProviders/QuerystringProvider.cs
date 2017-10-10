using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Adds a HTTP request query string collection.
    /// </summary>
    /// <remarks>The name of the collection is "HttpQueryString"</remarks>
    public class QueryStringProvider : IContextInfoProvider
    {
        /// <summary>
        ///     Collect information
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Collection</returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetContext;
            if (aspNetContext == null || aspNetContext.HttpContext.Request.QueryString.Count == 0)
                return null;

            return new ContextCollectionDTO("HttpQueryString", aspNetContext.HttpContext.Request.QueryString);
        }

        /// <summary>
        ///     "HttpQueryString"
        /// </summary>
        public string Name
        {
            get { return "HttpQueryString"; }
        }
    }
}