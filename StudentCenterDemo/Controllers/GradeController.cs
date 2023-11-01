using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public GradeController(IGradeRepository gradeRepository, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet("GetAllGradesByCourseId/{courseId}")]
        public ActionResult<IEnumerable<Grade>> GetAll(int courseId)
        {
            var grades = _gradeRepository.GetAll(courseId);
            return Ok(grades);
        }

        [HttpGet("GetStudentGrade/{studentId}/{courseId}")]
        public ActionResult<Grade> Get(int studentId, int courseId)
        {
            var grade = _gradeRepository.Get(studentId, courseId);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        [HttpPost("CreateNewGrade")]
        public ActionResult<Grade> Post([FromBody] Grade grade)
        {
            if (grade == null)
                return BadRequest("Invalid grade data");

            var studentExists = _studentRepository.Get(grade.StudentId);
            var courseExists = _courseRepository.Get(grade.CourseId);

            if (studentExists == null || courseExists == null)
            {
                return BadRequest("Invalid studentId or courseId");
            }

            _gradeRepository.Add(grade);
            return CreatedAtAction("GetStudentGrade", new { studentId = grade.StudentId, courseId = grade.CourseId }, grade);
        }

        [HttpPut("UpdateStudentGrade/{studentId}/{courseId}")]
        public ActionResult<Grade> Put(int studentId, int courseId, [FromBody] GradeDTOStudentGrade updatedGrade)
        {
            var existingGrade = _gradeRepository.Get(studentId, courseId);
            if (existingGrade == null)
                return NotFound();

            if (updatedGrade.GradeLetter != null)
            {
                existingGrade.GradeLetter = updatedGrade.GradeLetter;
                _gradeRepository.Update(existingGrade);
            }

            return Ok(existingGrade);
        }

        [HttpDelete("DeleteStudentGrade/{studentId}/{courseId}")]
        public ActionResult Delete(int studentId, int courseId)
        {
            var grade = _gradeRepository.Get(studentId, courseId);
            if (grade == null)
                return NotFound();

            _gradeRepository.Delete(studentId, courseId);
            return NoContent();
        }
    }
}
