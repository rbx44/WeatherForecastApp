# WeatherForecastApp
Weather Forecast Api allows user to get weather high and lows of the current day and the future days.


## Site Url
* https://weatherforecastapiweb.azurewebsites.net/

## How to?
* Hit the endpoint see below (site url + api endpoint) using Azure Function key with `x-functions-key` as header key. **Ask for header key**.
* UserId can be any string that will be passed on the url.
* E.g of a request -  `https://weatherforecastapiweb.azurewebsites.net/api/weather/testuser` with Header `x-functions-key` and Function key as value.
* **Ask for SQL access** to see or run the records for running analytics query.
* Create SQL schema script is included in the source `Create Scripts v1.sql` and database diagram `Database Diagram.JPG`  


## Implementation Details  
* Function App is under Consumption plan - which auto scales based on consumtpion. 
* Calling the Api first time after it goes to sleep could take some time as it spins up the resources.
* Uses Redis in Azure for caching
* Uses Azure Functions Http Function for WebApi
* Uses Azure Function level Security
* Uses Azure SQL Server
* Response is camelCased.
* Response is cached for 2 hours for future request(s) made from the same location.
* Writes history of successful invocations for analytics in SQL Server.
* Returns high and lows of ranges from utc today -1 to utc today +3. Total 5 records. Can be filtered in the client with something like MomentJS library to convert to local datetime.


## Api Endpoint
| Endpoint  | Status | Response
| ------------- | ------------- | ------------- |
| api/weather/{userId}  | Success 200 | $Ref: `Response Payload`: WeatherForecast |
| api/weather/{userId}  | Failure 400  | Error Message |

## Query String Params

| Query String Params (optional)
| ------------- |
|`__force`: boolean - A bool to bypass the cache |

## Response Payload

### WeatherForecast
  
  | Name | Type | Description
  | ------------- | ------------- | ------------- |
  | City  | String | City of the current user |
  | State  | String  | State of the current user |
  | DailyWeather  | WeatherInformation[]  | Weather description and high & low temperatures |
  
  
### WeatherInformation
  
  | Name | Type | Description
  | ------------- | ------------- | ------------- |
  | Description  | String | Weather Description |
  | HighTemperature  | Decimal  | High Temperature of the day |
  | LowTemperature  | Decimal  | Low Temperature of the day |
     
     
