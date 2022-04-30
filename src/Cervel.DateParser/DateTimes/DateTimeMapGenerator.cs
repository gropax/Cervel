using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class DateTimeMapGenerator : DateTimeGenerator
    {
        private IGenerator<DateTime> _generator;
        private Func<IEnumerable<DateTime>, IEnumerable<DateTime>> _modifier;

        public DateTimeMapGenerator(IGenerator<DateTime> generator, Func<IEnumerable<DateTime>, IEnumerable<DateTime>> modifier)
        {
            _generator = generator;
            _modifier = modifier;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            return _modifier(_generator.Generate(fromDate));
        }
    }
}
