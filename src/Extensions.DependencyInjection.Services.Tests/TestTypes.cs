namespace Extensions.DependencyInjection.Services.Tests
{
    public interface ITestInterface
    {
    }

    public interface ISecondaryTestInterface
    {
    }

    public abstract class ATestImplementation : ITestInterface, ISecondaryTestInterface
    {
    }

    [Service(typeof(ITestInterface), ServiceScope.Transient)]
    [Service(typeof(ISecondaryTestInterface), ServiceScope.Singleton)]
    public class TestImplementation : ATestImplementation
    {
    }

    public class NotAnImplementation
    {
    }
}
