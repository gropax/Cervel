using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class EveryUnitGenerator<T> : IGenerator<T>
        where T : ITimeUnit<T>
    {
        private ITimeMeasure<T> _timeMeasure;
        public string Name { get; }

        public EveryUnitGenerator(
            ITimeMeasure<T> timeMeasure,
            string name = null)
        {
            _timeMeasure = timeMeasure;
            Name = name ?? $"Every{timeMeasure.Name}";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            var unit = _timeMeasure.GetUnit(fromDate);

            yield return unit;

            while (true)
            {
                unit = unit.Next();

                yield return unit;
            }
        }
    }
}
