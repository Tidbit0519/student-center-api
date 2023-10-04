namespace StudentCenterDemo.Models.Persistence
{
    public interface IProfessorRepository
    {
        Professor Get(int professorId);
        IEnumerable<Professor> GetAll();
        Professor Add(Professor professor);
        bool Update(Professor professor);
        void Delete(int professorId);
    }
}
