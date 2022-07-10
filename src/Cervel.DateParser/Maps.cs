using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser
{
    public delegate IEnumerable<T> StrictOrderPreservingMap<T>(IEnumerable<T> elements);
    public delegate IEnumerable<T> OrderPreservingMap<T>(IEnumerable<T> elements);

    public static class Maps
    {
        public static StrictOrderPreservingMap<T> Take<T>(int i) => (ts) => ts.Take(i);
        public static StrictOrderPreservingMap<T> Skip<T>(int i) => (ts) => ts.Skip(i);
        public static StrictOrderPreservingMap<T> NEveryM<T>(int n, int m)
        {
            if (n >= m)
                throw new ArgumentException($"n must be < than m");

            return (ts) => ts.Where((t, i) => i % m < n);
        }

        public static StrictOrderPreservingMap<T> Filter<T>(Func<T, bool> filter) => (ts) => ts.Where(t => filter(t));
        public static OrderPreservingMap<DateTime> StartOfDay() => (ds) => ds.Select(d => d.Date);
        public static OrderPreservingMap<DateTime> StartOfMonth() => (ds) => ds.Select(d => new DateTime(d.Year, d.Month, 1));

        //public static StrictOrderPreservingMap<DateTime> ShiftDays<DateTime>(int n)
        //    => (ds) => ds.Select(d => d.Shift(TimeSpan.FromDays(n)));

        public static StrictOrderPreservingMap<DateTime> ShiftDays(int n)
        {
            var timeMeasure = new DayMeasure(n);
            return (ds) => ds.Select(d => timeMeasure.AddTo(d));
        }

        public static OrderPreservingMap<DateTime> ShiftMonth(int n)
        {
            var timeMeasure = new MonthMeasure(n);
            return (ds) => ds.Select(d => timeMeasure.AddTo(d));
        }

        public static OrderPreservingMap<DateTime> ShiftYears(int n)
        {
            var timeMeasure = new YearMeasure(n);
            return (ds) => ds.Select(d => timeMeasure.AddTo(d));
        }
    }

}
