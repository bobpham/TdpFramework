
using System.Collections.Generic;

namespace Tdp.Common.Tests.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
    }

    public class FakeData
    {
        public static List<Person> People()
        {
            var people = new List<Person>
            {
                new Person(){FirstName = "Bob", LastName="Pham", Age = 35},
                new Person(){FirstName = "An", LastName="Pham", Age = 70},
                new Person(){FirstName = "Dung", LastName="Huynh", Age=50}
            };

            return people;
        }
    }
}
