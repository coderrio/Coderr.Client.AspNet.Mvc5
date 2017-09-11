using System;
using System.Collections.Specialized;
using System.Web;
using FluentAssertions;
using NSubstitute;
using OneTrueError.Client.AspNet.Mvc5.ContextProviders;
using OneTrueError.Client.Reporters;
using Xunit;

namespace OneTrueError.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class FormProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_OneTrueErrorContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new FormProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_form_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);
            var items = new NameValueCollection
            {
                {"MyKey", "MyValue"},
                {"AnotherKey", "AnotherLevel"},
                {"MyKey", "SecondValue"}
            };
            httpContext.Request.Form.Returns(items);

            var sut = new FormProvider();
            var result = sut.Collect(context);

            result.Properties["MyKey"].Should().Be("MyValue,SecondValue");
            result.Properties["AnotherKey"].Should().Be("AnotherLevel");
        }

        [Fact]
        public void should_not_return_an_empty_collection_when_the_form_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);

            var sut = new FormProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }
    }
}