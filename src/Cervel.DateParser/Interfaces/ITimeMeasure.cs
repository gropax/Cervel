using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeMeasure<out T> : ITimeInterval<T>
    {
        T Increment(int number);
    }
}
