using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WhereIndex<T>(this IEnumerable<T> ts, Func<int, bool> indexSelector)
        {
            int idx = 0;
            foreach (var t in ts)
                if (indexSelector(idx++))
                    yield return t;
        }

        public static IEnumerable<U> SelectWithNext<T, U>(
            this IEnumerable<T> ts,
            Func<T, T, U> selector)
        {
            var enumerator = ts.GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var t = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var next = enumerator.Current;
                yield return selector(t, next);
                t = next;
            }

            yield return selector(t, default);
        }
    
        private static T[] ArgMinOrMax<T, U>(IEnumerable<T> ts, Func<T, U> selector, int cmpValue)
            where U : IComparable<U>
        {
            var enumerator = ts.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentNullException("The collection is empty.");

            List<T> argMax = new List<T>() { enumerator.Current };
            U max = selector(enumerator.Current);

            while (enumerator.MoveNext())
            {
                T t = enumerator.Current;
                U val = selector(t);

                if (val.CompareTo(max) == 0)
                {
                    argMax.Add(t);
                }
                else if (val.CompareTo(max) == cmpValue)
                {
                    max = val;
                    argMax.Clear();
                    argMax.Add(t);
                }
            }

            return argMax.ToArray();
        }

        public static T[] ArgMin<T, U>(this IEnumerable<T> ts, Func<T, U> selector)
            where U : IComparable<U>
        {
            return ArgMinOrMax(ts, selector, cmpValue: -1);
        }

        public static T[] ArgMax<T, U>(this IEnumerable<T> ts, Func<T, U> selector)
            where U : IComparable<U>
        {
            return ArgMinOrMax(ts, selector, cmpValue: 1);
        }

    }
}
