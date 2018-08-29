using Coderr.Client.ContextProviders;
using Coderr.Client.Contracts;
using Coderr.Client.Converters;
using Coderr.Client.Reporters;

namespace Coderr.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Name: "TempData"
    /// </summary>
    public class TempDataProvider : IContextInfoProvider
    {
        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetMvcContext;
            if (aspNetContext?.TempData == null || aspNetContext.TempData.Count == 0)
                return null;

            var converter = new ObjectToContextCollectionConverter();
            return converter.Convert(Name, aspNetContext.TempData);
        }

        /// <summary>TempData</summary>
        public string Name => "TempData";
    }
}