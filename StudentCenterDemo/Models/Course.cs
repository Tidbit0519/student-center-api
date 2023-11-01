using System.Text.Json.Serialization;

public class Course
{
    public virtual int CourseId { get; protected set; }
    public virtual string CourseName { get; set; }
    public virtual string CourseCode { get; set; }
    public virtual int Credits { get; set; }
    public virtual int? ProfessorId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Grade>? Grades { get; set; }
}

public class CourseDTOWithId
{
    public virtual int CourseId { get; set; }
    public virtual string CourseName { get; set; }
    public virtual string CourseCode { get; set; }
    public virtual int Credits { get; set; }
    public virtual int? ProfessorId { get; set; }
}

public class CourseDTO
{
    public virtual string CourseName { get; set; }
    public virtual string CourseCode { get; set; }
    public virtual int Credits { get; set; }
    public virtual int? ProfessorId { get; set; }
}

