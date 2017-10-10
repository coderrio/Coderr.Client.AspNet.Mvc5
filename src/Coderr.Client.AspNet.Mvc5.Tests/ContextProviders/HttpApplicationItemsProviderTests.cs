using System;
using System.Web;
using FluentAssertions;
using NSubstitute;
using codeRR.Client.AspNet.Mvc5.ContextProviders;
using codeRR.Client.AspNet.Mvc5.Tests.ContextProviders.Stubs;
using codeRR.Client.Reporters;
using Xunit;

namespace codeRR.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class HttpApplicationItemsProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new HttpApplicationItemsProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_form_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            var state = new TestApplicationState {["Ada"] = "Bada"};
            httpContext.Application.Returns(state);

            var sut = new HttpApplicationItemsProvider();
            var result = sut.Collect(context);

            result.Property("Ada").Should().Be("Bada");
        }

        [Fact]
        public void should_not_return_an_empty_collection_when_the_application_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            context.HttpContext.Application.Returns(new TestApplicationState());

            var sut = new HttpApplicationItemsProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
}