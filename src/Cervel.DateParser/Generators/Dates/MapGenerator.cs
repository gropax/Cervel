using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class MapGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private readonly IGenerator<Date> _generator;
        private readonly Func<IEnumerable<Date>, IEnumerable<Date>> _modifier;

        public MapGenerator(
            IGenerator<Date> generator,
            Func<IEnumerable<Date>, IEnumerable<Date>> modifier,
            string name = null)
        {
            _generator = generator;
            _modifier = modifier;
            Name = name ?? $"Map<{generator.Name}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            return _modifier(_generator.Generate(fromDate));
        }
    }
}
