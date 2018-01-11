using System.Collections.Generic;
using System.Linq;
using Orm.Practice.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Orm.Practice
{
    public class OneToManyQueryFactsCopy : OrmFactBase
    {
        public OneToManyQueryFactsCopy(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void get_child()
        {
            var parent = Session.Query<Child>().First();
        }

        [Fact]
        public void get_parent()
        {
            var parent = Session.Query<Parent>().First();
        }
    }
}