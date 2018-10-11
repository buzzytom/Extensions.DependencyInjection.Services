using System;

namespace Extensions.DependencyInjection.Services
{
    public class ServiceDeclaration
    {
        public ServiceDeclaration(Type serviceType, Type declaringType, ServiceScope scope)
        {
            DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            Scope = scope;

            if (!serviceType.IsAssignableFrom(declaringType))
                throw new InvalidOperationException($"A service declaration can not be made for '{declaringType.Name}' because it cannot be assigned to '{serviceType.Name}'.");
            if (declaringType.IsInterface)
                throw new InvalidOperationException($"A service declaration can not be made for an interface type '{declaringType.Name}'.");
            if (declaringType.IsAbstract)
                throw new InvalidOperationException($"A service declaration can not be made for an abstract type '{declaringType.Name}'.");
        }

        public ServiceScope Scope { get; }

        public Type ServiceType { get; }

        public Type DeclaringType { get; }
    }
}
