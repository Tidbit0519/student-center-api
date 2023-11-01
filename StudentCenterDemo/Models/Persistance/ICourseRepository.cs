namespace StudentCenterDemo.Models.Persistence
{
    public interface ICourseRepository
    {
        Course Get(int id);
        IEnumerable<Course> GetAll();
        IEnumerable<Course> GetCoursesByProfessor(int professorId);
        Course Add(Course course);
        bool Update(Course course);
        void Delete(int id);
    }
}
