using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //place data in a class then call the class instead
        weatherWrapper weatherVars = new weatherWrapper();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public string MillitaryToregual(string time)
        {
            DateTime dateTime = DateTime.Parse(time);
            return dateTime.ToString("h:mm tt");
        }

        //write a method for setting data
        public async Task getData()
        {
            //send api requst
            HttpClient client = new HttpClient();
            //
            //TO:DO add a feature to change location
            //
            var response = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=78e51fbcb61a41c0aa605637232601&q=Fargo&aqi=no");
            if (response.IsSuccessStatusCode)
            {
                //set the data to class variables
                string textResult = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<dynamic>(textResult);
                weatherVars.temperatureC = json.current.temp_c;
                weatherVars.temperatureF = json.current.temp_f;
                weatherVars.summary = json.current.condition.text;
                weatherVars.date = json.location.localtime;
                weatherVars.location = json.location.name;
                weatherVars.condition = json.current.condition.text;
                //get date in correct format
                weatherVars.date = this.MillitaryToregual(weatherVars.date);
                Console.WriteLine(json);
            }
            
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
           await this.getData();
           WeatherForecast[] forecasts = new WeatherForecast[]{
                new WeatherForecast
                {
                    Date = weatherVars.date,
                    TemperatureC = weatherVars.temperatureC,
                    Summary = weatherVars.summary,
                    Location = weatherVars.location,
                    Condition = weatherVars.condition,
                }
            };
            return forecasts;
        }


    }
}