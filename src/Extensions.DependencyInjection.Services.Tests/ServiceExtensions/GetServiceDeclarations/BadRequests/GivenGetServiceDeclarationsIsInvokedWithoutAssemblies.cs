using NUnit.Framework;
using System;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedWithoutAssemblies
    {
        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            Assembly[] assemblies = null;
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.GetServiceDeclarations(assemblies));
            Assert.AreEqual("assemblies", exception.ParamName);
        }
    }
}
