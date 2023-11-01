using FluentNHibernate.Mapping;

namespace StudentCenterDemo.Mappings
{
    public class GradeMap : ClassMap<Grade>
    {
        public GradeMap()
        {
            Id(x => x.GradeId);
            Map(x => x.CourseId);
            Map(x => x.StudentId);
            Map(x => x.GradeLetter);
        }
    }
}
