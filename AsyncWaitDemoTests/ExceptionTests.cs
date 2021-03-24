using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncWaitDemo;
using Xunit;

namespace AsyncWaitDemoTests
{
    public class ExceptionTests
    {
        async Task AsyncMethod()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));

            throw new CustomException("pop");
        }

        [Fact]
        public void when_using_wait()
        {
            try
            {
                AsyncMethod().Wait();
            }
            catch (AggregateException)
            {
            }
        }

        [Fact]
        public async Task when_using_async_await()
        {
            try
            {
                await AsyncMethod();
            }
            catch (CustomException)
            {
            }
        }

        [Fact]
        public void when_using_getAwaiter_getResult()
        {
            try
            {
                AsyncMethod().GetAwaiter().GetResult();
            }
            catch (CustomException)
            {
            }
        }

        [Fact]
        public void when_using_our_extension_method()
        {
            try
            {
                AsyncMethod().AwaitAndUnwrap();
            }
            catch (CustomException)
            {
            }
        }

        [Fact]
        public async Task when_cancelling_a_task()
        {
            async Task<int> SomethingAsync(CancellationToken token)
            {
                await Task.Delay(TimeSpan.FromHours(1), token);
                return 1;
            }

            var cancellationSource = new CancellationTokenSource();
            var task = SomethingAsync(cancellationSource.Token);
            cancellationSource.Cancel();

            await Assert.ThrowsAsync<TaskCanceledException>(() => task);
        }

        [Fact]
        public async Task when_cancelling_a_task_using()
        {
            async Task<int> SomethingAsync(CancellationToken token)
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    // do some work
                    token.ThrowIfCancellationRequested();
                }
            }

            var cancellationSource = new CancellationTokenSource();
            var task = SomethingAsync(cancellationSource.Token);
            cancellationSource.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() => task);
        }


        class CustomException : Exception
        {
            public CustomException(string message) : base(message)
            {

            }
        }
    }
}
