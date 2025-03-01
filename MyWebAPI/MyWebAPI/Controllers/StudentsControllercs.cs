using Microsoft.AspNetCore.Mvc;
using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;
using Microsoft.Extensions.Logging;
using ServerLibrary.MyAPI.EC;
using Library.LearningManagement.Utilities;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly StudentsEC _studentsEC;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
            _studentsEC = new StudentsEC();  // Initialize the Students entity controller
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> Get()
        {
            return Ok(_studentsEC.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetId(int id)
        {
            var student = _studentsEC.Get(id);
            return student != null ? Ok(student) : NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<StudentDTO> Delete(int id)
        {
            var student = _studentsEC.Delete(id);
            return student != null ? Ok(student) : NotFound();
        }

        [HttpPost]
        public ActionResult<StudentDTO> AddOrUpdate([FromBody] StudentDTO studentDto)
        {
            var student = _studentsEC.AddOrUpdate(studentDto);
            return Ok(student);
        }

        [HttpPost("Search")]
        public ActionResult<IEnumerable<StudentDTO>> Search([FromBody] QueryMessage query)
        {
            var results = _studentsEC.Search(query.Query);
            return Ok(results);
        }
    }
}