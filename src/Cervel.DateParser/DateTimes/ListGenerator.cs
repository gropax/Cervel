using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ListGenerator : DateTimeGenerator
    {
        public static ListGenerator Create(IEnumerable<Date> values)
        {
            var cleanValues = new HashSet<Date>(values).OrderBy(d => d).ToArray();
            return new ListGenerator(cleanValues);
        }

        private Date[] _values;
        public ListGenerator(
            Date[] values,
            string name = null)
            : base(name ?? $"List<[{values.Length}]>")
        {
            _values = values;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value.DateTime >= fromDate)
                    yield return value;
            }
        }
    }
}
