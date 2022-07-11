using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.DateTimes
{
    [DebuggerDisplay("{Name}")]
    public class ListGenerator : IGenerator<Date>
    {
        public string Name { get; }
        public static ListGenerator Create(IEnumerable<Date> values)
        {
            var cleanValues = new HashSet<Date>(values).OrderBy(d => d).ToArray();
            return new ListGenerator(cleanValues);
        }

        private Date[] _values;
        public ListGenerator(
            Date[] values,
            string name = null)
        {
            _values = values;
            Name = name ?? $"List<[{values.Length}]>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value.DateTime >= fromDate)
                    yield return value;
            }
        }
    }
}
