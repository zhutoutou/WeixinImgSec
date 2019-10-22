using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace WeiXinBackEnd.SDK.Core.Async
{
    public static class AsyncHelper
    {
        private static readonly ConcurrentDictionary<string, AsyncLock> MutexDictionary;

        static AsyncHelper()
        {
            MutexDictionary = new ConcurrentDictionary<string, AsyncLock>();
        }

        /// <summary>
        /// Checks if given method is an async method.
        /// </summary>
        /// <param name="method">A method to check</param>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task) ||
                   method.ReturnType.GetTypeInfo().IsGenericType &&
                   method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        /// <summary>
        /// Checks if given method is an async method.
        /// </summary>
        /// <param name="method">A method to check</param>
        [Obsolete("Use MethodInfo.IsAsync() extension method!")]
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return method.IsAsync();
        }

        /// <summary>
        /// Runs a async method synchronously.
        /// </summary>
        /// <param name="func">A function that returns a result</param>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <returns>Result of the async operation</returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }


        /// <summary>
        /// Runs a async method synchronously.
        /// </summary>
        /// <param name="action">An async action</param>
        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }

        /// <summary>
        /// 异步锁
        /// </summary>
        /// <param name="key">锁的标识</param>
        /// <param name="operateFactory">具体加锁后的操作</param>
        /// <param name="lockCancellationToken">等待锁的时间</param>
        /// <returns></returns>
        public static async Task GetLockAsync(string key, Func<Task> operateFactory, CancellationToken lockCancellationToken)
        {
            var mutex = MutexDictionary.GetOrAdd(key, k => new AsyncLock());
            using (await mutex.LockAsync(lockCancellationToken))
            {
                await operateFactory().ConfigureAwait(false);
            }
        }

    }
}