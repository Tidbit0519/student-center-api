namespace StudentCenterDemo.Models.Persistence
{
    public interface ICourseRepository
    {
        Course Get(int id);
        IEnumerable<Course> GetAll();
        Course Add(Course course);
        bool Update(Course course);
        void Delete(int id);
    }
}
