using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvokedWithoutAnyAssemblies
    {
        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.RegisterServices(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("assemblies", exception.ParamName);
        }
    }
}
