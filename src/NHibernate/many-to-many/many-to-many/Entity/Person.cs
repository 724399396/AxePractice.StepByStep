using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace one_to_one.Entity
{
    public class Person
    {
        public virtual Guid PersonId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Competency> Competencies { get; set; }
        public virtual bool IsForQuery { get; } = false;
    }

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("person");
            Id(x => x.PersonId).Column("PersonId").GeneratedBy.Guid();
            Map(x => x.Name).Column("Name");
            Map(x => x.IsForQuery).Column("IsForQuery");
            HasManyToMany(x => x.Competencies)
                .Cascade.All()
                .Table("person_x_competency")
                .ParentKeyColumn("ParentId")
                .ChildKeyColumn("CompetencyId");
        }
    }
}