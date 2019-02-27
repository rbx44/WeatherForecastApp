using System.Threading.Tasks;
using WeatherForecast.Business.Builders;
using WeatherForecast.Data.Models;
using WeatherForecast.Data.Repositories;

namespace WeatherForecast.Business.Handlers
{
    public class WeatherForecastHandler
    {
        public async Task<Models.WeatherForecast> GetResult(string userId, string remoteIpAddress, bool force)
        {
            var weatherForecastBuilder = new WeatherForecastBuilder();
            var result = await weatherForecastBuilder.GetWeatherForecastCache(remoteIpAddress, force);

            var weatherForecastRepository = new WeatherForecastRepository();
            var userEntry = new WeatherForecastData
            {
                UserId = userId,
                City = result.City
            };

            await weatherForecastRepository.InsertUserHistory(userEntry);
            return result;
        }
    }
}
