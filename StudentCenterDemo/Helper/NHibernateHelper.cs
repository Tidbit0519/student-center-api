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
            var connectionString = "Server=eis-learning.mysql.database.azure.com;"
                     + "Database=defaultdb;"
                     + "Uid=jtbt1999;"
                     + "Pwd=Jason@990519;"
                     + "SslMode=Required;"
                     + "CertificateFile=C:\\Users\\jtbt1999\\Desktop\\dev\\StudentCenterDemo\\StudentCenterDemo;";

            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql())
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
