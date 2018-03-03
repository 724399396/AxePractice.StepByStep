using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using one_to_one.Entity;
using Xunit;
using Xunit.Abstractions;

namespace one_to_one
{
    public class OneToOneFact : FactBase
    {
        public OneToOneFact(ITestOutputHelper output) : base(output)
        {
            ExecuteNonQuery("DELETE FROM [dbo].[person] WHERE IsForQuery=0");
            ExecuteNonQuery("DELETE FROM [dbo].[employee] WHERE IsForQuery=0");
        }

        [Fact]
        public void should_get_employee_by_person()
        {
            var person = Session.Query<Person>()
                .Single(p => p.Name == "A");

            Assert.Equal(
                "dev",
                person.Employee.Role);
        }

        [Fact]
        public void should_get_person_by_employee()
        {
            var idCard = Session.Query<Employee>()
                .Single(p => p.Role == "qa");

            Assert.Equal(
                "B",
                idCard.Person.Name);
        }

        [Fact]
        public void should_insert_person_and_employee_by_save_person()
        {
            SavePersonAndEmployeeByPerson(
                "nq-person-1", "ba");
            Session.Clear();

            var insertedPerson = Session.Query<Person>()
                .Fetch(p => p.Employee)
                .Single(p => !p.IsForQuery);

            Assert.Equal(
                "ba",
                insertedPerson.Employee.Role);
        }

        [Fact]
        public void should_insert_person_and_employee_by_save_employee()
        {
            SavePersonAndEmployeeByEmployee(
                "nq-person-1", "ba");
            Session.Clear();

            var insertedPerson = Session.Query<Person>()
                .Fetch(p => p.Employee)
                .Single(p => !p.IsForQuery);

            Assert.Equal(
                "ba",
                insertedPerson.Employee.Role);
        }

        [Fact]
        public void should_delete_in_a_cascade_manner_by_person()
        {
            SavePersonAndEmployeeByPerson(
                "nq-person-1",
                "pm");
            Session.Clear();

            DeletePersonAndEmployeeByPerson("nq-person-1");
            Session.Clear();

            Assert.False(Session.Query<Employee>().Any(c => !c.IsForQuery));
        }

        [Fact]
        public void should_delete_in_a_cascade_manner_by_employee()
        {
            SavePersonAndEmployeeByPerson(
                "nq-person-1",
                "new-role");
            Session.Clear();

            DeletePersonAndEmployeeByEmployee("new-role");
            Session.Clear();

            Assert.False(Session.Query<Employee>().Any(c => !c.IsForQuery));
        }

        void DeletePersonAndEmployeeByPerson(string personName)
        {
            var person = Session.Query<Person>().Single(p => p.Name == personName);
            Session.Delete(person);
            Session.Flush();
        }

        void DeletePersonAndEmployeeByEmployee(string role)
        {
            var employee = Session.Query<Employee>().Single(p => p.Role == role);
            Session.Delete(employee);
            Session.Flush();
        }

        void SavePersonAndEmployeeByPerson(string personName, string cardId)
        {
            var personId = Guid.NewGuid();
            var person = new Person
            {
                PersonId = personId,
                Name = personName,
            };

            var employee = new Employee
            {
                PersonId = personId,
                Role = cardId,
                Person = person
            };

            person.Employee = employee;

            Session.SaveOrUpdate(employee);
            Session.Flush();

        }

        void SavePersonAndEmployeeByEmployee(string personName, string cardId)
        {
            var personId = Guid.NewGuid();
            var person = new Person
            {
                PersonId = personId,
                Name = personName,
            };

            var employee = new Employee
            {
                PersonId = personId,
                Role = cardId,
                Person = person
            };

            person.Employee = employee;

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