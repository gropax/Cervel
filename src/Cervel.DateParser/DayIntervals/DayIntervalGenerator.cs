using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Models
{
    public class DayIntervalGenerator : IGenerator<DayInterval>
    {
        public string Name => throw new NotImplementedException();

        public IEnumerable<DayInterval> Generate(DateTime fromDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DayInterval> Generate(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}
