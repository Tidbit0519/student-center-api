using Microsoft.AspNetCore.Mvc;
using StudentCenterDemo.Models;
using StudentCenterDemo.Models.Persistance;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Controllers
{
    [Route("api/professors")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Professor> Get(int id)
        {
            var professor = _professorRepository.Get(id);
            if (professor == null)
                return NotFound();

            return Ok(professor);
        }

        [HttpGet("getAllProfessors")]
        public ActionResult<IEnumerable<Professor>> GetAll()
        {
            var professors = _professorRepository.GetAll();
            return Ok(professors);
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Professor> professors)
        {
            if (professors == null || professors.Count == 0)
            {
                return BadRequest("No professor data received.");
            }

            try
            {
                foreach (var person in professors)
                {
                    var professor = new Professor
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                    };

                    _professorRepository.Add(professor);
                }
                return Ok("Professors added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            if (professor == null || id != professor.Id)
                return BadRequest();

            var existingProfessor = _professorRepository.Get(id);
            if (existingProfessor == null)
                return NotFound();

            _professorRepository.Update(professor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _professorRepository.Get(id);
            if (professor == null)
                return NotFound();

            _professorRepository.Delete(id);
            return NoContent();
        }
    }
}
