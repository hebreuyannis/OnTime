using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Appointments.Queries.GetAppointment;

namespace OnTime.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ISender _mediator, ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        /*[HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

        /// <summary>
        /// Get appointment
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAppointment")]
        public async Task<IActionResult> GetAppointment()
        {
            var query = new GetAppointmentQuery(Guid.NewGuid());

            var result = await _mediator.Send(query);

            return result.Match(appointment =>
            Ok(appointment),
            errors => Problem(statusCode: 404, title: errors[0].Description));
        }
    }
}
