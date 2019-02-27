using System;
using System.Collections.Generic;

namespace WeatherForecast.Business.Models
{
    public class WeatherForecastApiResult
    {
        public bool IsSuccess { get; set; }
        public WeatherForecastApiResponseModel Data { get; set; }
        public string Error { get; set; }
    }

    public class WeatherForecastApiResponseModel
    {
        public Today Today { get; set; }
        public IEnumerable<Daily> Daily { get; set; }
        public IEnumerable<Hourly> Hourly { get; set; }
        public Alerts Alerts { get; set; }
    }

    public class Today
    {
        public string Daylight { get; set; }
        public string Description { get; set; }
        public string SkyInfo { get; set; }
        public string SkyDescription { get; set; }
        public string Temperature { get; set; }
        public string TemperatureDesc { get; set; }
        public string Comfort { get; set; }
        public double? HighTemperature { get; set; }
        public double? LowTemperature { get; set; }
        public string Humidity { get; set; }
        public string DewPoint { get; set; }
        public string Precipitation1H { get; set; }
        public string Precipitation3H { get; set; }
        public string Precipitation6H { get; set; }
        public string Precipitation12H { get; set; }
        public string Precipitation24H { get; set; }
        public string PrecipitationDesc { get; set; }
        public string AirInfo { get; set; }
        public string AirDescription { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string WindDesc { get; set; }
        public string WindDescShort { get; set; }
        public string BarometerPressure { get; set; }
        public string BarometerTrend { get; set; }
        public string Visibility { get; set; }
        public string SnowCover { get; set; }
        public string Icon { get; set; }
        public string IconName { get; set; }
        public string IconLink { get; set; }
        public string AgeMinutes { get; set; }
        public string ActiveAlerts { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Distance { get; set; }
        public double? Elevation { get; set; }
        public DateTime UtcTime { get; set; }
    }

    public class Daily
    {
        public string Daylight { get; set; }
        public string Description { get; set; }
        public string SkyInfo { get; set; }
        public string SkyDescription { get; set; }
        public string TemperatureDesc { get; set; }
        public string Comfort { get; set; }
        public double? HighTemperature { get; set; }
        public double? LowTemperature { get; set; }
        public string Humidity { get; set; }
        public string DewPoint { get; set; }
        public string PrecipitationProbability { get; set; }
        public string PrecipitationDesc { get; set; }
        public string RainFall { get; set; }
        public string SnowFall { get; set; }
        public string AirInfo { get; set; }
        public string AirDescription { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string WindDesc { get; set; }
        public string WindDescShort { get; set; }
        public string BeaufortScale { get; set; }
        public string BeaufortDescription { get; set; }
        public string UvIndex { get; set; }
        public string UvDesc { get; set; }
        public string BarometerPressure { get; set; }
        public string Icon { get; set; }
        public string IconName { get; set; }
        public string IconLink { get; set; }
        public string DayOfWeek { get; set; }
        public string Weekday { get; set; }
        public DateTime UtcTime { get; set; }
    }

    public class Hourly
    {
        public string Daylight { get; set; }
        public string Description { get; set; }
        public string SkyInfo { get; set; }
        public string SkyDescription { get; set; }
        public string Temperature { get; set; }
        public string TemperatureDesc { get; set; }
        public string Comfort { get; set; }
        public string Humidity { get; set; }
        public string DewPoint { get; set; }
        public string PrecipitationProbability { get; set; }
        public string PrecipitationDesc { get; set; }
        public string RainFall { get; set; }
        public string SnowFall { get; set; }
        public string AirInfo { get; set; }
        public string AirDescription { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string WindDesc { get; set; }
        public string WindDescShort { get; set; }
        public string Visibility { get; set; }
        public string Icon { get; set; }
        public string IconName { get; set; }
        public string IconLink { get; set; }
        public string DayOfWeek { get; set; }
        public string Weekday { get; set; }
        public DateTime? UtcTime { get; set; }
        public string LocalTime { get; set; }
        public string LocalTimeFormat { get; set; }
    }

    public class County
    {
        public string Value { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Warning
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; }
        public string Message { get; set; }
        public IEnumerable<County> County { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? ValidFromTimeLocal { get; set; }
        public DateTime? ValidUntilTimeLocal { get; set; }
    }

    public class Zone
    {
        public string Value { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Watch
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public int? Severity { get; set; }
        public string Message { get; set; }
        public IEnumerable<Zone> Zone { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? ValidFromTimeLocal { get; set; }
        public DateTime? ValidUntilTimeLocal { get; set; }
    }

    public class Alerts
    {
        public IEnumerable<Warning> Warning { get; set; }
        public IEnumerable<Watch> Watch { get; set; }
    }
}
