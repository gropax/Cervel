using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeUnit<out T> : ITimeInterval<T>
    {
        T Next(int number = 1);
    }
}
