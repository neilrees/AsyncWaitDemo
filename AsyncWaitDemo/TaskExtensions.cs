using System.Threading.Tasks;

namespace AsyncWaitDemo
{
    public static class TaskExtensions
    {
        public static void AwaitAndUnwrap(this Task task)
        {
            task.GetAwaiter().GetResult();
        }

        public static T AwaitAndUnwrap<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}