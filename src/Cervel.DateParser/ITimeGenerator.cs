using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeGenerator<T>
    {
        IEnumerable<T> Generate(DateTime fromDate);
        IEnumerable<T> Generate(DateTime fromDate, DateTime toDate);
        IEnumerable<T> Generate(TimeInterval interval) => Generate(interval.Start, interval.End);
    }
}
