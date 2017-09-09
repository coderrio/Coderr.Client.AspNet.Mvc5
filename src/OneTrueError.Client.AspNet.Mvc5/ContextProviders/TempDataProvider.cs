using OneTrueError.Client.ContextProviders;
using OneTrueError.Client.Contracts;
using OneTrueError.Client.Converters;
using OneTrueError.Client.Reporters;

namespace OneTrueError.Client.AspNet.Mvc5.ContextProviders
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