 using FluentNHibernate.Mapping;

namespace StudentCenterDemo.Mappings
{
    public class ProfessorMap : ClassMap<Professor>
    {
        public ProfessorMap()
        {
            Table("Professor");
            Id(x => x.ProfessorId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            HasMany(x => x.Courses)
               .KeyColumn("ProfessorId")
               .Inverse()
               .Cascade.SaveUpdate()
               .AsBag();
        }
    }
}
