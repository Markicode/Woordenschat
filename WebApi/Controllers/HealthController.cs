using Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/health")]

    public class HealthController : Controller
    { 
        private readonly LibraryContext _context;

        public HealthController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is running.");
        }

        [HttpGet("db")]
        public IActionResult CheckDatabase()
        {
            var canConnect = _context.Database.CanConnect();

            if(!canConnect)
            {
                return StatusCode(503, "Database is unreachable.");
            }

            return Ok("Database is reachable.");
        }
    }
}
