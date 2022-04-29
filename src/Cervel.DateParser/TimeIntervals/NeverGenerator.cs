﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class NeverGenerator : TimeIntervalGenerator
    {
        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
