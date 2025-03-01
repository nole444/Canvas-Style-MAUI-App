using Microsoft.AspNetCore.Mvc;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Utilities;
using Library.LearningManagement.Models;
using Microsoft.Extensions.Logging;
using ServerLibrary.MyAPI.EC;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILogger<CoursesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]

        public IEnumerable<CoursesDTO> Get()
        {
            return new CoursesEC().Search();
        }

        [HttpGet("/{id}")]
        public CoursesDTO? GetId(int id)
        {
            return new CoursesEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public CoursesDTO? Delete(int id)
        {
            return new CoursesEC().Delete(id);
        }

        [HttpPost]
        public CoursesDTO? AddOrUpdate([FromBody] CoursesDTO client)
        {
            return new CoursesEC().AddOrUpdate(client);
        }

        [HttpPost("Search")]
        public IEnumerable<CoursesDTO> Search([FromBody] QueryMessage query)
        {
            return new CoursesEC().Search(query.Query);
        }
    }
}