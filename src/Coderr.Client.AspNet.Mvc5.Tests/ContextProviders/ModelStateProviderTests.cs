using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using NSubstitute;
using codeRR.Client.AspNet.Mvc5.ContextProviders;
using codeRR.Client.Reporters;
using Xunit;

namespace codeRR.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class ModelStateProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new ModelStateProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_include_included_items()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            var items = new Dictionary<string,ModelState>
            {
                {"MyKey", BuildState("Raw", "Attempted", "Failed")},
                {"NoError", BuildState(5, "5", null)}
            };
            context.ModelState = items;

            var sut = new ModelStateProvider();
            var result = sut.Collect(context);

            result.Property("MyKey.RawValue").Should().Be("Raw");
            result.Property("MyKey.AttemptedValue").Should().Be("Attempted");
            result.Property("MyKey.Error").Should().Be("Failed");
            result.Properties.ContainsKey("NoError.Error").Should().BeFalse();
        }

        private ModelState BuildState(object rawValue, string attemptedValue, string errorMsg)
        {
            var state = new ModelState
            {
                Value =new ValueProviderResult(rawValue, attemptedValue, CultureInfo.CurrentCulture),
            };
            if (errorMsg != null)
                state.Errors.Add(errorMsg);
            return state;
        }

        [Fact]
        public void should_not_return_an_empty_collection_when_the_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetMvcContext(this, new Exception(), httpContext);
            context.ModelState = new Dictionary<string, ModelState>();

            var sut = new ModelStateProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

    }
}
