using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class MapGenerator : DateTimeGenerator
    {
        private readonly IGenerator<Date> _generator;
        private readonly Func<IEnumerable<Date>, IEnumerable<Date>> _modifier;

        public MapGenerator(
            IGenerator<Date> generator,
            Func<IEnumerable<Date>, IEnumerable<Date>> modifier,
            string name = null)
            : base(name ?? $"Map<{generator.Name}>")
        {
            _generator = generator;
            _modifier = modifier;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            return _modifier(_generator.Generate(fromDate));
        }
    }
}
