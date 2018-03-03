using System;
using FluentNHibernate.Mapping;

namespace one_to_one.Entity
{
    public class Person
    {
        public virtual Guid PersonId { get; set; }
        public virtual string Name { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual bool IsForQuery { get; } = false;
    }

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("person");
            Id(x => x.PersonId).Column("PersonId").GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name");
            Map(x => x.IsForQuery).Column("IsForQuery");
            HasOne(x => x.Employee).Constrained().ForeignKey(null).Cascade.All();
        }
    }
}