using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using one_to_one.Entity;
using Xunit;
using Xunit.Abstractions;

namespace one_to_one
{
    public class OneToOneQueryFact : FactBase
    {
        public OneToOneQueryFact(ITestOutputHelper output) : base(output)
        {
            ExecuteNonQuery("DELETE FROM [dbo].[person] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[idcard] WHERE IsForQuery=0");
        }

        [Fact]
        public void should_get_id_card_by_person()
        {
            var person = Session.Query<Person>()
                .Single(p => p.Name == "A");

            Assert.Equal(
                "1234567890", 
                person.IdCard.Id);
        }

        [Fact]
        public void should_get_person_by_id_card()
        {
            var idCard = Session.Query<IdCard>()
                .Single(p => p.Id == "abcdefghijk");

            Assert.Equal(
                "B",
                idCard.Person.Name);
        }

        [Fact]
        public void should_insert_person_and_id_cad()
        {
            SavePersonAndIdCard(
                "nq-person-1", "new-id");
            Session.Clear();

            var insertedPerson = Session.Query<Person>()
                .Fetch(p => p.IdCard)
                .Single(p => !p.IsForQuery);

            Assert.Equal(
                "new-id",
                insertedPerson.IdCard.Id);
        }

        [Fact]
        public void should_delete_in_a_cascade_manner()
        {
            SavePersonAndIdCard(
                "nq-person-1",
                "new-id");
            Session.Clear();

            DeletePersonAndIdCard("nq-person-1");
            Session.Clear();

            Assert.False(Session.Query<IdCard>().Any(c => !c.IsForQuery));
        }

        void DeletePersonAndIdCard(string personName)
        {
            var person = Session.Query<Person>().Single(p => p.Name == personName);
            Session.Delete(person);
            Session.Flush();
        }

        void SavePersonAndIdCard(string personName, string cardId)
        {
            var personId = Guid.NewGuid();
            var person = new Person
            {
                PersonId = personId,
                Name = personName,
            };

            var idcard = new IdCard
            {
                PersonId = personId,
                Id = cardId,
//                Person = person
            };

            person.IdCard = idcard;

            Session.SaveOrUpdate(person);
            Session.Flush();

        }

        void ExecuteNonQuery(string sql)
        {
            ISQLQuery query = StatelessSession.CreateSQLQuery(sql);
            query.ExecuteUpdate();
        }
    }
}