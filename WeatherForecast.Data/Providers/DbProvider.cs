using System;
using System.Data;
using System.Data.SqlClient;

namespace WeatherForecast.Data.Providers
{
    public class DbProvider
    {
        public IDbConnection Get() => new SqlConnection(GetSqlConnectionString("WeatherWarehouse"));

        private static string GetSqlConnectionString(string name)
        {
            var conStr = Environment.GetEnvironmentVariable($"ConnectionStrings:{name}", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(conStr))
                conStr = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);

            return conStr;
        }
    }
}
