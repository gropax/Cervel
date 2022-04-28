using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Extensions.Tests
{
    public class TimeIntervalExtensions : TestBase
    {
        [Fact]
        public void TestDisjunction_SortedInput()
        {
            var intervals = Intervals(
                Interval(0, 3),
                Interval(5, 7),
                Interval(5, 7),
                Interval(6, 8),
                Interval(6, 12),
                Interval(6, 9),
                Interval(11, 12),
                Interval(14, 15));

            var expected = Intervals(
                Interval(0, 3),
                Interval(5, 12),
                Interval(14, 15));

            Assert.Equal(expected, intervals.Disjunction());
        }

        [Fact]
        public void TestDisjunction_NonSortedInput()
        {
            var intervals = Intervals(
                Interval(0, 3),
                Interval(5, 7),
                Interval(5, 7),
                Interval(6, 8),
                Interval(6, 12),
                Interval(4, 9),  // <- wrong sorting here
                Interval(11, 12),
                Interval(14, 15));

            Assert.Throws<Exception>(() => intervals.Disjunction().ToArray());
        }

        private TimeInterval Interval(int start, int end)
        {
            return new TimeInterval(
                new DateTime().AddDays(start),
                new DateTime().AddDays(end));
        }
    }
}
