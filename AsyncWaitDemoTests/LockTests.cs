using System.Threading.Tasks;
using AsyncWaitDemo;
using Xunit;

namespace AsyncWaitDemoTests
{
    public class LockTests
    {
        readonly Locks _locks = new Locks();

        [Fact]
        public Task when_using_monitors()
        {
            return _locks.UsingMonitors();
        }

        [Fact]
        public Task when_using_semaphore_slim()
        {
            return _locks.UsingSemaphoreSlim();
        }

        [Fact]
        public async Task when_async_enumerating()
        {
            var sum = 0;
            await foreach (var i in _locks.LockingAsyncEnumerable())
            {
                await Task.Delay(1);
                sum += i;
            }

            Assert.Equal(6, sum);
        }

        [Fact]
        public async Task when_enumerating()
        {
            var sum = 0;
            foreach (var i in _locks.LockingEnumerable())
            {
                await Task.Delay(1);
                sum += i;
            }

            Assert.Equal(6, sum);
        }

    }
}
