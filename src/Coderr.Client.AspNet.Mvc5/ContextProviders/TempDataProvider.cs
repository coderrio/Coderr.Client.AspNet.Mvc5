using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Converters;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
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