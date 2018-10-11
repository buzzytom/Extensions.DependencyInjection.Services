using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenAddServiceIsInvoked
    {
        [Test]
        public void WithNullServices_ThenAnExceptionIsThrown()
        {
            ServiceDeclaration declaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Scoped);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.AddService(null, declaration));
            Assert.AreEqual("services", exception.ParamName);
        }

        [Test]
        public void WithANullDeclaration_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.AddService(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("declaration", exception.ParamName);
        }

        [Test]
        public void WithATransientDeclaration_ThenAServiceIsAddedWithTheDescriptorMappedVerbatim()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();
            ServiceDeclaration declaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Transient);

            ServiceExtensions.AddService(services.Object, declaration);

            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Transient &&
                x.ServiceType == typeof(ITestInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }

        [Test]
        public void WithAScopedDeclaration_ThenAServiceIsAddedWithTheDescriptorMappedVerbatim()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();
            ServiceDeclaration declaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Scoped);

            ServiceExtensions.AddService(services.Object, declaration);

            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Scoped &&
                x.ServiceType == typeof(ITestInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }

        [Test]
        public void WithASingletonDeclaration_ThenAServiceIsAddedWithTheDescriptorMappedVerbatim()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();
            ServiceDeclaration declaration = new ServiceDeclaration(typeof(ITestInterface), typeof(TestImplementation), ServiceScope.Singleton);

            ServiceExtensions.AddService(services.Object, declaration);

            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Singleton &&
                x.ServiceType == typeof(ITestInterface) &&
                x.ImplementationType == typeof(TestImplementation))), Times.Once);
        }
    }
}
