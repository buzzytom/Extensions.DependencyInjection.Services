using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenAddServiceIsInvokedWithoutAServiceCollection
    {
        private ServiceDeclaration declaration = null;

        [OneTimeSetUp]
        public void Setup()
        {
            declaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Scoped);
        }

        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.AddService(null, declaration));
            Assert.AreEqual("services", exception.ParamName);
        }
    }
}
