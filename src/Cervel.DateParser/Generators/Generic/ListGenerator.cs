using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class ListGenerator<T> : IGenerator<T> where T : ITimeInterval<T>
    {
        public string Name { get; }
        public static ListGenerator<T> Create(IEnumerable<T> values)
        {
            var cleanValues = new HashSet<T>(values).OrderBy(d => d).ToArray();
            return new ListGenerator<T>(cleanValues);
        }

        private T[] _values;
        public ListGenerator(
            T[] values,
            string name = null)
        {
            _values = values;
            Name = name ?? $"List<[{values.Length}]>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            foreach (var value in _values)
            {
                if (value.Start >= fromDate)
                    yield return value;
            }
        }
    }
}
