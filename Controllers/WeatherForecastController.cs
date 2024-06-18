using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace AppInsightTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly TelemetryClient _telemetryClient;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            //add to app insights telemetry
            _telemetryClient.TrackEvent("Get weather forecast");
            //log message to telemetry
            _telemetryClient.TrackTrace("Get API is called!", SeverityLevel.Information);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
