using Coderr.Client.ContextProviders;
using Coderr.Client.Contracts;
using Coderr.Client.Reporters;

namespace Coderr.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Adds a HTTP request form collection.
    /// </summary>
    /// <remarks>The name of the collection is "HttpForm"</remarks>
    public class FormProvider : IContextInfoProvider
    {
        /// <summary>
        ///     Collect information
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Collection</returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspContext = context as AspNetContext;
            if (aspContext == null || aspContext.HttpContext.Request.Form.Count == 0)
                return null;

            return new ContextCollectionDTO("HttpForm", aspContext.HttpContext.Request.Form);
        }

        /// <summary>
        ///     "HttpForm"
        /// </summary>
        public string Name => "HttpForm";
    }
}