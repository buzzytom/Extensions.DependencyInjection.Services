using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedWithAssemblies
    {
        private ServiceDeclaration[] result = null;

        [OneTimeSetUp]
        public void Setup()
        {
            Assembly assembly = typeof(TestImplementation).Assembly;

            result = ServiceExtensions
                .GetServiceDeclarations(new[] { assembly })
                .ToArray();
        }

        [Test]
        public void ThenAServiceIsReturnedForEveryServiceAttributeOnTheClass()
        {
            Assert.That(result.Length, Is.EqualTo(3));
        }

        [Test]
        public void ThenTheTransientServiceDescriptorIsReturned()
        {
            ServiceDeclaration serviceDeclaration = result.Single(x => x.ServiceType == typeof(ITransientInterface));
            Assert.That(serviceDeclaration.DeclaringType, Is.EqualTo(typeof(TestImplementation)));
            Assert.That(serviceDeclaration.Scope, Is.EqualTo(ServiceScope.Transient));
        }

        [Test]
        public void ThenThePrimarySingletonServiceDescriptorIsReturned()
        {
            ServiceDeclaration serviceDeclaration = result.Single(x => x.ServiceType == typeof(ISingletonTestInterface));
            Assert.That(serviceDeclaration.DeclaringType, Is.EqualTo(typeof(TestImplementation)));
            Assert.That(serviceDeclaration.Scope, Is.EqualTo(ServiceScope.Singleton));
        }

        [Test]
        public void ThenTheSecondarySingletonServiceDescriptorIsReturned()
        {
            ServiceDeclaration serviceDeclaration = result.Single(x => x.ServiceType == typeof(ISecondSingletonTestInterface));
            Assert.That(serviceDeclaration.DeclaringType, Is.EqualTo(typeof(TestImplementation)));
            Assert.That(serviceDeclaration.Scope, Is.EqualTo(ServiceScope.Singleton));
        }
    }
}
