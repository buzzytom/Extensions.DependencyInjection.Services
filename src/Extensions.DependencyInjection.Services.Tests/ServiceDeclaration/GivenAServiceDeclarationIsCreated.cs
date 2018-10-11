using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenAServiceDeclarationIsCreated
    {
        [Test]
        public void WithANullServiceType_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ServiceDeclaration(null, typeof(int), ServiceScope.Singleton));
            Assert.AreEqual("serviceType", exception.ParamName);
        }

        [Test]
        public void WithANullDeclaringType_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ServiceDeclaration(typeof(int), null, ServiceScope.Singleton));
            Assert.AreEqual("declaringType", exception.ParamName);
        }

        [Test]
        public void WithAnInvalidTypeConfiguration_ThenAnExceptionIsThrown()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITestInterface), typeof(NotAnImplementation), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for 'NotAnImplementation' because it cannot be assigned to 'ITestInterface'.", exception.Message);
        }

        [Test]
        public void WithAnInterfaceDeclaringType_ThenAnExceptionIsThrown()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITestInterface), typeof(ITestInterface), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for an interface type 'ITestInterface'.", exception.Message);
        }

        [Test]
        public void WithAnAbstractDeclaringType_ThenAnExceptionIsThrown()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITestInterface), typeof(ATestImplementation), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for an abstract type 'ATestImplementation'.", exception.Message);
        }

        [Test]
        public void WithValidParameters_ThenThePropertiesMapVerbatim()
        {
            ServiceDeclaration serviceDeclaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Singleton);
            Assert.AreEqual(typeof(ITestInterface), serviceDeclaration.ServiceType);
            Assert.AreEqual(typeof(TestImplementation), serviceDeclaration.DeclaringType);
            Assert.AreEqual(ServiceScope.Singleton, serviceDeclaration.Scope);
        }
    }
}
