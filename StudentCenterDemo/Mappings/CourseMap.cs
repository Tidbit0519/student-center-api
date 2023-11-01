using FluentNHibernate.Mapping;

namespace StudentCenterDemo.Mappings
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Table("Course");
            Id(x => x.CourseId);
            Map(x => x.CourseName);
            Map(x => x.CourseCode);
            Map(x => x.Credits);
            Map(x => x.ProfessorId);
            HasMany(x => x.Grades)
               .KeyColumn("CourseId")
               .Inverse()
               .Cascade.All()
               .AsBag();
        }
    }
}
