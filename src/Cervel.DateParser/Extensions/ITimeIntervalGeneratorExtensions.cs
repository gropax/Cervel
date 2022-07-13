using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Generators.DayIntervals;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class ITimeIntervalGeneratorExtensions
    {
        public static IGenerator<U> Map<T, U>(
            this IGenerator<T> generator,
            StrictOrderPreservingMap<T, U> map,
            string name = null)
            where T : ITimeInterval<T>
            where U : ITimeInterval<U>
        {
            return new MapGenerator<T, U>(generator, map.Invoke, name);
        }

        public static IGenerator<U> Map<T, U>(
            this IGenerator<T> generator,
            OrderPreservingMap<T, U> map,
            string name = null)
            where T : ITimeInterval<T>
            where U : ITimeInterval<U>
        {
            return new MapGenerator<T, U>(generator, map.Invoke, name);
        }

        public static IGenerator<T> Take<T>(
            this IGenerator<T> generator,
            int n,
            string name = null)
            where T : ITimeInterval<T>
        {
            return generator.Map(Maps.Take<T>(n), name ?? $"Take<{n}, {generator.Name}>");
        }

        public static IGenerator<T> Skip<T>(
            this IGenerator<T> generator,
            int n,
            string name = null)
            where T : ITimeInterval<T>
        {
            return generator.Map(Maps.Skip<T>(n), name ?? $"Skip<{n}, {generator.Name}>");
        }

        public static IGenerator<T> NEveryM<T>(
            this IGenerator<T> generator,
            int n,
            int m,
            string name = null)
            where T : ITimeInterval<T>
        {
            return generator.Map(Maps.NEveryM<T>(n, m), name ?? $"NEveryM<{n}, {m}, {generator.Name}>");
        }

        public static IGenerator<T> First<T>(
            this IGenerator<T> generator,
            string name = null)
            where T : ITimeInterval<T>
        {
            return generator.Map(Maps.Take<T>(1), name ?? $"First<{generator.Name}>");
        }

        public static IGenerator<Date> StartDate<T>(
            this IGenerator<T> generator,
            string name = null)
            where T : ITimeInterval<T>
        {
            return generator.Map(Maps.StartDate<T>(), name ?? $"Start<{generator.Name}>");
        } 

        public static IGenerator<Day> CoveringDays<T>(
            this IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new CoveringDaysGenerator<T>(generator);
        }

        public static IGenerator<TimeInterval> Coalesce<T>(
            this IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new CoalesceGenerator<T>(generator);
        }
    }
}
