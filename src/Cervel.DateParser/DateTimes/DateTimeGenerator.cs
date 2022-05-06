﻿using System;
using System.Collections.Generic;

namespace Cervel.TimeParser
{
    public abstract class DateTimeGenerator : IGenerator<DateTime>
    {
        public abstract IEnumerable<DateTime> Generate(DateTime fromDate);

        public IEnumerable<DateTime> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current < toDate)
                yield return enumerator.Current;
        }
    }

    ///// <summary>
    ///// A frequency is a generator that produces a sequence starting from the starting date.
    ///// Therefore, the set of produced date may vary depending on the starting date.
    ///// </summary>
    //public interface IFrequency : ITimeGenerator<DateTime> { }
}
