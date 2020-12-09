using System;
using NSubstitute;
using Xunit;
using FluentAssertions;

namespace MyLibTests
{
    /// <summary>
    /// Unit tests for the simple timer class. 
    /// </summary>
    /// <remarks>
    /// Using NSubstitute to provide a mocked date time system, whereby we
    /// can simulate any particular date/time.
    ///
    /// Using FluentAssertions for enhanced assertion checking.
    /// </remarks>
    public class SimpleTimerTests
    {
        readonly TimeSpan timerInterval = TimeSpan.FromSeconds(10);


        /// <summary>
        /// Check timer has not elapsed after waiting a (simulated) amount
        /// of time less than the timer's interval.
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        public void WithinInterval_Expired(int secondsElapsed)
        {
            // Use NSubstitute to provide an implementation of IDateTimeServices.
            var dateTimeServices = Substitute.For<System.Abstractions.IDateTimeServices>();
            dateTimeServices.UtcNow.Returns(new DateTime(2020, 1, 1));


            // Our timer that we want to test
            var simpleTimer = new MyLib.SimpleTimer(
                dateTimeServices: dateTimeServices,
                interval: timerInterval);


            // Update the mocked date time services so that a simulated interval
            // of time has elapsed, less than the 
            dateTimeServices.UtcNow.Returns(
                dateTimeServices.UtcNow +
                TimeSpan.FromSeconds(secondsElapsed));


            // FluentAssertions method of checking the Expired property
            simpleTimer.Expired.Should().BeFalse();


            // Standard XUnit test for comparison
            Assert.False(simpleTimer.Expired);
        }


        /// <summary>
        /// Check timer has expired after the (simulated) interval has passed.
        /// </summary>
        [Theory]
        [InlineData(11)]
        public void AfterInterval_Expired(int secondsElapsed)
        {
            var dateTimeServices = Substitute.For<System.Abstractions.IDateTimeServices>();

            dateTimeServices.UtcNow.Returns(new DateTime(2020, 1, 1));


            var simpleTimer = new MyLib.SimpleTimer(
                dateTimeServices: dateTimeServices,
                interval: timerInterval);


            dateTimeServices.UtcNow.Returns(
                dateTimeServices.UtcNow +
                TimeSpan.FromSeconds(secondsElapsed));


            simpleTimer.Expired.Should().BeTrue();
        }
    }
}
