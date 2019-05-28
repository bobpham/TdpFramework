using NUnit.Framework;
using System;
using Tdp.Common.Tests.Models;

namespace Tdp.Common.Tests
{
    [TestFixture(Category = "Relection Utility")]
    public class PropertyUtilsTests
    {
                      
        [TestCase("FirstName", true, typeof(string))]
        [TestCase("LastName", true, typeof(string))]
        [TestCase("FirstName1", false, typeof(string))]
        [TestCase("LastName1", false, typeof(string))]
        [TestCase("Age", true, typeof(int))]
        public void CheckHasPropertyWithGivenClassAndPropertyNameWithTypeString(string propertyName, bool expectedOutCome, Type expectedType )
        {
            //Arrange
            
            //Act 
            Type t = PropertyUtils.GetPropertyTypeFromClass<Person>(propertyName);

          
            if (expectedOutCome)
                Assert.That(t, Is.EqualTo(expectedType));
            else
                Assert.That(t, Is.Not.EqualTo(expectedType));
        }

      
    }
}
