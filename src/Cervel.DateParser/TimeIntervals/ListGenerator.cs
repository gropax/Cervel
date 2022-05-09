using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ListGenerator : TimeIntervalGenerator
    {
        public static ListGenerator Create(IEnumerable<TimeInterval> values)
        {
            var cleanValues = new HashSet<TimeInterval>(values).OrderBy(d => d).ToArray();
            return new ListGenerator(cleanValues);
        }

        private TimeInterval[] _values;
        public ListGenerator(
            TimeInterval[] values,
            string name = null)
            : base(name ?? $"List<[{values.Length}]>")
        {
            _values = values;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value.Start >= fromDate)
                    yield return value;
            }
        }
    }
}
