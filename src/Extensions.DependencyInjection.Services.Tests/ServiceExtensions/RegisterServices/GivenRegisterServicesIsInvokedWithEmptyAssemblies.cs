using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvokedWithEmptyAssemblies
    {
        private Mock<IServiceCollection> subject = null;

        [OneTimeSetUp]
        public void Setup()
        {
            subject = new Mock<IServiceCollection>();

            ServiceExtensions.RegisterServices(subject.Object, new Assembly[0]);
        }

        [Test]
        public void ThenNoServicesAreRegisteredToTheServiceCollection()
        {
            subject.Verify(instance => instance.Add(It.IsAny<ServiceDescriptor>()), Times.Never);
        }
    }
}
