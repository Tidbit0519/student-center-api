using System.Text.Json.Serialization;
public class Student
{
    public virtual int StudentId { get; protected set; }
    public virtual string? FirstName { get; set; }
    public virtual string? LastName { get; set; }
    [JsonIgnore]
    public virtual ICollection<Grade>? Grades { get; set; }
}

public class StudentDTOWithId
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class StudentDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}