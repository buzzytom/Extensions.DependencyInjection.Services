using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture(ServiceScope.Transient, ServiceLifetime.Transient)]
    [TestFixture(ServiceScope.Scoped, ServiceLifetime.Scoped)]
    [TestFixture(ServiceScope.Singleton, ServiceLifetime.Singleton)]
    public sealed class GivenAddServiceIsInvoked
    {
        private readonly ServiceScope declartionScope;
        private readonly ServiceLifetime expectedLifetime;
        Mock<IServiceCollection> services = null;

        public GivenAddServiceIsInvoked(ServiceScope declartionScope, ServiceLifetime expectedLifetime)
        {
            this.declartionScope = declartionScope;
            this.expectedLifetime = expectedLifetime;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            services = new Mock<IServiceCollection>();
            ServiceDeclaration declaration = new ServiceDeclaration(typeof(ITransientInterface), typeof(TestImplementation), declartionScope);

            ServiceExtensions.AddService(services.Object, declaration);
        }

        [Test]
        public void ThenAServiceIsAddedWithTheDescriptorMappedVerbatim()
        {
            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == expectedLifetime &&
                x.ServiceType == typeof(ITransientInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }
    }
}
