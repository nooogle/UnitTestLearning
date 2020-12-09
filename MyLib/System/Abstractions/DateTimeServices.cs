using System;

namespace System.Abstractions
{
    /// <summary>
    /// Provides default .Net date and time services
    /// </summary>
    public class DateTimeServices : IDateTimeServices
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
