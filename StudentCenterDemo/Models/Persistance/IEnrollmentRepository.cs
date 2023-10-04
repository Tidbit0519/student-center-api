namespace StudentCenterDemo.Models.Persistence
{
    public interface IEnrollmentRepository
    {
        Enrollment Get(int enrollmentId);
        IEnumerable<Enrollment> GetAll();
        Enrollment Add(Enrollment enrollment);
        bool Update(Enrollment enrollment);
        void Delete(int id);
    }
}
