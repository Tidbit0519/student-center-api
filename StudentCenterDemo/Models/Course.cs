using System.ComponentModel.DataAnnotations;

namespace StudentCenterDemo.Models
{
    public class Course
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Code { get; set; }
        [Required]
        public virtual int Credits { get; set; }
        [Required]
        public virtual int ProfessorId { get; set; }
    }
}
