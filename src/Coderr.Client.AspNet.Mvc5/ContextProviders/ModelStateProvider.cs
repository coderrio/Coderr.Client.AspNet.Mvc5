using System.Collections.Generic;
using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;

namespace codeRR.Client.AspNet.Mvc5.ContextProviders
{
    /// <summary>
    ///     Name: "ModelState"
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Each property have a key named <c>"[PropertyName].RawValue"</c> and <c>"[PropertyName].AttemptedValue"</c>. If
    ///         there are errors, another property named "[PropertyName].Error" will be added (one for each error)
    ///     </para>
    /// </remarks>
    public class ModelStateProvider : IContextInfoProvider
    {
        /// <inheritdoc />
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var aspNetContext = context as AspNetMvcContext;
            if (aspNetContext?.ModelState == null || aspNetContext.ModelState.Count == 0)
                return null;

            var dict = new Dictionary<string, string>();
            foreach (var kvp in aspNetContext.ModelState)
            {
                if (kvp.Value == null)
                {
                    dict[kvp.Key + ".RawValue"] = "null";
                    continue;
                }

                var state = kvp.Value;
                if (state.Value != null)
                {
                    if (state.Value.RawValue != null)
                        dict[kvp.Key + ".RawValue"] = state.Value.RawValue.ToString();
                    if (state.Value.AttemptedValue != null)
                        dict[kvp.Key + ".AttemptedValue"] = state.Value.AttemptedValue;
                }

                foreach (var error in state.Errors)
                    dict[kvp.Key + ".Error"] = error.ErrorMessage;
            }
            return new ContextCollectionDTO(Name, dict);
        }

        /// <summary>"ModelState"</summary>
        public string Name => "ModelState";
    }
}