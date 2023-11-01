namespace StudentCenterDemo.Models.Persistence
{
    public class GradeRepository : IGradeRepository
    {
        private readonly NHibernate.ISession _session;

        public GradeRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public Grade Get(int studentId, int courseId)
        {
            return _session.QueryOver<Grade>()
                .Where(g => g.StudentId == studentId && g.CourseId == courseId)
                .SingleOrDefault();
        }

        public IEnumerable<Grade> GetAll(int courseId)
        {
            return _session.QueryOver<Grade>()
                .Where(g => g.CourseId == courseId)
                .List();
        }

        public void Add(Grade grade)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(grade);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Grade grade)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(grade);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(int studentId, int courseId)
        {
            var grade = Get(studentId, courseId);
            if (grade != null)
            {
                using (var transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Delete(grade);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IEnumerable<Course> GetStudentCourses(int studentId)
{
            return _session.Query<Course>()
                .Where(c => c.Grades.Any(g => g.StudentId == studentId))
                .ToList();
        }
    }
}
