#define SYNC

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if ASYNC

namespace AsyncWaitDemo
{

    public static class CompressionExtensions
    {
        public static void Compress(this CompressionLibary compressionLibary, string sourceFilePath, string targetFilePath)
        {
            compressionLibary.CompressAsync(sourceFilePath, targetFilePath).Await();
        }
    }
}

#endif
