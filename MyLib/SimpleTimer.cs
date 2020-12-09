using System;
namespace MyLib
{
    /// <summary>
    /// Demo timer using abstract date and time services. Just for
    /// unit testing development.
    /// </summary>
    /// <remarks>
    /// This class could use DateTime directly to track when an interval
    /// of time has elapsed. However, a unit test would be then have to actually
    /// allow the interval to expire for testing. Instead we use the date time
    /// services interface to allow for both real time and mocked/simulated
    /// time to be used.
    /// </remarks>
    public class SimpleTimer
    {
        System.Abstractions.IDateTimeServices dateTimeServices;
        DateTime creationTime;
        TimeSpan interval;


        /// <summary>
        /// Initialise the timer such that it will expire after the
        /// specified interval.
        /// </summary>
        public SimpleTimer(
            System.Abstractions.IDateTimeServices dateTimeServices,
            TimeSpan interval)
        {
            this.dateTimeServices = dateTimeServices;
            creationTime = dateTimeServices.UtcNow;
            this.interval = interval;
        }


        /// <summary>
        /// True if the timer has expired
        /// </summary>
        public bool Expired => (dateTimeServices.UtcNow - creationTime) > interval;
    }
}
