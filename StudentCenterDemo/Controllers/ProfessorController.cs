using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models.Persistence;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ICourseRepository _courseRepository;

        public ProfessorController(IProfessorRepository professorRepository, ICourseRepository courseRepository)
        {
            _professorRepository = professorRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet("GetAllProfessors")]
        public ActionResult<IEnumerable<ProfessorDTOWithID>> Get()
        {
            var professors = _professorRepository.GetAll();
            return Ok(professors);
        }

        [HttpGet("GetProfessorById/{id}")]
        public ActionResult<ProfessorDTO> Get(int id)
        {
            var professor = _professorRepository.Get(id);
            if (professor == null)
                return NotFound(); // Return 404 Not Found for non-existent professors
            var professorDTO = new ProfessorDTO
            {
                FirstName = professor.FirstName,
                LastName = professor.LastName
            };

            return Ok(professorDTO);
        }

        [HttpGet("GetCoursesByProfessorId/{id}")]
        public ActionResult<IEnumerable<CourseDTO>> GetCoursesByProfessor(int id)
        {
            var courses = _courseRepository.GetCoursesByProfessor(id);
            if (courses == null || !courses.Any())
            {
                return NotFound(); // Return 404 Not Found for professors with no courses
            }
            else
            {
                var courseDTOs = courses.Select(course => new CourseDTO
                {
                    CourseName = course.CourseName,
                    CourseCode = course.CourseCode,
                    Credits = course.Credits
                }).ToList();
                return Ok(courseDTOs);
            }
        }

        [HttpPost("CreateNewProfessor")]
        public ActionResult<IEnumerable<Professor>> Post([FromBody] IList<Professor> professors)
        {
            if (professors == null || !professors.Any())
            {
                return BadRequest("Invalid professor data"); // Return 400 Bad Request for invalid data
            }

            var createdProfessors = new List<Professor>();

            foreach (var professor in professors)
            {
                _professorRepository.Add(professor);
                createdProfessors.Add(professor);
            }

            return CreatedAtAction("GetAllProfessors", createdProfessors); // Return 201 Created with a link to the newly created resources
        }

        [HttpPut("UpdateProfessorById/{id}")]
        public ActionResult<Professor> Put(int id, Professor updatedProfessor)
        {
            var existingProfessor = _professorRepository.Get(id);
            if (existingProfessor == null)
                return NotFound(); // Return 404 Not Found for non-existent professors

            existingProfessor.FirstName = updatedProfessor.FirstName;
            existingProfessor.LastName = updatedProfessor.LastName;

            _professorRepository.Update(existingProfessor);
            return Ok(existingProfessor);
        }

        [HttpDelete("DeleteProfessorById/{id}")]
        public ActionResult Delete(int id)
        {
            var professor = _professorRepository.Get(id);
            if (professor == null)
                return NotFound(); // Return 404 Not Found for non-existent professors

            _professorRepository.Delete(id);
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
    }
}
