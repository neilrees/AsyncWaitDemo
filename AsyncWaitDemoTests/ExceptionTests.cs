using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.Throws<AggregateException>(() =>
            {
                AsyncMethod().Wait();
            });
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


        class CustomException : Exception
        {
            public CustomException(string message) : base(message)
            {

            }
        }
    }
}
