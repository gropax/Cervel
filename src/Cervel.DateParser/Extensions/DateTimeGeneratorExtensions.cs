using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class DateTimeGeneratorExtensions
    {
        public static IDateTimeGenerator Shift(this IDateTimeGenerator generator, TimeSpan timeSpan)
        {
            return new ShiftGenerator(generator, timeSpan);
        }
    }
}
