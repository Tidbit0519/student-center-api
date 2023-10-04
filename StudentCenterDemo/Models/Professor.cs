using System.ComponentModel.DataAnnotations;

namespace StudentCenterDemo.Models
{
    public class Professor
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }
        // Add other properties as needed
    }
}
