using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using NSubstitute;
using OneTrueError.Client.AspNet.Mvc5.ContextProviders;
using OneTrueError.Client.AspNet.Mvc5.Tests.ContextProviders.Stubs;
using OneTrueError.Client.Reporters;
using Xunit;

namespace OneTrueError.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class HttpHeadersProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_OneTrueErrorContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new HttpHeadersProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_headers()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            var headers = new NameValueCollection {{"Host", "local"}, {"Server", "Awesome"}};
            httpContext.Request.Headers.Returns(headers);

            var sut = new HttpHeadersProvider();
            var result = sut.Collect(context);

            result.Property("Host").Should().Be("local");
            result.Property("Server").Should().Be("Awesome");
        }

        [Fact]
        public void should_include_url()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            httpContext.Request.Url.Returns(new Uri("http://localhost/majs"));
            httpContext.Request.Headers.Returns(new NameValueCollection());

            var sut = new HttpHeadersProvider();
            var result = sut.Collect(context);

            result.Property("Url").Should().Be("http://localhost/majs");
        }


        [Fact]
        public void should_not_return_an_empty_collection_when_the_header_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            httpContext.Request.Headers.Returns(new NameValueCollection());

            var sut = new HttpHeadersProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
    
}
