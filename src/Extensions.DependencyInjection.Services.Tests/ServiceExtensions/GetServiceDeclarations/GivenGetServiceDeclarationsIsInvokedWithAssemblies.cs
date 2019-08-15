using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedWithAssemblies
    {
        private ServiceDeclaration result = null;

        [OneTimeSetUp]
        public void Setup()
        {
            Assembly assembly = typeof(TestImplementation).Assembly;

            result = ServiceExtensions
                .GetServiceDeclarations(new[] { assembly })
                .Single();
        }

        [Test]
        public void ThenTheServiceDescriptorIsReturned()
        {
            Assert.AreEqual(typeof(TestImplementation), result.DeclaringType);
            Assert.AreEqual(typeof(ITestInterface), result.ServiceType);
            Assert.AreEqual(ServiceScope.Transient, result.Scope);
        }
    }
}
