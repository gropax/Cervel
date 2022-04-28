﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Tests
{
    public class TestBase
    {
        protected DateTime[] Dates(params DateTime[] dateTimes) => dateTimes;
        protected DateTime Day(int year, int month, int day) => new DateTime(year, month, day);

        protected TimeInterval[] Intervals(params TimeInterval[] timeSpans) => timeSpans;
        protected TimeInterval DayInterval(int year, int month, int day)
        {
            var start = new DateTime(year, month, day);
            return new TimeInterval(
                start: start,
                end: start + TimeSpan.FromDays(1));
        }
    }
}