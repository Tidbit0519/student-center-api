

using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Models.Persistance
{
    public class StudentRepository : IStudentRepository
    {
        private readonly NHibernate.ISession _session;

        public StudentRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public Student Get(int studentId)
        {
            return _session.Query<Student>().FirstOrDefault(s => s.StudentId == studentId);
        }

        public IEnumerable<Student> GetAll()
        {
            return _session.Query<Student>().ToList();
        }

        public Student Add(Student student)
        {
            using var transaction = _session.BeginTransaction();
            _session.Save(student);
            transaction.Commit();
            return student;
        }

        public Student Update(Student student)
        {
            using var transaction = _session.BeginTransaction();
            _session.Update(student);
            transaction.Commit();
            return student;
        }

        public void Delete(int studentId)
        {
            using var transaction = _session.BeginTransaction();
            var student = Get(studentId);
            if (student != null)
            {
                _session.Delete(student);
            }
            transaction.Commit();
        }
    }
}