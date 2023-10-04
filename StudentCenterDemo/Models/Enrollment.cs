namespace StudentCenterDemo.Models
{
    public class Enrollment
    {
        public virtual int Id { get; set; }
        public virtual int StudentId { get; set; }
        public virtual int CourseId { get; set; }
        public virtual string EnrollmentDate { get; set; }
        public virtual string EnrollmentStatus { get; set; }
    }
}
