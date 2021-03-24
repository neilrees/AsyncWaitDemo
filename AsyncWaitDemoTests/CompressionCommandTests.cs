using System.IO;
using AsyncWaitDemo;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace AsyncWaitDemoTests
{
    public class CompressionCommandTests
    {
        readonly MockFileSystem _fileSystem;

        public CompressionCommandTests()
        {
            _fileSystem = new MockFileSystem();
            _fileSystem.AddFile(@"source.txt", MockFileData.NullObject);
        }

        [Fact]
        public void When_compressing_a_file_it_should_not_throw()
        {
            var sut = new CompressionCommandHandler(_fileSystem);

            sut.Handle(new CompressCommand
            {
                Source = "source.txt",
                Target = "target.txt"
            });
        }

        [Fact]
        public void When_file_is_missing_it_should_throw()
        {
            var sut = new CompressionCommandHandler(_fileSystem);

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
