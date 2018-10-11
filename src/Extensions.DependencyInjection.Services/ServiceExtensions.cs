using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services
{
    public static class ServiceExtensions
    {
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

        public static IEnumerable<ServiceDeclaration> GetServiceDeclarations(this IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            return assemblies.SelectMany(x => x.GetServiceDeclarations());
        }

        public static IEnumerable<ServiceDeclaration> GetServiceDeclarations(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly
                .GetExportedTypes()
                .Select(x => new
                {
                    Attribute = (ServiceAttribute)x.GetCustomAttribute(typeof(ServiceAttribute)),
                    DeclaringType = x
                })
                .Where(x => x.Attribute != null)
                .Select(x => new ServiceDeclaration(x.Attribute, x.DeclaringType));
        }

        public static void AddService(this IServiceCollection services, ServiceDeclaration declaration)
        {
            if (declaration == null)
                throw new ArgumentNullException(nameof(declaration));

            services.AddService(declaration.Attribute.Type, declaration.DeclaringType, declaration.Attribute.Scope);
        }

        public static void AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceScope scope = ServiceScope.Transient)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));

            if (!serviceType.IsAssignableFrom(implementationType))
                throw new Exception($"The class {implementationType.Name} has a {nameof(ServiceAttribute)} for a type which it cannot be assigned to: '{serviceType.Name}'.");

            if (scope == ServiceScope.Scoped)
                services.AddScoped(serviceType, implementationType);
            else if (scope == ServiceScope.Transient)
                services.AddTransient(serviceType, implementationType);
            else if (scope == ServiceScope.Singleton)
                services.AddSingleton(serviceType, implementationType);
            else
                throw new Exception($"Unkown service scope '{scope}' for type '{implementationType.Name}'.");
        }
    }
}
