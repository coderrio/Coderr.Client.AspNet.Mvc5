using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Converters;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Name: "ViewBag"
    /// </summary>
    public class ViewBagProvider : IContextInfoProvider
    {
        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetMvcContext;
            if (aspNetContext?.ViewBag == null)
                return null;

            var converter = new ObjectToContextCollectionConverter();
            var collection = converter.Convert(Name, aspNetContext.ViewBag);

            //not beatiful, but we do not want to reflect the object twice
            return collection.Properties.Count == 0 ? null : collection;
        }

        /// <summary>ViewBag</summary>
        public string Name => "ViewBag";
    }
}