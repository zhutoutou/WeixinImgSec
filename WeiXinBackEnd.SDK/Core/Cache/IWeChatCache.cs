using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WeiXinBackEnd.SDK.Core.Cache
{
    public interface IWeChatCache
    {
        T Get<T>([NotNull]string key);

        Task<T> GetAsync<T>([NotNull]string key, CancellationToken cancellationToken);

        bool Set<T>([NotNull]string key, T value);

        Task<bool> SetAsync<T>([NotNull]string key, T value, CancellationToken cancellationToken);

        T GetOrCreate<T>([NotNull]string key, Func<T> createFactory);

        T GetOrCreate<T>([NotNull]string key, Func<T> createFactory, DateTimeOffset? offset);

        Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory,
            CancellationToken cancellationToken);

        Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset,
            CancellationToken cancellationToken);
    }
}