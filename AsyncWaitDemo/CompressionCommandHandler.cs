using System.IO.Abstractions;
using System.Threading.Tasks;

namespace AsyncWaitDemo
{
    public class CompressionCommandHandler
    {
        readonly IFileSystem _fileSystem;

        public CompressionCommandHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Handle(CompressCommand command)
        {
            var compression = new CompressionLibrary(_fileSystem);

            compression.Compress(command.Source, command.Target);
        }

        public void Handle(UpdateCommand command)
        {
            var compression = new CompressionLibrary(_fileSystem);

            using (compression.Lock(command.Source))
            {
                // Do stuffs whilst holding the lock
            }
        }
    }
}
