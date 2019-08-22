namespace Extensions.DependencyInjection.Services.Tests
{
    public interface ITransientInterface
    {
    }

    public interface ISingletonTestInterface
    {
    }

    public interface ISecondSingletonTestInterface
    {
    }

    public abstract class TestImplementationBase : ITransientInterface, ISingletonTestInterface, ISecondSingletonTestInterface
    {
    }

    [Service(typeof(ITransientInterface), ServiceScope.Transient)]
    [Service(typeof(ISingletonTestInterface), ServiceScope.Singleton)]
    [Service(typeof(ISecondSingletonTestInterface), ServiceScope.Singleton)]
    public class TestImplementation : TestImplementationBase
    {
    }

    public class NotAnImplementation
    {
    }
}
