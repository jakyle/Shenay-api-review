using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web_Api_example.Model;

namespace Web_Api_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<string> Summaries = new List<string>()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Count)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult PostSummary([FromBody] SummaryPost model)
        {
            Summaries.Add(model.WeatherSummary);
            return Created("https://localhost:5001/WeatherForecast", model);
        }

        [HttpPut]
        public IActionResult PutSummary([FromBody] SummaryPut model)
        {
            Summaries[Summaries.FindIndex(ind => ind.Equals(model.CurrentSummary))] = model.NewSummary;
            return Accepted("https://localhost:5001/WeatherForecast", model);
        }

        [HttpDelete]
        public IActionResult DeleteSummary([FromBody] SummaryDelete model)
        {
            Summaries.Remove(model.WeatherSummary);
            return Ok();
        }
    }
}
