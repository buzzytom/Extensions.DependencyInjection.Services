namespace Extensions.DependencyInjection.Services
{
    /// <summary>Defines an enumeration of the possible lifetimes of a service.</summary>
    public enum ServiceScope
    {
        /// <summary>A new instance shall be created for each intialization usage.</summary>
        Transient,
        /// <summary>An instance will be shared for all dependency injections within a single injection scope.</summary>
        Scoped,
        /// <summary>A single instance will be used for every injection.</summary>
        Singleton
    }
}
