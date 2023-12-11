using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models.Persistence;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;

        public StudentController(IStudentRepository studentRepository, IGradeRepository gradeRepository)
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        [HttpGet("GetAllStudents")]
        public ActionResult<IEnumerable<StudentDTOWithId>> Get()
        {
            var students = _studentRepository.GetAll();
            var studentDTOs = students.Select(student => new StudentDTOWithId
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName
            }).ToList();

            return Ok(studentDTOs);
        }

        [HttpGet("GetStudentById/{id}")]
        public ActionResult<StudentDTO> Get(int id)
        {
            var student = _studentRepository.Get(id);
            if (student == null)
                return NotFound();
            var studentDTO = new StudentDTO
            {
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            return Ok(studentDTO);
        }

        [HttpGet("GetCoursesByStudentId/{studentId}")]
        public ActionResult<IEnumerable<CourseDTO>> GetStudentCourses(int studentId)
        {
            var studentCourses = _gradeRepository.GetStudentCourses(studentId);
            if (studentCourses == null || !studentCourses.Any())
            {
                return NotFound();
            }
            else
            {
                var courseDTOs = studentCourses.Select(course => new CourseDTOWithId
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    CourseCode = course.CourseCode,
                    Credits = course.Credits
                }).ToList();
                return Ok(courseDTOs);
            }
        }

        [HttpPost("CreateNewStudent")]
        public ActionResult<StudentDTO> Post([FromBody] IList<StudentDTO> studentDTOs)
        {
            if (studentDTOs == null || !studentDTOs.Any())
            {
                return BadRequest("Invalid student data");
            }

            var createdStudents = new List<StudentDTO>();

            foreach (var studentDTO in studentDTOs)
            {
                var student = new Student
                {
                    FirstName = studentDTO.FirstName,
                    LastName = studentDTO.LastName
                };
                _studentRepository.Add(student);
                createdStudents.Add(studentDTO);
            }

            return Ok(createdStudents);
        }

        [HttpPut("UpdateStudentById/{id}")]
        public ActionResult<Student> Put(int id, Student updatedStudent)
        {
            var existingStudent = _studentRepository.Get(id);
            if (existingStudent == null)
                return NotFound();
            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            // Update other properties as needed

            _studentRepository.Update(existingStudent);
            return Ok(existingStudent);
        }

        [HttpDelete("DeleteStudentById/{id}")]
        public ActionResult Delete(int id)
        {
            var student = _studentRepository.Get(id);
            if (student == null)
                return NotFound();
            _studentRepository.Delete(id);
            return NoContent();
        }
    }
}
