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

        public Professor Get(int professorId)
        {
            return _session.Query<Professor>().FirstOrDefault(s => s.ProfessorId == professorId);
        }

        public IEnumerable<Professor> GetAll()
        {
            return _session.Query<Professor>().ToList();
        }

        public Professor Add(Professor professor)
        {
            using var transaction = _session.BeginTransaction();
            _session.Save(professor);
            transaction.Commit();
            return professor;
        }

        public Professor Update(Professor professor)
        {
            using var transaction = _session.BeginTransaction();
            _session.Update(professor);
            transaction.Commit();
            return professor;
        }

        public void Delete(int professorId)
        {
            using var transaction = _session.BeginTransaction();
            var professor = Get(professorId);
            if (professor != null)
            {
                _session.Delete(professor);
            }
            transaction.Commit();
        }
    }
}