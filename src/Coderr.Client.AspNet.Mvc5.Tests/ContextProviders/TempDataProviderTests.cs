using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using NSubstitute;
using Coderr.Client.AspNet.Mvc5.ContextProviders;
using Coderr.Client.Reporters;
using Xunit;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class TempDataProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new TempDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            var tempData = new TempDataDictionary {{"MyToken", "Value1"}, {"MyValue", 20}};
            context.TempData = tempData;

            var sut = new TempDataProvider();
            var result = sut.Collect(context);

            result.Property("MyToken").Should().Be("Value1");
            result.Property("MyValue").Should().Be("20");
        }

        [Fact]
        public void should_return_null_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            context.TempData = new TempDataDictionary();

            var sut = new TempDataProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
}