using MVCPlayground.Framework.Common;
using NUnit.Framework;
using System;

namespace MVCPlayground.Framework.Tests.Common
{
    class TestNVP : NameValuePair
    {
        public TestNVP(string name, string value)
            : base(name, value) { }
    }

    class NameValuePairTests
    {
        [Test]
        public void Constructor_ShouldThrowWithInvalidArgs()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>(),
                () => new TestNVP(null, "test"));

            Assert.Throws(
                Is.TypeOf<ArgumentNullException>(),
                () => new TestNVP("test", null));

            Assert.Throws(
                Is.TypeOf<ArgumentNullException>(),
                () => new TestNVP(null, null));
        }

        [Test]
        public void Constructor_ShouldNotThrowWithValidArgs()
        {
            Assert.DoesNotThrow(() => new TestNVP("test", "test"));
        }

        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var name = "test_name";
            var value = "test_value";

            var nvp = new TestNVP(name, value);

            Assert.AreEqual(name, nvp.Name);
            Assert.AreEqual(value, nvp.Value);
        }
    }
}
