using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvoked
    {
        [Test]
        public void WithNullServices_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.RegisterServices(null, new Assembly[0]));
            Assert.AreEqual("services", exception.ParamName);
        }

        [Test]
        public void WithNullAssemblies_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.RegisterServices(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("assemblies", exception.ParamName);
        }

        [Test]
        public void WithAnEmptyAssemblySet_ThenNoServicesAreRegistered()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();

            ServiceExtensions.RegisterServices(services.Object, new Assembly[0]);

            services.Verify(instance => instance.Add(It.IsAny<ServiceDescriptor>()), Times.Never);
        }

        [Test]
        public void WithTheTestAssembly_ThenTheTestServiceIsAdded()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();

            ServiceExtensions.RegisterServices(services.Object, new[] { typeof(ITestInterface).Assembly });

            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Transient &&
                x.ServiceType == typeof(ITestInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }
    }
}
