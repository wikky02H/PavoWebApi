using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PavoWeb.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        public IEnumerable<object> Get()
        {
            //string connectionString = @"Server=localhost;Database=pavo;Trusted_Connection=True;TrustServerCertificate=True;";
            //string connectionString = @"Server=DESKTOP-22LTFL0\SQLEXPRESS;Database=pavo;User ID=sa;Password=Welcome2VH!@#;TrustServerCertificate=True;";
            string connectionString = @"Server=DESKTOP-22LTFL0\SQLEXPRESS;Database=pavo;Trusted_Connection=True;TrustServerCertificate=True;";


            var menus = new List<object>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var query = "SELECT * FROM Menus";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Adjust to match your `Menus` table structure
                                menus.Add(new
                                {
                                    Id = reader["Id"],
                                    Name = reader["Name"],
                                    Type = reader["Type"],
                                    OrderNo = reader["OrderNo"]
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as required
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return menus;
        }
    }
}
