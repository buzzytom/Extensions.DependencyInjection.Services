using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedForASingleAssembly
    {
        [Test]
        public void WithANullAssembly_ThenAnExceptionIsThrown()
        {
            Assembly assembly = null;
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.GetServiceDeclarations(assembly));
            Assert.AreEqual("assembly", exception.ParamName);
        }

        [Test]
        public void WithAnAssembly_ThenTheAppropriateDeclarationsAreReturned()
        {
            Assembly assembly = typeof(TestImplementation).Assembly;

            ServiceDeclaration declaration = ServiceExtensions
                .GetServiceDeclarations(assembly)
                .Single();

            Assert.AreEqual(typeof(TestImplementation), declaration.DeclaringType);
            Assert.AreEqual(typeof(ITestInterface), declaration.ServiceType);
            Assert.AreEqual(ServiceScope.Transient, declaration.Scope);
        }
    }
}
