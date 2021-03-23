using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncWaitDemo
{
    public class Locks
    {
        static readonly object Sync = new object();

        public void UsingLock()
        {
            lock (Sync)
            {
                Task.Delay(1).Wait();
            }
        }

        public async Task UsingMonitors()
        {
            Monitor.Enter(Sync);

            await Task.Delay(1);

            Monitor.Exit(Sync);
        }

        readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task UsingSemaphoreSlim()
        {
            await _semaphore.WaitAsync();

            await Task.Delay(1);

            _semaphore.Release();
        }

        public async IAsyncEnumerable<int> LockingAsyncEnumerable()
        {
            await Task.Delay(1);

            lock (Sync)
            {
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }

        public IEnumerable<int> LockingEnumerable()
        {
            lock (Sync)
            {
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }

    }
}
