using System;

namespace WeatherForecast.Business.Exceptions
{
    public class BadApiResponseException : Exception
    {
        public BadApiResponseException(string message) : base(message)
        {
        }
    }
}
