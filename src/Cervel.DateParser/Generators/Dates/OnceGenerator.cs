using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class OnceGenerator<T> : IGenerator<T>
        where T : ITimeInterval<T>
    {
        public string Name { get; }
        private T _value;

        public OnceGenerator(
            T value,
            string name = null)
        {
            _value = value;
            Name = name ?? $"Once<{value}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            if (fromDate < _value.End)
                yield return _value;
        }
    }
}
