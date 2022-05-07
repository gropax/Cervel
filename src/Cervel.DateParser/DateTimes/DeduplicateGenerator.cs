using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class DeduplicateGenerator : DateTimeGenerator
    {
        private readonly IGenerator<DateTime> _generator;

        public DeduplicateGenerator(
            IGenerator<DateTime> generator,
            string name = null)
            : base(name ?? $"Dedup<>")
        {
            _generator = generator;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var last = enumerator.Current;
            yield return last;

            while (enumerator.MoveNext())
            {
                if (enumerator.Current > last)
                {
                    last = enumerator.Current;
                    yield return last;
                }
            }
        }
    }
}
