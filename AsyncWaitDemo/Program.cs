#define SYNC

using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncWaitDemo
{
    class Program
    {

#if SYNC

        static void _Main(string[] args)
        {
            using var stream = File.OpenRead("TextFile.txt");
            using var streamReader = new StreamReader(stream);

            var s = streamReader.ReadToEnd();

            Console.WriteLine(s);
        }

        static Task Main(string[] args)
        {
            Stream stream;
            StreamReader streamReader;

            Task<string> Part1()
            {
                stream = File.OpenRead("TextFile.txt");
                streamReader = new StreamReader(stream);

                return streamReader.ReadToEndAsync();
            }

            Task Part2(Task<string> s)
            {
                Console.WriteLine(s.Result);

                return Task.CompletedTask;
            }

            Task Dispose1(Task t)
            {
                return stream.DisposeAsync().AsTask();
            }

            Task Dispose2(Task t)
            {
                streamReader.Dispose();

                return Task.CompletedTask;
            }

            return Part1()
                .ContinueWith(t => Part2(t))
                .ContinueWith(t => Dispose1(t))
                .ContinueWith(t => Dispose2(t));
        }

#else

        static async Task Main(string[] args)
        {
            await using var stream = File.OpenRead("TextFile.txt");
            using var streamReader = new StreamReader(stream);

            var s = await streamReader.ReadToEndAsync();

            Console.WriteLine(s);
        }

        

#endif
    }
}
