#define SYNC

using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Compression
{

#if SYNC

    public class CompressionLibrary
    {
        readonly IFileSystem _fileSystem;

        public CompressionLibrary(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Compress(string sourceFilePath, string targetFilePath)
        {
            using var source = _fileSystem.File.OpenRead(sourceFilePath);
            using var target = _fileSystem.File.OpenWrite(targetFilePath);

            source.CopyTo(target);
        }

        public void Uncompress(string sourceFilePath, string targetFilePath)
        {
            using var source = _fileSystem.File.OpenRead(sourceFilePath);
            using var target = _fileSystem.File.OpenWrite(targetFilePath);

            source.CopyTo(target);
        }

        public LockScope Lock(string commandSource)
        {
            return new LockScope();
        }

        public class LockScope : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }

#endif
#if ASYNC

    public class CompressionLibrary
    {
        IFileSystem _fileSystem;

        public CompressionLibrary(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Task Compress(string sourceFilePath, string targetFilePath)
        {
            var source = _fileSystem.File.OpenRead(sourceFilePath);
            var target = _fileSystem.File.OpenWrite(targetFilePath);

            return source.CopyToAsync(target);
        }

        public Task Uncompress(string sourceFilePath, string targetFilePath)
        {
            var source = _fileSystem.File.OpenRead(sourceFilePath);
            var target = _fileSystem.File.OpenWrite(targetFilePath);

            return source.CopyToAsync(target);
        }

        public async Task<LockScope> Lock(string commandSource)
        {
            await Task.Delay(1);

            return new LockScope();
        }

        public class LockScope : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }


#endif


#if ASYNC2

    public class CompressionLibrary
    {
        readonly IFileSystem _fileSystem;

        public CompressionLibrary(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Task CompressAsync(string sourceFilePath, string targetFilePath, CancellationToken token)
        {
            var source = _fileSystem.File.OpenRead(sourceFilePath);
            var target = _fileSystem.File.OpenWrite(targetFilePath);

            return source.CopyToAsync(target, token);
        }

        public Task Uncompress(string sourceFilePath, string targetFilePath, CancellationToken token)
        {
            var source = _fileSystem.File.OpenRead(sourceFilePath);
            var target = _fileSystem.File.OpenWrite(targetFilePath);

            return source.CopyToAsync(target, token);
        }

        public async Task<LockScope> LockAsync(string commandSource, CancellationToken token)
        {
            await Task.Delay(1, token);

            return new LockScope();
        }

        public class LockScope : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }


#endif


}
