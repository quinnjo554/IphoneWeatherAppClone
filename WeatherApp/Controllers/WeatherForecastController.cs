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
        public string date;

        public int temperatureC;

        public int temperatureF;

        public string summary;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //write a method for setting data
        public async Task getData()
        {
            //send api requst
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=78e51fbcb61a41c0aa605637232601&q=Bismarck&aqi=no");
            if (response.IsSuccessStatusCode)
            {
                //set the data to class variables
                string textResult = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<dynamic>(textResult);
                temperatureC = json.current.temp_c;
                temperatureF = json.current.temp_f;
                summary = json.current.condition.text;
                date = "today lol";
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
                    Date = date,
                    TemperatureC = temperatureC,
                    Summary = summary,
                }
            };
            return forecasts;
        }


    }
}