using Cervel.TimeParser.Dates;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class DayGeneratorExtensions
    {
        public static IGenerator<Day> Where(
            this IGenerator<Day> generator,
            DayOfWeek dow)
        {
            return generator.Map(Maps.Filter<Day>(d => d.DayOfWeek == dow));
        }
    }
}
