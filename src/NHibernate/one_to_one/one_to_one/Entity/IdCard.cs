using System;
using FluentNHibernate.Mapping;

namespace one_to_one.Entity
{
    public class IdCard
    {
        public virtual Guid PersonId { get; set; }
        public virtual string Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual bool IsForQuery { get; } = false;
    }

    public class IdCardMap : ClassMap<IdCard>
    {
        public IdCardMap()
        {
            Table("idcard");
            Id(x => x.PersonId).Column("PersonId").GeneratedBy.Assigned();
            Map(x => x.Id).Column("Id");
            Map(x => x.IsForQuery).Column("IsForQuery");
            HasOne(x => x.Person).Cascade.None();
        }
    }
}