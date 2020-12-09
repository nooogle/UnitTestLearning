using System;
namespace MyLib
{
    /// <summary>
    /// Demo timer using abstract date and time services. Just for
    /// unit testing development.
    /// </summary>
    public class SimpleTimer
    {
        System.Abstractions.IDateTimeServices dateTimeServices;
        DateTime creationTime;
        TimeSpan interval;


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
