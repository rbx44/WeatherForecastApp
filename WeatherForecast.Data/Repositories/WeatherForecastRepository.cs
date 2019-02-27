
using System;
using System.Threading.Tasks;
using Dapper;
using WeatherForecast.Data.Models;
using WeatherForecast.Data.Providers;

namespace WeatherForecast.Data.Repositories
{
    public class WeatherForecastRepository
    {
        private readonly DbProvider _dbProvider;
        public WeatherForecastRepository()
        {
            _dbProvider = new DbProvider();
        }

        public async Task InsertUserHistory(WeatherForecastData data)
        {
            const string query = @"
                Declare @location int;
                
                If Not Exists(select Top 1 1 from dbo.[User] where UserID=@userId)
                Begin
                    Insert into dbo.[User] (UserId 
                        ,CreatedByName
                        ,CreatedOn
                        ,LastUpdatedByName
                        ,LastUpdatedOn)
                    values (@userId
                        ,'System'
                        ,getutcdate()
                        ,'System'
                        ,getutcdate())
                End;
                
                If Not Exists(select Top 1 1 from dbo.[Location] where city = @city)
                Begin
                    Insert into dbo.[Location] (City 
                        ,CreatedByName
                        ,CreatedOn
                        ,LastUpdatedByName
                        ,LastUpdatedOn)
                    values (@city
                        ,'System'
                        ,getutcdate()
                        ,'System'
                        ,getutcdate())
                End;
                
                Set @location = (Select LocationId from dbo.Location where city = @city);
                
                Insert into dbo.[User_Location] (UserId
                    ,LocationId
                    ,RequestedOn)
                values (@userId
                    ,@location
                    ,getutcdate());
                ";

            using (var connection = _dbProvider.Get())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(query, new
                        {
                            userId = data.UserId,
                            city = data.City
                        }, transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
