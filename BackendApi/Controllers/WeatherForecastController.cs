using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Summaries);
        }

        [HttpGet("{index:int}")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Invalid index.");
            }
            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var matches = Summaries.Where(s => s.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matches.Count == 0)
            {
                return NotFound("Name not found.");
            }
            return Ok(matches.Count);
        }

        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int? sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            if (sortStrategy == 1)
            {
                return Ok(Summaries.OrderBy(s => s));
            }
            if (sortStrategy == -1)
            {
                return Ok(Summaries.OrderByDescending(s => s));
            }
            return BadRequest("Invalid sortStrategy parameter.");
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Invalid index.");
            }
            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete("{index:int}")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Invalid index.");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}
