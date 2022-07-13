using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class UnionGenerator<T> : IGenerator<T>
        where T : ITimeMeasure<T>, IComparable<T>
    {
        public string Name { get; }
        private IGenerator<T>[] _generators;
        public UnionGenerator(
            IGenerator<T>[] generators,
            string name = null)
        {
            _generators = generators;
            Name = name ?? $"Union<{string.Join(", ", generators.Select(g => g.Name))}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            // Initialize dictionary of enumerators
            var dict = new Dictionary<IEnumerator<T>, T>();
            for (int i = 0; i < _generators.Length; i++)
            {
                var enumerator = _generators[i].Generate(fromDate).GetEnumerator();
                if (enumerator.MoveNext())
                    dict[enumerator] = enumerator.Current;
            }

            while (dict.Count() > 0)
            {
                var argMin = dict.ArgMin(kv => kv.Value);

                foreach (var kv in argMin)
                {
                    if (kv.Key.MoveNext())
                        dict[kv.Key] = kv.Key.Current;
                    else
                        dict.Remove(kv.Key);
                }

                yield return argMin[0].Value;
            }
        }
    }
}
