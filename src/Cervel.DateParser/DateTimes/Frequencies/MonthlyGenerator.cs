﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class MonthlyGenerator : FrequencyGenerator
    {
        private int _factor;
        public MonthlyGenerator(int factor)
        {
            _factor = factor;
        }

        protected override DateTime GetNext(DateTime date)
        {
            int virtMonth = date.Month + _factor;
            int year = date.Year + virtMonth / 12;
            int month = virtMonth % 12;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, Math.Min(date.Day, daysInMonth));
        }
    }
}
