using FluentNHibernate.Mapping;
using StudentCenterDemo.Models;

namespace StudentCenterDemo.Mappings
{
    public class EnrollmentMap : ClassMap<Enrollment>
    {
        public EnrollmentMap()
        {
            Table("Enrollment"); // Set the name of the database table

            Id(x => x.Id, "Id").GeneratedBy.Identity();
            Map(x => x.StudentId);
            Map(x => x.CourseId);
            Map(x => x.EnrollmentDate);
            Map(x => x.EnrollmentStatus);
        }
    }
}
