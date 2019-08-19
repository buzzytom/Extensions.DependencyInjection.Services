using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvoked
    {
        private Mock<IServiceCollection> subject;

        [OneTimeSetUp]
        public void Setup()
        {
            subject = new Mock<IServiceCollection>();

            ServiceExtensions.RegisterServices(subject.Object, new[] { typeof(ITestInterface).Assembly });
        }

        [Test]
        public void ThenTheTestServiceIsAdded()
        {
            subject.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Transient &&
                x.ServiceType == typeof(ITestInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }
    }
}
