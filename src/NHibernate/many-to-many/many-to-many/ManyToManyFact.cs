using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using one_to_one.Entity;
using Xunit;
using Xunit.Abstractions;

namespace one_to_one
{
    public class ManyToManyFact : FactBase
    {
        public ManyToManyFact(ITestOutputHelper output) : base(output)
        {
            ExecuteNonQuery("DELETE FROM [dbo].[person] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[competency] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[person_x_competency] WHERE IsForQuery=0");
        }

        [Fact]
        public void should_get_competency_by_person()
        {
            var person = Session.Query<Person>()
                .Single(p => p.Name == "A");

            Assert.Equal(
                new [] {"csharp", "java"}, 
                person.Competencies.Select(x => x.Title).ToArray());
        }

        [Fact]
        public void should_get_person_by_competency()
        {
            var competencies = Session.Query<Competency>()
                .Single(p => p.Title == "csharp");

            Assert.Equal(
                new [] {"A", "B"},
                competencies.Persons.Select(x => x.Name).ToArray());
        }

        [Fact]
        public void should_insert_person_and_competency_by_person()
        {
            SavePersonAndCompetencies(
                "nq-person-1", "new-skill1", "new-skill2");
            Session.Clear();

            var insertedPerson = Session.Query<Person>()
                .Fetch(p => p.Competencies)
                .Single(p => !p.IsForQuery);

            Assert.Equal(
                new [] { "new-skill1", "new-skill2" },
                insertedPerson.Competencies.Select(x => x.Title).ToArray());
        }

        [Fact]
        public void should_insert_person_and_competency_by_competency()
        {
            SaveCompetenciesAndPerson(
                "new-skill", "new-person-1", "new-person-2");
            Session.Clear();

            var insertedCompentency = Session.Query<Competency>()
                .Fetch(p => p.Persons)
                .Single(p => !p.IsForQuery);

            Assert.Equal(
                new[] { "new-person-1", "new-person-2" },
                insertedCompentency.Persons.Select(x => x.Name).ToArray());
        }

        [Fact]
        public void should_delete_competency_and_person_by_person()
        {
            SavePersonAndCompetencies(
                "nq-person-1", "new-skill1", "new-skill2");

            Session.Clear();

            DeletePersonAndCompentencyByPerson("nq-person-1");
            Session.Clear();

            Assert.False(Session.Query<Competency>().Any(c => !c.IsForQuery));
        }


        [Fact]
        public void should_delete_competency_and_person_by_competency()
        {
            SaveCompetenciesAndPerson(
                "new-skill", "new-person-1", "new-person-2");

            Session.Clear();

            DeletePersonAndCompentencyByCompetency("new-skill");
            Session.Clear();

            Assert.False(Session.Query<Person>().Any(c => !c.IsForQuery));
        }

        void DeletePersonAndCompentencyByPerson(string personName)
        {
            var person = Session.Query<Person>().Single(p => p.Name == personName);
            Session.Delete(person);
            Session.Flush();
        }

        void DeletePersonAndCompentencyByCompetency(string competencyTitle)
        {
            var competency = Session.Query<Competency>().Single(p => p.Title == competencyTitle);
            Session.Delete(competency);
            Session.Flush();
        }

        void SavePersonAndCompetencies(string personName, params string[] competencies)
        {
            var person = new Person
            {
                Name = personName
            };

            var competency = competencies.Select(t => new Competency
            {
                Title = t,
            }).ToList();

            person.Competencies = competency;

            Session.SaveOrUpdate(person);
            Session.Flush();

        }

        void SaveCompetenciesAndPerson(string competencyTitle, params string[] personNames)
        {
            var competency = new Competency()
            {
                Title = competencyTitle
            };

            var persons = personNames.Select(t => new Person
            {
                Name = t,
            }).ToList();

            competency.Persons = persons;

            Session.SaveOrUpdate(competency);
            Session.Flush();

        }


        void ExecuteNonQuery(string sql)
        {
            ISQLQuery query = StatelessSession.CreateSQLQuery(sql);
            query.ExecuteUpdate();
        }
    }
}