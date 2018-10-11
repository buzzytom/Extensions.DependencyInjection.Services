using System;

namespace Extensions.DependencyInjection.Services
{
    public class ServiceDeclaration
    {
        public ServiceDeclaration(ServiceAttribute attribute, Type declaringType)
        {
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
        }

        public ServiceAttribute Attribute { get; }

        public Type DeclaringType { get; }
    }
}
