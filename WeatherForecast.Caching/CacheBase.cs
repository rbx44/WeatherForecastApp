namespace WeatherForecast.Caching
{
    public abstract class CacheBase
    {
        protected virtual int TtlSeconds => 3600;
    }
}
