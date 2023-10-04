

using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Models.Persistance
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly NHibernate.ISession _session;

        public EnrollmentRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public Enrollment Add(Enrollment enrollment)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Save(enrollment);
                transaction.Commit();
                return enrollment;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // You can handle the exception as needed
            }
        }

        public void Delete(int enrollmentId)
        {
            using var transaction = _session.BeginTransaction();
            var enrollment = _session.Get<Enrollment>(enrollmentId);
            if (enrollment != null)
            {
                _session.Delete(enrollment);
                transaction.Commit();
            }
        }

        public Enrollment Get(int enrollmentId)
        {
            return _session.Get<Enrollment>(enrollmentId);
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return _session.Query<Enrollment>().ToList();
        }

        public bool Update(Enrollment enrollment)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Clear();
                _session.Update(enrollment);
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