using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvoked
    {
        private Mock<IServiceCollection> subject;

        [OneTimeSetUp]
        public void Setup()
        {
            IEnumerable<ServiceDescriptor> serviceDescriptors = new ServiceDescriptor[0];

            subject = new Mock<IServiceCollection>();
            subject
                .Setup(instance => instance.GetEnumerator())
                .Returns(serviceDescriptors.GetEnumerator());

            ServiceExtensions.RegisterServices(subject.Object, new[] { typeof(ITransientInterface).Assembly });
        }

        [Test]
        public void ThenTheTestServiceIsAdded()
        {
            subject.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Transient &&
                x.ServiceType == typeof(ITransientInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }
    }
}
