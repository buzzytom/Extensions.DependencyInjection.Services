using System;

namespace Extensions.DependencyInjection.Services
{
    /// <summary>
    /// Specifies the class being attributed should be registered for dependency injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableAttribute"/> class with the base type this service class is implementing.
        /// </summary>
        /// <param name="type">The base type this service implements.</param>
        /// <param name="scope">The lifetime of the service upon initialization.</param>
        public ServiceAttribute(Type type, ServiceScope scope = ServiceScope.Scoped)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Scope = scope;
        }

        // ----- Properties ----- //

        /// <summary>Gets the base type the attributed service is implementing.</summary>
        public Type Type { get; }

        /// <summary>Gets the lifetime scope that will control the intialization of the attributed class.</summary>
        public ServiceScope Scope { get; }
    }
}
