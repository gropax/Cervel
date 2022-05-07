using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ListGenerator : DateTimeGenerator
    {
        public static ListGenerator Create(IEnumerable<DateTime> values)
        {
            var cleanValues = new HashSet<DateTime>(values).OrderBy(d => d).ToArray();
            return new ListGenerator(cleanValues);
        }

        private DateTime[] _values;
        public ListGenerator(
            DateTime[] values,
            string name = null)
            : base(name ?? $"List<[{values.Length}]>")
        {
            _values = values;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value >= fromDate)
                    yield return value;
            }
        }
    }
}
