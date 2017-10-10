using System;
using System.Collections.Specialized;
using System.Web;
using FluentAssertions;
using NSubstitute;
using codeRR.Client.AspNet.Mvc5.ContextProviders;
using codeRR.Client.AspNet.Mvc5.Tests.ContextProviders.Stubs;
using codeRR.Client.Reporters;
using Xunit;

namespace codeRR.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class SessionProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new SessionProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            var mySession = new MySession {{"MyToken", "Value1"}, {"MyValue", 20}};
            context.HttpContext.Session.Returns(mySession);

            var sut = new SessionProvider();
            var result = sut.Collect(context);

            result.Property("MyToken").Should().Be("Value1");
            result.Property("MyValue").Should().Be("20");
        }

        [Fact]
        public void should_return_null_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            httpContext.Session.Returns(new MySession());

            var sut = new SessionProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
}