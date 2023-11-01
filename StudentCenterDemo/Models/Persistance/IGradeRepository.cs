namespace StudentCenterDemo.Models.Persistence
{
    public interface IGradeRepository
    {
        Grade Get(int studentId, int courseId);
        IEnumerable<Grade> GetAll (int courseId);
        void Add(Grade grade);
        void Update(Grade grade);
        void Delete(int studentId, int courseId);
        IEnumerable<Course> GetStudentCourses(int studentId);
    }
}
