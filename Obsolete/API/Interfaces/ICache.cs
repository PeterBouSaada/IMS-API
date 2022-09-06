using System;

namespace API.Interfaces
{
    public interface ICache<T>
    {
        T GetOrSet(string key, int ttl, Func<T> function);
        bool Set(string key, int ttl, T objectToCache);
        void Expire(string key);
    }
}