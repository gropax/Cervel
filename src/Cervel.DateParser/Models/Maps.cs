using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser
{
    public delegate IEnumerable<U> StrictOrderPreservingMap<T, U>(IEnumerable<T> elements);
    public delegate IEnumerable<U> OrderPreservingMap<T, U>(IEnumerable<T> elements);

    public static class Maps
    {
        public static StrictOrderPreservingMap<T, Date> StartDate<T>()
            where T : ITimeInterval<T>
        {
            return (ts) => ts.Select(t => t.StartDate);
        }

        public static StrictOrderPreservingMap<T, TimeInterval> ToTimeInterval<T>()
            where T : ITimeInterval<T>
        {
            return (ts) => ts.Select(t => t.ToTimeInterval());
        }

        public static StrictOrderPreservingMap<T, T> Take<T>(int i) => (ts) => ts.Take(i);
        public static StrictOrderPreservingMap<T, T> Skip<T>(int i) => (ts) => ts.Skip(i);
        public static StrictOrderPreservingMap<T, T> NEveryM<T>(int n, int m)
        {
            if (n >= m)
                throw new ArgumentException($"n must be < than m");

            return (ts) => ts.Where((t, i) => i % m < n);
        }

        public static StrictOrderPreservingMap<T, T> Filter<T>(Func<T, bool> filter) => (ts) => ts.Where(t => filter(t));
        public static OrderPreservingMap<Date, Date> StartOfMonth() => (ds) => ds.Select(d => new Date(new DateTime(d.DateTime.Year, d.DateTime.Month, 1)));  // @refactor

        public static OrderPreservingMap<Date, Date> ShiftMonth(int n)
        {
            var timeMeasure = new MonthMeasure(n);
            return (ds) => ds.Select(d => timeMeasure.AddTo(d));
        }
    }

}
