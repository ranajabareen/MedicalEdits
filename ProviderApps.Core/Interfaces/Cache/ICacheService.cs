using System;
using System.Collections.Generic;
using ProviderApps.Core.Models;

namespace ProviderApps.Core.Interfaces
{
    public interface ICacheService
    {
        void Cache<T>(string key, T value);
        T GetObject<T>(string key);
        List<T> GetList<T>(string key);
        List<T> GetOrDbLoadList<T>(string key) where T : class, IModel;
        List<T> RefreshList<T>(string key) where T : class, IModel;

        ICachedList CachedLists { get; }
        Dictionary<string, List<DateTimeOffset>> CachedLogger { get; }
        Dictionary<string, List<string>> CachedLoggerMessage { get; }
    }
}
