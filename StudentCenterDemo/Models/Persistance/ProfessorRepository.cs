

using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo.Models.Persistance
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly NHibernate.ISession _session;

        public ProfessorRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public Professor Add(Professor professor)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Save(professor);
                transaction.Commit();
                return professor;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // You can handle the exception as needed
            }
        }

        public void Delete(int professorId)
        {
            using var transaction = _session.BeginTransaction();
            var professor = _session.Get<Professor>(professorId);
            if (professor != null)
            {
                _session.Delete(professor);
                transaction.Commit();
            }
        }

        public Professor Get(int professorId)
        {
            return _session.Get<Professor>(professorId);
        }

        public IEnumerable<Professor> GetAll()
        {
            return _session.Query<Professor>().ToList();
        }

        public bool Update(Professor professor)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                _session.Clear();
                _session.Update(professor);
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