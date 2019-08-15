using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenAddServiceIsInvokedWithoutADeclaration
    {
        [Test]
        public void ThenAnArgumentNullExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ServiceExtensions.AddService(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("declaration", exception.ParamName);
        }
    }
}
