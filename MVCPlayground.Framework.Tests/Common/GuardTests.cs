using MVCPlayground.Framework.Common;
using NUnit.Framework;
using System;

namespace MVCPlayground.Framework.Tests.Common
{
    public class GuardTests
    {
        private object[] nonNullData;

        [SetUp]
        public void Setup()
        {
            nonNullData = new object[] {
                5,
                true,
                false,
                new { },
                new int[] { },
                0.5,
                0.6f,
                0.7m,
                0,
                string.Empty,
                'a'
            };
        }

        [TearDown]
        public void TearDown()
        {
            nonNullData = null;
        }

        [Test]
        public void AgainstNull_ShouldNotThrowForNonNullValues()
        {
            foreach (var value in nonNullData)
            {
                Assert.DoesNotThrow(() => Guard.AgainstNull(value, nameof(value)));
            }
        }

        [Test]
        public void AgainstNull_ShouldThrowCorrectErrorForNullValue()
        {
            object testValue = null;
            string nameOf = nameof(testValue);

            Assert.Throws(
                Is.TypeOf<ArgumentNullException>()
                .And.Message.EqualTo($"Value cannot be null. (Parameter '{nameOf} cannot be null')"),
                () => Guard.AgainstNull(testValue, nameOf));
        }
    }
}
