using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class ListGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        public static ListGenerator Create(IEnumerable<TimeInterval> values)
        {
            var cleanValues = new HashSet<TimeInterval>(values).OrderBy(d => d).ToArray();
            return new ListGenerator(cleanValues);
        }

        private TimeInterval[] _values;
        public ListGenerator(
            TimeInterval[] values,
            string name = null)
        {
            _values = values;
            Name = name ?? $"List<[{values.Length}]>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value.Start >= fromDate)
                    yield return value;
            }
        }
    }
}
