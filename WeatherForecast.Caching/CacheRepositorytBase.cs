using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace WeatherForecast.Caching
{
    public abstract class CacheRepositoryBase
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public async Task<IEnumerable<T>> GetByKeysAsync<T>(params string[] keys)
        {
            var db = CacheConnectionProvider.GetDatabase();
            var result = await Task.WhenAll(keys
                    .Select(x => db.HashGetAsync(x, "payload"))
            );
            return result
                .Select(x => (x.HasValue ? JsonConvert.DeserializeObject<T>(x) : default(T)))
                .Where(x => x != null);
        }

        public async Task<T> GetByKeyAsync<T>(string index, string key)
        {
            var db = CacheConnectionProvider.GetDatabase();
            var val = await db.HashGetAsync(key, index);
            return val.HasValue ? JsonConvert.DeserializeObject<T>(val) : default(T);
        }

        public async Task SetKeyExpirationAsync(string key, TimeSpan expiry)
        {
            var db = CacheConnectionProvider.GetDatabase();
            await db.KeyExpireAsync(key, expiry);
        }

        public async Task DeleteKeyAsync(string key)
        {
            var db = CacheConnectionProvider.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task UpsertKeyAsync<T>(string key, T entry) where T : new()
        {
            var db = CacheConnectionProvider.GetDatabase();
            var payload = JsonConvert.SerializeObject(entry, _settings);
            await db.HashSetAsync(key, new[] { new HashEntry("payload", payload) });
        }
    }
}
