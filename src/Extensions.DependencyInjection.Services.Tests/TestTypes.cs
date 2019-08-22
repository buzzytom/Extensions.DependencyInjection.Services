namespace Extensions.DependencyInjection.Services.Tests
{
    public interface ITransientInterface
    {
    }

    public interface ISingletonTestInterface
    {
    }

    public abstract class ATestImplementation : ITransientInterface, ISingletonTestInterface
    {
    }

    [Service(typeof(ITransientInterface), ServiceScope.Transient)]
    [Service(typeof(ISingletonTestInterface), ServiceScope.Singleton)]
    public class TestImplementation : ATestImplementation
    {
    }

    public class NotAnImplementation
    {
    }
}
