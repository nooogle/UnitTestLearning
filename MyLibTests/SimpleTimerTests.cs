using System;
using NSubstitute;
using Xunit;
using FluentAssertions;

namespace MyLibTests
{
    /// <summary>
    /// Unit tests for the simple timer class. 
    /// </summary>
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
            var dateTimeServices = Substitute.For<System.Abstractions.IDateTimeServices>();

            dateTimeServices.UtcNow.Returns(new DateTime(2020, 1, 1));


            var simpleTimer = new MyLib.SimpleTimer(
                dateTimeServices: dateTimeServices,
                interval: timerInterval);


            dateTimeServices.UtcNow.Returns(
                dateTimeServices.UtcNow +
                TimeSpan.FromSeconds(secondsElapsed));

            simpleTimer.Expired.Should().BeFalse();                        
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
