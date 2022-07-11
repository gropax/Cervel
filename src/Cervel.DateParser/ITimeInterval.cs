using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeInterval
    {
        DateTime Start { get; }
        DateTime End { get; }
    }

    public interface ITimeInterval<T> : ITimeInterval
    {
        T Cut(DateTime endTime);
    }
}
