using System.Web.Mvc;
using Coderr.Client.AspNet.Mvc5.ContextProviders;
using Coderr.Client.AspNet.Mvc5.Handlers;
using Coderr.Client.Config;

// ReSharper disable once CheckNamespace

namespace Coderr.Client
{
    /// <summary>
    ///     Configuration extensions specific for ASP.NET MVC. Read the <see cref="CoderrConfiguration" /> documentation for
    ///     all
    ///     configuration options.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        ///     Activate our automatic detection of unhandled exceptions.
        /// </summary>
        /// <param name="configurator">configuration class</param>
        /// <remarks>
        ///     <para>Adds context collectors for forms, query string, sessions, user agent and http headers.</para>
        ///     <para>
        ///         MVC exceptions are detected through a custom global error filter.
        ///     </para>
        /// </remarks>
        public static void CatchMvcExceptions(this CoderrConfiguration configurator)
        {
            configurator.ContextProviders.Add(new FormProvider());
            configurator.ContextProviders.Add(new FileProvider());
            configurator.ContextProviders.Add(new QueryStringProvider());
            configurator.ContextProviders.Add(new SessionProvider());
            configurator.ContextProviders.Add(new HttpHeadersProvider());
            configurator.ContextProviders.Add(new HttpApplicationItemsProvider());

            configurator.ContextProviders.Add(new ViewDataProvider());
            configurator.ContextProviders.Add(new ViewBagProvider());
            configurator.ContextProviders.Add(new RouteDataProvider());
            configurator.ContextProviders.Add(new TempDataProvider());
            configurator.ContextProviders.Add(new ModelStateProvider());
            configurator.ContextProviders.Add(new ModelProvider());


            GlobalFilters.Filters.Add(new CoderrFilter());
            ErrorHttpModule.Activate();
        }

        /// <summary>
        ///     Display the built in error pages.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         codeRR has a set of built in error pages which can shown when an exception is thrown.
        ///     </para>
        /// </remarks>
        public static void DisplayErrorPages(this CoderrConfiguration instance)
        {
            ErrorHttpModule.DisplayErrorPage = true;
        }
    }
}