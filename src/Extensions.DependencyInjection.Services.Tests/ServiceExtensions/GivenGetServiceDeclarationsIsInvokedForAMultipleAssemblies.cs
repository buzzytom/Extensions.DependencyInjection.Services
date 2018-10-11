using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedForAMultipleAssemblies
    {
        [Test]
        public void WithNullAssemblies_ThenAnExceptionIsThrown()
        {
            Assembly[] assemblies = null;
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.GetServiceDeclarations(assemblies));
            Assert.AreEqual("assemblies", exception.ParamName);
        }

        [Test]
        public void WithAssemblies_ThenTheAppropriateDeclarationsAreReturned()
        {
            Assembly assembly = typeof(TestImplementation).Assembly;

            ServiceDeclaration declaration = ServiceExtensions
                .GetServiceDeclarations(new[] { assembly })
                .Single();

            Assert.AreEqual(typeof(TestImplementation), declaration.DeclaringType);
            Assert.AreEqual(typeof(ITestInterface), declaration.ServiceType);
            Assert.AreEqual(ServiceScope.Transient, declaration.Scope);
        }
    }
}
