

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

        public Student Add(Student student)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Save(student);
                transaction.Commit();
                return student;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // You can handle the exception as needed
            }
        }

        public void Delete(int id)
        {
            using var transaction = _session.BeginTransaction();
            var student = _session.Get<Student>(id);
            if (student != null)
            {
                _session.Delete(student);
                transaction.Commit();
            }
        }

        public Student Get(int emplid)
        {
            return _session.Get<Student>(emplid);
        }

        public IEnumerable<Student> GetAll()
        {
            return _session.Query<Student>().ToList();
        }

        public bool Update(Student student)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Clear();
                _session.Update(student);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }


    }
}