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
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITransientInterface), typeof(NotAnImplementation), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for 'NotAnImplementation' because it cannot be assigned to 'ITransientInterface'.", exception.Message);
        }

        [Test]
        public void WithAnInterfaceDeclaringType_ThenAnExceptionIsThrown()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITransientInterface), typeof(ITransientInterface), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for an interface type 'ITransientInterface'.", exception.Message);
        }

        [Test]
        public void WithAnAbstractDeclaringType_ThenAnExceptionIsThrown()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new ServiceDeclaration(typeof(ITransientInterface), typeof(TestImplementationBase), ServiceScope.Singleton));
            Assert.AreEqual("A service declaration can not be made for an abstract type 'TestImplementationBase'.", exception.Message);
        }

        [Test]
        public void WithValidParameters_ThenThePropertiesMapVerbatim()
        {
            ServiceDeclaration serviceDeclaration = new ServiceDeclaration(typeof(ITransientInterface), typeof(TestImplementation), ServiceScope.Singleton);
            Assert.AreEqual(typeof(ITransientInterface), serviceDeclaration.ServiceType);
            Assert.AreEqual(typeof(TestImplementation), serviceDeclaration.DeclaringType);
            Assert.AreEqual(ServiceScope.Singleton, serviceDeclaration.Scope);
        }
    }
}
