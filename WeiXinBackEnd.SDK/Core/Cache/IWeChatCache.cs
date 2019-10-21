using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WeiXinBackEnd.SDK.Core.Cache
{
    public interface IWeChatCache
    {
        T Get<T>([NotNull]string key) where T : class;

        Task<T> GetAsync<T>([NotNull]string key, CancellationToken cancellationToken) where T : class;

        bool Set<T>([NotNull] string key, T value) where T : class;

        Task<bool> SetAsync<T>([NotNull] string key, T value, CancellationToken cancellationToken) where T : class;

        T GetOrCreate<T>([NotNull]string key, Func<T> createFactory) where T : class;

        T GetOrCreate<T>([NotNull]string key, Func<T> createFactory, DateTimeOffset? offset) where T : class;

        Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory,
            CancellationToken cancellationToken) where T : class;

        Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset,
            CancellationToken cancellationToken) where T : class;
    }
}