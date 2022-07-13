using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class MapGenerator<T, U> : IGenerator<U>
        where T : ITimeInterval<T>
        where U : ITimeInterval<U>
    {
        public string Name { get; }
        private readonly IGenerator<T> _generator;
        private readonly Func<IEnumerable<T>, IEnumerable<U>> _modifier;

        public MapGenerator(
            IGenerator<T> generator,
            Func<IEnumerable<T>, IEnumerable<U>> modifier,
            string name = null)
        {
            _generator = generator;
            _modifier = modifier;
            Name = name ?? $"Map<{generator.Name}>";
        }

        public IEnumerable<U> Generate(DateTime fromDate)
        {
            return _modifier(_generator.Generate(fromDate));
        }
    }
}
