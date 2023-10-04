using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models;
using StudentCenterDemo.Models.Persistance;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> Get(int id)
        {
            var enrollment = _enrollmentRepository.Get(id);
            if (enrollment == null)
                return NotFound();

            return Ok(enrollment);
        }

        [HttpGet("getAllEnrollments")]
        public ActionResult<IEnumerable<Enrollment>> GetAll()
        {
            var enrollments = _enrollmentRepository.GetAll();
            return Ok(enrollments);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Enrollment> enrollments)
        {
            if (enrollments == null || enrollments.Count == 0)
            {
                return BadRequest("No enrollment data received.");
            }

            try
            {
                foreach (var item in enrollments)
                {
                    var enrollment = new Enrollment
                    {
                        StudentId = item.StudentId,
                        CourseId = item.CourseId,
                        EnrollmentDate = item.EnrollmentDate,
                        EnrollmentStatus = item.EnrollmentStatus,
                    };

                    _enrollmentRepository.Add(enrollment);
                }
                return Ok("Enrollments added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Enrollment enrollment)
        {
            if (enrollment == null || id != enrollment.Id)
                return BadRequest();

            var existingEnrollment = _enrollmentRepository.Get(id);
            if (existingEnrollment == null)
                return NotFound();

            _enrollmentRepository.Update(enrollment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var enrollment = _enrollmentRepository.Get(id);
            if (enrollment == null)
                return NotFound();

            _enrollmentRepository.Delete(id);
            return NoContent();
        }
    }
}
