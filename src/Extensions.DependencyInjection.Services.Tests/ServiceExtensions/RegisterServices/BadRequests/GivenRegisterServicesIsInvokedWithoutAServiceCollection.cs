using NUnit.Framework;
using System;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenRegisterServicesIsInvokedWithoutAServiceCollection
    {
        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.RegisterServices(null, new Assembly[0]));
            Assert.AreEqual("services", exception.ParamName);
        }
    }
}
