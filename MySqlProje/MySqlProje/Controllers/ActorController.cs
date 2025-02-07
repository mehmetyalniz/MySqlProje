using Microsoft.AspNetCore.Mvc;
using MySqlProje.Services;

namespace MySqlProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public ActorsController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
            var actors = _databaseService.GetActors();
            return Ok(actors);
        }
    }
}
