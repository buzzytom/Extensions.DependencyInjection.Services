using System;

namespace Extensions.DependencyInjection.Services
{
    /// <summary>
    /// Defines the specification for a class to be registered as a service for dependency injection.
    /// </summary>
    public class ServiceDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDeclaration"/> class with a configuration for intializing a dependency injector.
        /// </summary>
        /// <param name="serviceType">The base type that will be injected.</param>
        /// <param name="declaringType">The actual implementation type of the <paramref name="serviceType"/> that will be injected.</param>
        /// <param name="scope">The lifetime of the service upon initialization.</param>
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

        // ----- Properties ----- //

        /// <summary>Gets the scope that will control the lifetime of the instatiated class.</summary>
        public ServiceScope Scope { get; }

        /// <summary>Gets the type of the service to be registered.</summary>
        public Type ServiceType { get; }

        /// <summary>Gets the implementation type of the service to be injected.</summary>
        public Type DeclaringType { get; }
    }
}
