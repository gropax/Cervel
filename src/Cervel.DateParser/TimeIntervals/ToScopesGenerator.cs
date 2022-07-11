using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    /// <summary>
    /// Comme ToIntervalsGenerator, mais au lieu de fusionner les intervalles qui devraient se 
    /// rencontrer, on les rétrécie (par la fin) pour qu'ils se touchent sans se chevaucher.
    /// </summary>
    public class ToScopesGenerator : TimeIntervalGenerator
    {
        private IGenerator<Date> _generator;
        private TimeSpan _timeSpan;

        public ToScopesGenerator(
            IGenerator<Date> generator,
            TimeSpan timeSpan,
            string name = null)
            : base(name ?? $"ToScopes<{timeSpan}, {generator.Name}>")
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var t = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var next = enumerator.Current;
                yield return GetInterval(t.DateTime, next.DateTime);
                t = next;
            }

            yield return GetInterval(t.DateTime, null);
        }

        private TimeInterval GetInterval(DateTime t, DateTime? next)
        {
            var end = t + _timeSpan;
            if (next.HasValue)
                end = Time.Min(end, next.Value);

            return new TimeInterval(t, end);
        }
    }
}
