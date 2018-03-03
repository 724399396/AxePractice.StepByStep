using System;
using FluentNHibernate.Mapping;

namespace one_to_one.Entity
{
    public class Employee
    {
        public virtual Guid PersonId { get; set; }
        public virtual string Role { get; set; }
        public virtual Person Person { get; set; }
        public virtual bool IsForQuery { get; } = false;
    }

    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("employee");
            Id(x => x.PersonId).Column("PersonId").GeneratedBy.Assigned();
            Map(x => x.Role).Column("Role");
            Map(x => x.IsForQuery).Column("IsForQuery");
            HasOne(x => x.Person).Constrained().ForeignKey(null).Cascade.All();
        }
    }
}