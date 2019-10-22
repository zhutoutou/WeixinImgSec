using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WeiXinBackEnd.SDK.Core.Cache
{
    /// <summary>
    /// 微信模块内置缓存接口
    /// Todo 滑动超时
    /// </summary>
    public interface IWeChatCache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <returns></returns>
        TResult Get<TResult>([NotNull]string key) where TResult:class;

        /// <summary>
        /// 异步获取缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <param name="cancellationToken">超时Token</param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>([NotNull]string key, CancellationToken cancellationToken = default) where TResult:class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <param name="value">缓存的值</param>
        /// <param name="offset">过期时间</param>
        /// <returns></returns>
        bool Set<TResult>([NotNull] string key, TResult value, DateTimeOffset? offset = null) where TResult:class;

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <param name="value">缓存的值</param>
        /// <param name="offset">过期时间</param>
        /// <param name="cancellationToken">超时Token</param>
        /// <returns></returns>
        Task<bool> SetAsync<TResult>([NotNull] string key, TResult value, DateTimeOffset? offset = null, CancellationToken cancellationToken = default) where TResult:class;

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存的key</param>
        bool Remove([NotNull] string key);

        /// <summary>
        /// 异步移除缓存
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="cancellationToken">超时Token</param>
        /// <returns></returns>
        Task<bool> RemoveAsync([NotNull] string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取或者创建缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <param name="createFactory">创建缓存对象方法</param>
        /// <param name="offset">过期时间</param>
        /// <returns></returns>
        TResult GetOrCreate<TResult>([NotNull]string key, Func<TResult> createFactory, DateTimeOffset? offset = null) where TResult:class;

        /// <summary>
        /// 异步获取或者创建缓存
        /// </summary>
        /// <typeparam name="TResult">缓存对象类型</typeparam>
        /// <param name="key">缓存的key</param>
        /// <param name="createFactory">创建缓存对象方法</param>
        /// <param name="offset">过期时间</param>
        /// <param name="cancellationToken">超时Token</param>
        /// <returns></returns>
        Task<TResult> GetOrCreateAsync<TResult>(string key, Func<Task<TResult>> createFactory, DateTimeOffset? offset = null,
            CancellationToken cancellationToken = default) where TResult:class;

        /// <summary>
        /// 锁定并完成CacheOperate
        /// </summary>
        /// <param name="key">缓存的key</param>
        /// <param name="operateFactory">具体加锁后的操作</param>
        /// <param name="lockTimeSpan">等待锁的时间</param>
        /// <returns></returns>
        Task LockAndOperateAsync(string key, Func<Task> operateFactory, TimeSpan lockTimeSpan = default);
    }
}