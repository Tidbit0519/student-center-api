using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models.Persistance;
using StudentCenterDemo.Models.Persistence;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IProfessorRepository _professorRepository;

        public CourseController(ICourseRepository courseRepository, IProfessorRepository professorRepository)
        {
            _courseRepository = courseRepository;
            _professorRepository = professorRepository;
        }

        [HttpGet("GetAllCourses")]
        public ActionResult<IEnumerable<CourseDTOWithId>> Get()
        {
            var courses = _courseRepository.GetAll();
            var courseDTOs = courses.Select(course => new CourseDTOWithId
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                Credits = course.Credits,
                ProfessorId = course.ProfessorId
            }).ToList();
            return Ok(courseDTOs);
        }

        [HttpGet("GetCourseById/{id}")]
        public ActionResult<CourseDTO> Get(int id)
        {
            var course = _courseRepository.Get(id);
            if (course == null)
                return NotFound(); // Return 404 Not Found for non-existent courses
            return Ok(course);
        }

        [HttpPost("CreateNewCourse")]
        public ActionResult<IEnumerable<CourseDTO>> Post([FromBody] IList<CourseDTO> courses)
        {
            if (courses == null || !courses.Any())
            {
                return BadRequest("Invalid course data"); // Return 400 Bad Request for invalid data
            }

            var createdCourses = new List<CourseDTO>();

            foreach (var course in courses)
            {
                if (_professorRepository.Get((int)course.ProfessorId) == null)
                {
                    return BadRequest("Invalid ProfessorId"); // Return 400 Bad Request for invalid ProfessorId
                }

                var newCourse = new Course
                {
                    CourseName = course.CourseName,
                    CourseCode = course.CourseCode,
                    Credits = course.Credits,
                    ProfessorId = course.ProfessorId,
                };

                _courseRepository.Add(newCourse);
                createdCourses.Add(course);
            }

            return CreatedAtAction("GetAllCourses", createdCourses); // Return 201 Created with a link to the newly created resources
        }

        [HttpPut("UpdateCourseById/{id}")]
        public ActionResult<Course> Put(int id, Course updatedCourse)
        {
            var existingCourse = _courseRepository.Get(id);
            if (existingCourse == null)
                return NotFound(); // Return 404 Not Found for non-existent courses

            if (_professorRepository.Get((int)updatedCourse.ProfessorId) == null)
            {
                return BadRequest("Invalid ProfessorId"); // Return 400 Bad Request for invalid ProfessorId
            }

            existingCourse.CourseName = updatedCourse.CourseName;
            existingCourse.CourseCode = updatedCourse.CourseCode;
            existingCourse.Credits = updatedCourse.Credits;
            existingCourse.ProfessorId = updatedCourse.ProfessorId;

            _courseRepository.Update(existingCourse);
            return Ok(existingCourse);
        }

        [HttpDelete("DeleteCourseById/{id}")]
        public ActionResult Delete(int id)
        {
            var course = _courseRepository.Get(id);
            if (course == null)
                return NotFound(); // Return 404 Not Found for non-existent courses

            _courseRepository.Delete(id);
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
    }
}
