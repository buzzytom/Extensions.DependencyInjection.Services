Extensions.DependencyInjection.Services
=======================================

An extension for *Microsoft.Extensions.DependencyInjection* allowing classes to
be decorated with [Service] for quick and easy dependency registration.

Installation Guide
------------------

Install the NuGet package

>   Buzzytom.Extensions.DependencyInjection.Services

Add using statements to get the extensions.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
using Extensions.DependencyInjection.Services;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the ConfigureServices of your application (or equivalent) add the call to
register the services.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
public void ConfigureServices(IServiceCollection services)
{
    // Register other services here, like entity framework
    
    // RegisterServices binds all the services declared in the specified assemblies
    services.RegisterServices(GetType().Assembly);
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Declare dependencies with the service attribute

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
[Service(typeof(ISomeServiceInterface))]
public class SomeServiceImplementation
{
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Improvements
------------

Raise them as an issue
