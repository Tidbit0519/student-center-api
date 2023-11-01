using System.Text.Json.Serialization;

public class Professor
{
    public virtual int ProfessorId { get; protected set; }
    public virtual string? FirstName { get; set; }
    public virtual string? LastName { get; set; }
    [JsonIgnore]
    public virtual ICollection<Course>? Courses { get; set; }
}

public class ProfessorDTOWithID
{
    public int ProfessorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class ProfessorDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}