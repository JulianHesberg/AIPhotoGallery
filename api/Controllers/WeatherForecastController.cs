using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        public WeatherForecastController()
        {
            
        }

        [HttpGet(Name = "")]
        public void Get()
        {
            
        }
    }
}
