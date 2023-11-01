using System.Text.Json.Serialization;

public class Grade
{
    public virtual int GradeId { get; protected set; }
    public virtual int CourseId { get; set; }
    public virtual int StudentId { get; set; } 
    public virtual string? GradeLetter { get; set; }

}

public class GradeDTOAllGrades
{
    public virtual int StudentId { get; set; }
    public virtual string? GradeLetter { get; set; }
}

public class GradeDTOStudentGrade
{
    public virtual string? GradeLetter { get; set; }
}
