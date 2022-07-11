using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class DayFilterGenerator : IGenerator<Date>
    {
        public string Name { get; }

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
        {
            _dateTimeSelector = dateTimeSelector;
            _indexSelector = indexSelector;
            _skip = skip;
            _take = take;
            Name = name ?? $"DayFilter<>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            var dates = DateTimesFrom(fromDate);

            if (_dateTimeSelector != null)
                dates = dates.Where(d => _dateTimeSelector(d.DateTime));

            if (_indexSelector != null)
                dates = dates.WhereIndex(i => _indexSelector(i));

            if (_skip.HasValue)
                dates = dates.Skip(_skip.Value);

            if (_take.HasValue)
                dates = dates.Take(_take.Value);

            return dates;
        }

        private IEnumerable<Date> DateTimesFrom(DateTime fromDate)
        {
            var timeSpan = TimeSpan.FromDays(1);
            var date = fromDate;

            while (date < DateTime.MaxValue)
            {
                yield return new Date(date);
                date = date.Shift(timeSpan);
            }

            yield return new Date(DateTime.MaxValue);
        }
    }
}
