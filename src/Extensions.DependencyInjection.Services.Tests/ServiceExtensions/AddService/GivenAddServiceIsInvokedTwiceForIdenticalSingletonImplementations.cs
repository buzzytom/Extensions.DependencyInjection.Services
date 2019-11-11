using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public sealed class GivenAddServiceIsInvokedTwiceForIdenticalSingletonImplementations
    {
        private ServiceProvider result = null;

        [OneTimeSetUp]
        public void Setup()
        {
            ServiceCollection subject = new ServiceCollection();

            ServiceDeclaration singletonOneDeclaration = new ServiceDeclaration(typeof(TestImplementation), typeof(TestImplementation), ServiceScope.Singleton);
            ServiceDeclaration singletonTwoDeclaration = new ServiceDeclaration(typeof(TestImplementation), typeof(TestImplementation), ServiceScope.Singleton);

            ServiceExtensions.AddService(subject, singletonOneDeclaration);
            ServiceExtensions.AddService(subject, singletonTwoDeclaration);

            result = subject.BuildServiceProvider();
        }

        [Test]
        public void ThenBothOfTheSingletonServicesReturnTheSameInstance()
        {
            ISingletonTestInterface one = result.GetService<TestImplementation>();
            ISecondSingletonTestInterface two = result.GetService<TestImplementation>();

            Assert.That(ReferenceEquals(one, two), Is.True);
        }
    }
}
