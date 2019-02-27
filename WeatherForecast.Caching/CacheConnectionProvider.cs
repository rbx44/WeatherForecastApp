using System;
using StackExchange.Redis;

namespace WeatherForecast.Caching
{
    public class CacheConnectionProvider
    {
        private static readonly string Config = GetRedisConnectionString();
        private static readonly int DatabaseId = GetRedisDataBaseId();

        private static int GetRedisDataBaseId(string name = "RedisDataBaseId")
        {
            int.TryParse(Environment.GetEnvironmentVariable($"ConnectionStrings:{name}", EnvironmentVariableTarget.Process), out var dbId);
            return dbId;
        }

        private static string GetRedisConnectionString(string name = "Redis")
        {
            return Environment.GetEnvironmentVariable($"{name}", EnvironmentVariableTarget.Process);
        }

        private static readonly Lazy<ConnectionMultiplexer> Multiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(Config));
        public static ConnectionMultiplexer Connection => Multiplexer.Value;

        public static IDatabase GetDatabase()
        {
            return GetDatabase(DatabaseId);
        }

        public static IDatabase GetDatabase(int dbId)
        {
            return Connection.GetDatabase(dbId);
        }
    }
}
