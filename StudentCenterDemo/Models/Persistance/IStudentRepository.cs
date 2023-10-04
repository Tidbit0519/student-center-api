namespace StudentCenterDemo.Models.Persistence
{
    public interface IStudentRepository
    {
        Student Get(int studentId);
        IEnumerable<Student> GetAll();
        Student Add(Student student);
        bool Update(Student student);
        void Delete(int id);
    }
}
