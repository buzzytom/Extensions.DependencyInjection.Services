using NUnit.Framework;
using System;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedWithoutAnAssembly
    {
        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            Assembly assembly = null;
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.GetServiceDeclarations(assembly));
            Assert.AreEqual("assembly", exception.ParamName);
        }
    }
}
