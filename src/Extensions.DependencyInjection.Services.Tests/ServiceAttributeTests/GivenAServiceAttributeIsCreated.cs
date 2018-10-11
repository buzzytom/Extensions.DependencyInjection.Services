using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenAServiceAttributeIsCreated
    {
        [Test]
        public void WithANullType_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ServiceAttribute(null));
            Assert.AreEqual("type", exception.ParamName);
        }

        [Test]
        public void WithAType_ThenThePropertiesMapVerbatim()
        {
            ServiceAttribute attribute = new ServiceAttribute(typeof(int), ServiceScope.Singleton);
            Assert.AreEqual(typeof(int), attribute.Type);
            Assert.AreEqual(ServiceScope.Singleton, attribute.Scope);
        }
    }
}
