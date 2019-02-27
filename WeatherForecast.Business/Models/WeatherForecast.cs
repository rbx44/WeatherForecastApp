using System.Collections.Generic;

namespace WeatherForecast.Business.Models
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public string State { get; set; }
        public IEnumerable<WeatherInformation> DailyWeather { get; set; }
    }
}