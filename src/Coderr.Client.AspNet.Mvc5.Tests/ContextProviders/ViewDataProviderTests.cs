using System;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using NSubstitute;
using Coderr.Client.AspNet.Mvc5.ContextProviders;
using Coderr.Client.Reporters;
using Xunit;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class ViewDataProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new ViewDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            context.ViewData = new ViewDataDictionary {{"Id", 3}, {"Title", "Hello world"}};

            var sut = new ViewDataProvider();
            var result = sut.Collect(context);

            result.Property("Id").Should().Be("3");
            result.Property("Title").Should().Be("Hello world");
        }

        [Fact]
        public void should_return_null_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            context.ViewData = new ViewDataDictionary();

            var sut = new ViewDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
}