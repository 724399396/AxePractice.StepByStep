using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace Orm.Practice.Entities
{
    public class Parent
    {
        public virtual Guid ParentId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Child> Children { get; set; }
        public virtual bool IsForQuery { get; set; }
    }

    public class ParentMap : ClassMap<Parent>
    {
        public ParentMap()
        {
            #region Please modify the code to pass the test
            Not.LazyLoad();
            Table("Parent");
            Id(x => x.ParentId).Column("ParentID").GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name");
            HasMany(x => x.Children).KeyColumn("ParentID").Inverse().Cascade.AllDeleteOrphan().Not.LazyLoad();
            Map(x => x.IsForQuery).Column("IsForQuery");

            #endregion
        }
    }
}