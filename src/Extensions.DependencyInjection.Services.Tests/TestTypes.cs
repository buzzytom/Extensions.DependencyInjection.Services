namespace Extensions.DependencyInjection.Services.Tests
{
    public interface ITestInterface
    {
    }

    public abstract class ATestImplementation : ITestInterface
    {
    }

    public class TestImplementation : ATestImplementation
    {
    }

    public class NotAnImplementation
    {
    }
}
