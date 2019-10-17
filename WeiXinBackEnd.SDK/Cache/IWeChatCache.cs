using System;
using System.Threading.Tasks;

namespace WeiXinBackEnd.SDK.Cache
{
    public interface IWeChatCache
    {
        T Get<T>(string key);

        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createFactory);

        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createFactory, DateTimeOffset offset);
    }
}