using Cervel.TimeParser.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface IGenerator<out T>
    {
        string Name { get; }
        IEnumerable<T> Generate(DateTime fromDate);
        IEnumerable<T> Generate(ITimeInterval interval) => Generate(interval.Start, interval.End);
        IEnumerable<T> Generate(DateTime fromDate, DateTime toDate);
    }
}
