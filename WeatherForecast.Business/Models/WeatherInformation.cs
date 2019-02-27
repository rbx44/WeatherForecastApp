using System;

namespace WeatherForecast.Business.Models
{
    public class WeatherInformation
    {
        public string Description { get; set; }
        public double? HighTemperature { get; set; }
        public double? LowTemperature { get; set; }
        public DateTime WeatherTime { get; set; }
    }
}