using System;
using System.Collections.Specialized;
using System.Web;
using FluentAssertions;
using NSubstitute;
using Coderr.Client.AspNet.Mvc5.ContextProviders;
using Coderr.Client.Reporters;
using Xunit;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class QueryStringProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new QueryStringProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            var items = new NameValueCollection
            {
                {"MyKey", "Value"}
            };
            httpContext.Request.QueryString.Returns(items);

            var sut = new QueryStringProvider();
            var result = sut.Collect(context);

            result.Property("MyKey").Should().Be("Value");
        }
        
        [Fact]
        public void should_return_null_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            httpContext.Request.QueryString.Returns(new NameValueCollection());

            var sut = new QueryStringProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

    }
}
