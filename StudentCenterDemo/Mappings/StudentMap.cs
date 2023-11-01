using FluentNHibernate.Mapping;

namespace StudentCenterDemo.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student");
            Id(x => x.StudentId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            HasMany(x => x.Grades)
                .KeyColumn("StudentId")
                .Inverse()
                .Cascade.All();
        }
    }
}
