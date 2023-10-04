using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _studentRepository.Get(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("getAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            var students = _studentRepository.GetAll();
            return Ok(students);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                return BadRequest("No student data received.");
            }

            try
            {
                foreach (var person in students)
                {
                    var student = new Student
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                    };

                    _studentRepository.Add(student);
                }
                return Ok("Students added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if (student == null || id != student.Id)
                return BadRequest();

            var existingStudent = _studentRepository.Get(id);
            if (existingStudent == null)
                return NotFound();

            _studentRepository.Update(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _studentRepository.Get(id);
            if (student == null)
                return NotFound();

            _studentRepository.Delete(id);
            return NoContent();
        }
    }
}
