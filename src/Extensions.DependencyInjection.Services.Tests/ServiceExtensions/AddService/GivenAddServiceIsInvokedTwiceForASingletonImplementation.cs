using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public sealed class GivenAddServiceIsInvokedTwiceForASingletonImplementation
    {
        private ServiceProvider result = null;

        [OneTimeSetUp]
        public void Setup()
        {
            ServiceCollection subject = new ServiceCollection();

            ServiceDeclaration transientDeclaration = new ServiceDeclaration(typeof(ITransientInterface), typeof(TestImplementation), ServiceScope.Transient);
            ServiceDeclaration singletonOneDeclaration = new ServiceDeclaration(typeof(ISingletonTestInterface), typeof(TestImplementation), ServiceScope.Singleton);
            ServiceDeclaration singletonTwoDeclaration = new ServiceDeclaration(typeof(ISecondSingletonTestInterface), typeof(TestImplementation), ServiceScope.Singleton);

            ServiceExtensions.AddService(subject, transientDeclaration);
            ServiceExtensions.AddService(subject, singletonOneDeclaration);
            ServiceExtensions.AddService(subject, singletonTwoDeclaration);

            result = subject.BuildServiceProvider();
        }

        [Test]
        public void ThenBothOfTheSingletonServicesReturnTheSameInstance()
        {
            ISingletonTestInterface one = result.GetService<ISingletonTestInterface>();
            ISecondSingletonTestInterface two = result.GetService<ISecondSingletonTestInterface>();

            Assert.That(ReferenceEquals(one, two), Is.True);
        }

        [Test]
        public void ThenTheTransientServiceReferenceIsNotTheSameAsTheSingleton()
        {
            ISingletonTestInterface singleton = result.GetService<ISingletonTestInterface>();
            ITransientInterface transient = result.GetService<ITransientInterface>();

            Assert.That(ReferenceEquals(singleton, transient), Is.False);
        }
    }
}
