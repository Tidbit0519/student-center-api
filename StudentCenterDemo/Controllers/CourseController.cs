using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models;
using StudentCenterDemo.Models.Persistance;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            var course = _courseRepository.Get(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpGet("getAllCourses")]
        public ActionResult<IEnumerable<Course>> GetAll()
        {
            var courses = _courseRepository.GetAll();
            return Ok(courses);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Course> courses)
        {
            if (courses == null || courses.Count == 0)
            {
                return BadRequest("No course data received.");
            }

            try
            {
                foreach (var item in courses)
                {
                    var course = new Course
                    {
                        Name = item.Name,
                        Code = item.Code,
                        Credits = item.Credits,
                        ProfessorId = item.ProfessorId,
                    };

                    _courseRepository.Add(course);
                }
                return Ok("Courses added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course course)
        {
            if (course == null || id != course.Id)
                return BadRequest();

            var existingCourse = _courseRepository.Get(id);
            if (existingCourse == null)
                return NotFound();

            _courseRepository.Update(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _courseRepository.Get(id);
            if (course == null)
                return NotFound();

            _courseRepository.Delete(id);
            return NoContent();
        }
    }
}
