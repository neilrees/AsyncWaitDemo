using System.IO;
using AsyncWaitDemo;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace AsyncWaitDemoTests
{
    public class CompressionCommandTests
    {
        MockFileSystem fileSystem;

        public CompressionCommandTests()
        {
            fileSystem = new MockFileSystem();
            fileSystem.AddFile(@"source.txt", MockFileData.NullObject);
        }

        [Fact]
        public void When_compressing_a_file_it_should_not_throw()
        {
            var sut = new CompressionCommandHandler(fileSystem);

            sut.Handle(new CompressCommand
            {
                Source = "source.txt",
                Target = "target.txt"
            });
        }

        [Fact]
        public void When_file_is_missing_it_should_throw()
        {
            var sut = new CompressionCommandHandler(fileSystem);

            Assert.Throws<FileNotFoundException>(() =>
            {
                sut.Handle(new CompressCommand
                {
                    Source = "missing.txt",
                    Target = "target.txt"
                });
            });
        }
    }
}
