namespace InMemoryDatabase.Sample.Controllers
{
    using System.Collections.Generic;
    using InMemoryDatabase.Sample.Models;
    using InMemoryDatabase.Sample.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> Get(string name = null, int? classNumber = null, int? studentNumber = null)
        {
            return Ok(_studentRepository.Get(name, classNumber, studentNumber));
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<StudentDTO> Get(string id)
        {
            try
            {
                var student = _studentRepository.Get(id);
                return Ok(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Student
        [HttpPost]
        public ActionResult<string> Post([FromBody] StudentDTO value)
        {
            return Ok(_studentRepository.Save(value));
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] StudentDTO value)
        {
            _studentRepository.Update(value);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _studentRepository.Delete(id);
            return NoContent();
        }
    }
}