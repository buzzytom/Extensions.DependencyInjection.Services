namespace Extensions.DependencyInjection.Services.Tests
{
    public interface ITestInterface
    {
    }

    public abstract class ATestImplementation : ITestInterface
    {
    }

    [Service(typeof(ITestInterface), ServiceScope.Transient)]
    public class TestImplementation : ATestImplementation
    {
    }

    public class NotAnImplementation
    {
    }
}
