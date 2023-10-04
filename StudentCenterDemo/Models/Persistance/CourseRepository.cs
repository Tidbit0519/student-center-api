

using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Models.Persistance
{
    public class CourseRepository : ICourseRepository
    {
        private readonly NHibernate.ISession _session;

        public CourseRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public Course Add(Course course)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Save(course);
                transaction.Commit();
                return course;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // You can handle the exception as needed
            }
        }

        public void Delete(int courseId)
        {
            using var transaction = _session.BeginTransaction();
            var course = _session.Get<Course>(courseId);
            if (course != null)
            {
                _session.Delete(course);
                transaction.Commit();
            }
        }

        public Course Get(int courseId)
        {
            return _session.Get<Course>(courseId);
        }

        public IEnumerable<Course> GetAll()
        {
            return _session.Query<Course>().ToList();
        }

        public bool Update(Course course)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Clear();
                _session.Update(course);
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