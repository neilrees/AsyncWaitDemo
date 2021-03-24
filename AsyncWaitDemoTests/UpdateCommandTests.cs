using System.IO;
using AsyncWaitDemo;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace AsyncWaitDemoTests
{
    public class UpdateCommandTests
    {
        readonly MockFileSystem _fileSystem;

        public UpdateCommandTests()
        {
            _fileSystem = new MockFileSystem();
            _fileSystem.AddFile(@"source.txt", MockFileData.NullObject);
        }

        [Fact]
        public void When_updating_an_archive()
        {
            var sut = new CompressionCommandHandler(_fileSystem);

            sut.Handle(new UpdateCommand
            {
                Source = "source.txt",
            });
        }
    }
}
