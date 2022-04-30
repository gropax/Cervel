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
    }
}
