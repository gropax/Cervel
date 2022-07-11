using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cervel.TimeParser
{
    [DebuggerDisplay("{Name}")]
    public abstract class DateTimeGenerator : IGenerator<Date>
    {
        public string Name { get; }
        protected DateTimeGenerator(string name)
        {
            Name = name;
        }

        public abstract IEnumerable<Date> Generate(DateTime fromDate);
    }
}
