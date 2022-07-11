using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Generic
{
    public class NeverGenerator<T> : IGenerator2<T> where T : ITimeInterval<T>
    {
        public string Name { get; }
        public NeverGenerator(string name = null)
        {
            Name = name ?? "Never";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
