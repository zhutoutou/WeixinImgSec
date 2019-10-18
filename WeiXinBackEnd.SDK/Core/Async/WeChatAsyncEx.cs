using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace WeiXinBackEnd.SDK.Core.Async
{
    public class WeChatAsyncEx
    {
        private readonly ConcurrentDictionary<string, AsyncLock> _mutexDictionary;

        public WeChatAsyncEx(ConcurrentDictionary<string, AsyncLock> mutexDictionary)
        {
            _mutexDictionary = mutexDictionary;
        }

        /// <summary>
        /// 异步锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <param name="lockCancellationToken"></param>
        /// <returns></returns>
        public async Task GetLockAsync(string key, Func<Task> factory, CancellationToken lockCancellationToken)
        {
            var mutex = _mutexDictionary.GetOrAdd(key, k => new AsyncLock());
            using (await mutex.LockAsync(lockCancellationToken))
            {
                await factory().ConfigureAwait(false);
            }
        }
    }
}