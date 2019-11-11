using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services
{
    /// <summary>
    /// Defines the extension methods to register all services in assemblies.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers all exported types declaring a <see cref="ServiceAttribute"/> to a service collection within the specified assemblies.
        /// </summary>
        /// <param name="services">The service collection to register the services to.</param>
        /// <param name="assemblies">The assemblies to search for services in.</param>
        public static void RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            IEnumerable<ServiceDeclaration> declarations = assemblies.GetServiceDeclarations();
            foreach (ServiceDeclaration declaration in declarations)
                services.AddService(declaration);
        }

        /// <summary>
        /// Gets a <see cref="ServiceDeclaration"/> instance for every exported type in the specified assemblies that is attributed with a <see cref="ServiceAttribute"/>.
        /// </summary>
        /// <param name="assemblies">The assemblies to search for services in.</param>
        /// <returns>A collection of all the service descriptors found in the specified assemblies.</returns>
        public static IEnumerable<ServiceDeclaration> GetServiceDeclarations(this IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            return assemblies.SelectMany(x => x.GetServiceDeclarations());
        }

        /// <summary>
        /// Gets a <see cref="ServiceDeclaration"/> instance for every exported type in the specified assembly that is attributed with a <see cref="ServiceAttribute"/>.
        /// </summary>
        /// <param name="assembly">The assembly to search for services in.</param>
        /// <returns>A collection of all the service descriptors found in the specified assembly.</returns>
        public static IEnumerable<ServiceDeclaration> GetServiceDeclarations(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            List<ServiceDeclaration> serviceDeclarations = new List<ServiceDeclaration>();
            Type[] types = assembly.GetExportedTypes();
            foreach (Type type in types)
            {
                IEnumerable<ServiceAttribute> serviceAttributes = (IEnumerable<ServiceAttribute>)type.GetCustomAttributes(typeof(ServiceAttribute));
                foreach (ServiceAttribute serviceAttribute in serviceAttributes)
                    serviceDeclarations.Add(new ServiceDeclaration(serviceAttribute.Type, type, serviceAttribute.Scope));
            }

            return serviceDeclarations;
        }

        /// <summary>
        /// Adds the specified <see cref="ServiceDeclaration"/> to a <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The collection to add the service to.</param>
        /// <param name="declaration">The description of the service.</param>
        public static void AddService(this IServiceCollection services, ServiceDeclaration declaration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (declaration == null)
                throw new ArgumentNullException(nameof(declaration));

            if (declaration.Scope == ServiceScope.Scoped)
                services.AddScoped(declaration.ServiceType, declaration.DeclaringType);
            else if (declaration.Scope == ServiceScope.Transient)
                services.AddTransient(declaration.ServiceType, declaration.DeclaringType);
            else if (declaration.Scope == ServiceScope.Singleton)
            {
                ServiceDescriptor existingServiceDescription = services.FirstOrDefault(x =>
                    x.Lifetime == ServiceLifetime.Singleton &&
                    x.ImplementationType == declaration.DeclaringType &&
                    x.ServiceType != declaration.ServiceType);

                if (existingServiceDescription == null)
                    services.AddSingleton(declaration.ServiceType, declaration.DeclaringType);
                else
                    services.AddSingleton(declaration.ServiceType, provider => provider.GetService(existingServiceDescription.ServiceType));
            }
        }
    }
}
