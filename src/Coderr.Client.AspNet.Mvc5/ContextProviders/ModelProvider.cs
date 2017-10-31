using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Returns the "ViewModel" collection
    /// </summary>
    public class ModelProvider : IContextInfoProvider
    {
        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetMvcContext;
            return aspNetContext?.Model?.ToContextCollection(Name);
        }

        /// <summary>
        ///     "ViewModel"
        /// </summary>
        public string Name => "ViewModel";
    }
}