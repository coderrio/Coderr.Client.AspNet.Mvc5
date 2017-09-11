using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using FluentAssertions;
using NSubstitute;
using OneTrueError.Client.AspNet.Mvc5.ContextProviders;
using OneTrueError.Client.Reporters;
using Xunit;

namespace OneTrueError.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class RouteDataProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_OneTrueErrorContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new RouteDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            var routeData = new RouteData();
            routeData.DataTokens.Add("MyToken", "Value1");
            routeData.Values.Add("MyValue", "Value2");
            context.RouteData = routeData;

            var sut = new RouteDataProvider();
            var result = sut.Collect(context);

            result.Property("DataToken[\"MyToken\"]").Should().Be("Value1");
            result.Property("Values[\"MyValue\"]").Should().Be("Value2");
        }

        [Fact]
        public void should_return_null_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            context.RouteData = new RouteData();

            var sut = new RouteDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

    }
}
