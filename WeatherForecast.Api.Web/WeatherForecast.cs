using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using WeatherForecast.Business.Handlers;

namespace WeatherForecast.Api.Web
{
    public static class WeatherForecast
    {
        [FunctionName("WeatherForecast")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "options", Route = "weather/{userId}")] HttpRequest req, string userId, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var remoteIpAddress = req.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(remoteIpAddress))
                return new BadRequestObjectResult("something bad happened, please try again");

            var force = false;
            if (bool.TryParse(req.Query["__force"], out var forceValue))
                force = forceValue;

            try
            {
                var weatherForecastHandler = new WeatherForecastHandler();
                var weatherForecastData = await weatherForecastHandler.GetResult(userId, remoteIpAddress, force);

                return weatherForecastData != null
                    ? (ActionResult)new OkObjectResult(weatherForecastData)
                    : new BadRequestObjectResult("something bad happened, please try again");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
