using System;

namespace Extensions.DependencyInjection.Services
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(Type type, ServiceScope scope = ServiceScope.Scoped)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope;
        }

        // ----- Properties ----- //

        public Type Type { get; }

        public ServiceScope Scope { get; }
    }
}
