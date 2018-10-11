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
                .Select(x => new ServiceDeclaration(x.Attribute.Type, x.DeclaringType, x.Attribute.Scope));
        }

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
                services.AddSingleton(declaration.ServiceType, declaration.DeclaringType);
        }
    }
}
