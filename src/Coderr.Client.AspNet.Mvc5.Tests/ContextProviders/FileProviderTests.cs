using System;
using System.Web;
using FluentAssertions;
using NSubstitute;
using Coderr.Client.AspNet.Mvc5.ContextProviders;
using Coderr.Client.Reporters;
using Xunit;

namespace Coderr.Client.AspNet.Mvc5.Tests.ContextProviders
{
    public class FileProviderTests
    {
        [Fact]
        public void should_ignore_incorrect_codeRRContext()
        {
            var context = new ErrorReporterContext(this, new Exception());

            var sut = new FileProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();

        }

        [Fact]
        public void should_not_return_an_empty_collection_when_the_File_collection_is_empty()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var context = new AspNetContext(this, new Exception(), httpContext);

            var sut = new FileProvider();
            var result = sut.Collect(context);

            result.Should().BeNull();
        }

        [Fact]
        public void should_collect_files_that_are_included_in_the_Request()
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var files = Substitute.For<HttpFileCollectionBase>();
            var file = Substitute.For<HttpPostedFileBase>();
            file.FileName.Returns("my.file");
            file.ContentType.Returns("application/octet-stream");
            file.ContentLength.Returns(100);
            files.Count.Returns(1);
            files.Get(0).Returns(file);
            httpContext.Request.Files.Returns(files);
            var context = new AspNetContext(this, new Exception(), httpContext);

            var sut = new FileProvider();
            var result = sut.Collect(context);

            result.Name.Should().Be("HttpRequestFiles");
            result.Properties["my.file"].Should().Be("application/octet-stream;length=100");
        }
    }
}
