﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.DateTimes
{
    public class DailyGenerator : FrequencyGenerator
    {
        private TimeSpan _timeSpan;
        public DailyGenerator(
            TimeSpan? timeSpan = null,
            string name = null)
            : base(name ?? $"Daily<{timeSpan}>")
        {
            _timeSpan = timeSpan ?? TimeSpan.FromDays(1);
        }

        protected override DateTime GetNext(DateTime date) => date.Shift(_timeSpan);
    }
}
