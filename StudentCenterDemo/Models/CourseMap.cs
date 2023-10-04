using FluentNHibernate.Mapping;
using StudentCenterDemo.Models;

namespace StudentCenterDemo.Mappings
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Table("Course"); // Set the name of the database table

            Id(x => x.Id, "Id").GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Credits);
            Map(x => x.ProfessorId);
        }
    }
}
