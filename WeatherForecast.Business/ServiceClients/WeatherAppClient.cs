using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Business.Extensions;
using WeatherForecast.Business.Models;

namespace WeatherForecast.Business.ServiceClients
{
    public class WeatherAppClient
    {
        private const string WeatherApiUrl = "https://j9l4zglte4.execute-api.us-east-1.amazonaws.com/api/ctl/weather";

        public async Task<WeatherForecastApiResult> GetWeatherForecast(string remoteIpAddress)
        {
            var result = new WeatherForecastApiResult();
            try
            {
                using (var client = new HttpClient())
                {
                    if (remoteIpAddress != "127.0.0.1")
                        client.DefaultRequestHeaders.Add("X-Forwarded-For", remoteIpAddress);

                    using (var response = await client.GetAsync(WeatherApiUrl))
                    {
                        result.IsSuccess = response.IsSuccessStatusCode;

                        if (!response.IsSuccessStatusCode)
                            result.Error = $"{response.StatusCode}: {response.ReasonPhrase}";

                        result.Data = await response.DeserializeContent<WeatherForecastApiResponseModel>();
                    }
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Error = e.Message;
            }

            return result;
        }
    }
}