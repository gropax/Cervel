using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class UnionGenerator : DateTimeGenerator
    {
        private IGenerator<Date>[] _generators;
        public UnionGenerator(
            IGenerator<Date>[] generators,
            string name = null)
            : base(name ?? $"Union<{string.Join(", ", generators.Select(g => g.Name))}>")
        {
            _generators = generators;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            // Initialize dictionary of enumerators
            var dict = new Dictionary<IEnumerator<Date>, Date>();
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
