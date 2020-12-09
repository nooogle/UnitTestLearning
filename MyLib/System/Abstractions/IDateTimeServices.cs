using System;

namespace System.Abstractions
{
    /// <summary>
    /// Provides date and time services.
    /// </summary>
    /// <remarks>
    /// Using an interface, rather than the built-in .Net DateTime class,
    /// we have options to mock dates and times for testing.
    /// </remarks>
    public interface IDateTimeServices
    {
        DateTime UtcNow { get; } 
    }
}
