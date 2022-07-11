using System.Runtime.Caching;
using IMDbApiLib.Models;

namespace MoviesWorld.Services;

public class CacheService
{
    private readonly MemoryCache _memoryCache = MemoryCache.Default;

    public void LastSearchMovie(string research, SearchData itemResult)
    {
        if (_memoryCache.Contains(research)) return;
        DateTimeOffset expiration = DateTimeOffset.UtcNow.AddMinutes(20);
        _memoryCache.Add(research, itemResult, expiration);
    }

    public bool ContainsItem(string key)
    {
        return _memoryCache.Contains(key);
    }

    public object? GetItem(string key)
    {
        return _memoryCache.Get(key) ?? null;
    }
}