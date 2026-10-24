using System;
#region Q20
/*
Complete Exercise - Create a generic Cache<TKey, TValue> with Add, Get, Remove, Contains, and expiration support.
Answer:
A generic cache class supporting item expiration.
*/
#endregion
class Program
{
    public class Cache<TKey, TValue>
    {
        private class CacheItem
        {
            public TValue Value { get; set; }
            public DateTime ExpirationTime { get; set; }
        }

        private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();

        public void Add(TKey key, TValue value, TimeSpan duration)
        {
            var item = new CacheItem
            {
                Value = value,
                ExpirationTime = DateTime.Now.Add(duration)
            };
            cache[key] = item;
        }

        public TValue Get(TKey key)
        {
            if (Contains(key))
            {
                return cache[key].Value;
            }
            return default(TValue);
        }

        public void Remove(TKey key)
        {
            cache.Remove(key);
        }

        public bool Contains(TKey key)
        {
            if (cache.ContainsKey(key))
            {
                if (cache[key].ExpirationTime > DateTime.Now)
                {
                    return true;
                }
                cache.Remove(key);
            }
            return false;
        }
    }
    public static void Main()
    {

    }
}
