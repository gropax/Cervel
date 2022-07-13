using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Generators.DayIntervals
{
    [DebuggerDisplay("{Name}")]
    public class IncrementGenerator<T> : IGenerator<T>
        where T : ITimeUnit<T>
    {
        private IGenerator<T> _generator;
        private int _shift { get; }
        public string Name { get; }

        public IncrementGenerator(IGenerator<T> generator, int shift,
            string name = null)
        {
            _generator = generator;
            _shift = shift;
            Name = name ?? $"Shift<{_shift}, {_generator.Name}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            foreach (var t in _generator.Generate(fromDate))
                yield return t.Next(_shift);
        }
    }
}
