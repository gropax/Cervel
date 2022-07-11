using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class DeduplicateGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private readonly IGenerator<Date> _generator;

        public DeduplicateGenerator(
            IGenerator<Date> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"Dedup<{generator.Name}>";
        }


        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var last = enumerator.Current;
            yield return last;

            while (enumerator.MoveNext())
            {
                if (((ITimeInterval)enumerator.Current).IsAfter(last))
                {
                    last = enumerator.Current;
                    yield return last;
                }
            }
        }
    }
}
