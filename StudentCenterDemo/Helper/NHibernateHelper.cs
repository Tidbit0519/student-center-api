using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using StudentCenterDemo.Models;
using NHibernate.Tool.hbm2ddl;

namespace StudentCenterDemo.Helper
{
    public class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;

        public NHibernateHelper()
        {
            CreateSessionFactory();
        }

        private void CreateSessionFactory()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentCenterDemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Professor>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();

        }

        public NHibernate.ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}