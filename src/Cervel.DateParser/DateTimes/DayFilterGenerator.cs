﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.DateTimes
{
    public class DayFilterGenerator : DateTimeGenerator
    {
        private readonly Func<DateTime, bool> _dateTimeSelector;
        private readonly Func<int, bool> _indexSelector;
        private readonly int? _skip;
        private readonly int? _take;

        public DayFilterGenerator(
            Func<DateTime, bool> dateTimeSelector = null,
            Func<int, bool> indexSelector = null,
            int? skip = null,
            int? take = null,
            string name = null)
            : base(name ?? $"DayFilter<>")
        {
            _dateTimeSelector = dateTimeSelector;
            _indexSelector = indexSelector;
            _skip = skip;
            _take = take;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var dates = DateTimesFrom(fromDate);

            if (_dateTimeSelector != null)
                dates = dates.Where(d => _dateTimeSelector(d));

            if (_indexSelector != null)
                dates = dates.WhereIndex(i => _indexSelector(i));

            if (_skip.HasValue)
                dates = dates.Skip(_skip.Value);

            if (_take.HasValue)
                dates = dates.Take(_take.Value);

            return dates;
        }

        private IEnumerable<DateTime> DateTimesFrom(DateTime fromDate)
        {
            var timeSpan = TimeSpan.FromDays(1);
            var date = fromDate;

            while (date < DateTime.MaxValue)
            {
                yield return date;
                date = date.Shift(timeSpan);
            }

            yield return DateTime.MaxValue;
        }
    }
}
