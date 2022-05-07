using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public delegate IEnumerable<T> StrictOrderPreservingMap<T>(IEnumerable<T> elements);
    public delegate IEnumerable<T> OrderPreservingMap<T>(IEnumerable<T> elements);

    public static class Maps
    {
        public static StrictOrderPreservingMap<T> Take<T>(int i) => (ts) => ts.Take(i);
        public static StrictOrderPreservingMap<T> Skip<T>(int i) => (ts) => ts.Skip(i);
        public static StrictOrderPreservingMap<T> Filter<T>(Func<T, bool> filter) => (ts) => ts.Where(t => filter(t));
        public static OrderPreservingMap<DateTime> StartOfDay() => (ds) => ds.Select(d => d.Date);
        public static OrderPreservingMap<DateTime> StartOfMonth() => (ds) => ds.Select(d => new DateTime(d.Year, d.Month, 1));
    }

}
