using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tdp.Common.Tests.Models;

namespace Tdp.Common.Tests
{
    [TestFixture(Category = "Expression Tree")]
    public class PredicateBuilderTests
    {

        [Test]
        public void BuildGreaterThanExpression( )
        {
            //Arrange
            List<Person> people = FakeData.People();

            Expression<Func<Person, bool>> express = PredicateBuilder.BuildGreaterThan<Person>("Age", 40);

            Func<Person, bool> func = express.Compile();

            var result = people.Where(func);

            var expectedResult = people.Where(p => p.Age >= 40);

            Assert.That(result, Is.EquivalentTo(expectedResult));
        }

        [Test]
        public void BuildEqualExpression()
        {
            //Arrange
            List<Person> people = FakeData.People();

            Expression<Func<Person, bool>> express = PredicateBuilder.BuildEqual<Person>("Age", 70);

            Func<Person, bool> func = express.Compile();

            var result = people.Where(func);

            var expectedResult = people.Where(p => p.Age == 70);

            Assert.That(result.Count(), Is.EqualTo(expectedResult.Count()));
        }



        [Test]
        public void BuildContainExpression()
        {
            //Arrange
            List<Person> people = FakeData.People();

            Expression<Func<Person, bool>> express = PredicateBuilder.BuildContains<Person>("FirstName", "Du");

            Func<Person, bool> func = express.Compile();

            string format = express.ToString();

            var result = people.Where(func);

            var expectedResult = people.Where(p => p.FirstName.ToLower().Contains("du"));

            Assert.That(result.Count(), Is.EqualTo(expectedResult.Count()));
        }



    }
}
