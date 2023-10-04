using FluentNHibernate.Mapping;
using StudentCenterDemo.Models;

namespace StudentCenterDemo.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Student"); // Set the name of the database table

            Id(x => x.Id, "Id").GeneratedBy.Identity();
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}
