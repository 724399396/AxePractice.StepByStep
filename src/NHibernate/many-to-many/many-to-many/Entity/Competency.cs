using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace one_to_one.Entity
{
    public class Competency
    {
        public virtual Guid CompetencyId { get; set; }
        public virtual string Title { get; set; }
        public virtual bool IsForQuery { get; } = false;
        public virtual IList<Person> Persons { get; set; }
    }

    public class CompetencyMap : ClassMap<Competency>
    {
        public CompetencyMap()
        {
            Table("competency");
            Id(x => x.CompetencyId).Column("CompetencyId").GeneratedBy.Guid();
            Map(x => x.Title).Column("Title");
            Map(x => x.IsForQuery).Column("IsForQuery");
            HasManyToMany(x => x.Persons)
                .Cascade.All()
                .Table("person_x_competency")
                .ParentKeyColumn("CompetencyId")
                .ChildKeyColumn("ParentId");
        }
    }
}