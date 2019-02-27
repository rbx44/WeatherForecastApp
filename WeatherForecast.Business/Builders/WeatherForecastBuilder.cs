using System;
using System.Linq;
using System.Threading.Tasks;
using NeoSmart.AsyncLock;
using WeatherForecast.Business.Exceptions;
using WeatherForecast.Business.Models;
using WeatherForecast.Business.ServiceClients;
using WeatherForecast.Caching;

namespace WeatherForecast.Business.Builders
{
    public class WeatherForecastBuilder : CacheRepositoryBase
    {
        private readonly AsyncLock _lock = new AsyncLock();
        private const string Payload = "payload";
        public async Task<Models.WeatherForecast> GetWeatherForecastCache(string remoteIpAddress, bool force)
        {
            var key = $"weather:{remoteIpAddress}";

            if(!force)
            {
                var firstCheck = await GetByKeyAsync<Models.WeatherForecast>(Payload, key);
                if (firstCheck != null)
                    return firstCheck;
            }

            using (await _lock.LockAsync())
            {
                if (force)
                    return await FillWeatherForecastCache(remoteIpAddress, key);

                var secondCheck = await GetByKeyAsync<Models.WeatherForecast>(Payload, key);
                if (secondCheck != null)
                    return secondCheck;

                return await FillWeatherForecastCache(remoteIpAddress, key);
            }
        }

        private async Task<Models.WeatherForecast> FillWeatherForecastCache(string remoteIpAddress, string key)
        {
            var weatherClient = new WeatherAppClient();
            var weatherData = await weatherClient.GetWeatherForecast(remoteIpAddress);

            if (!weatherData.IsSuccess || string.IsNullOrEmpty(weatherData.Data?.Today?.City))
                throw new BadApiResponseException(weatherData.Error);

            var now = DateTime.UtcNow.Date;
            var itemToCache = new Models.WeatherForecast
            {
                City = weatherData.Data.Today.City,
                State = weatherData.Data.Today.State,
                DailyWeather = weatherData.Data.Daily
                        .Where(x => now.AddDays(-1) < x.UtcTime
                                    && x.UtcTime < now.AddDays(4))
                        .Select(x => new WeatherInformation
                        {
                            Description = x.Description,
                            HighTemperature = x.HighTemperature,
                            LowTemperature = x.LowTemperature,
                            WeatherTime = x.UtcTime
                        })
            };

            await UpsertKeyAsync(key, itemToCache);
            await SetKeyExpirationAsync(key, TimeSpan.FromHours(2));

            return itemToCache;
        }
    }
}