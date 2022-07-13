using Cervel.TimeParser.Dates;
using Cervel.TimeParser.TimeIntervals;
using Cervel.TimeParser.TimeIntervals.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class TimeIntervalGeneratorExtensions
    {
        public static IGenerator<FuzzyInterval> ToFuzzy(this IGenerator<TimeInterval> generator)
        {
            return new CrispToFuzzyGenerator(generator);
        }

        public static IGenerator<T> Shift<T>(
            this IGenerator<T> generator,
            TimeSpan timeSpan)
            where T : ITimeInterval<T>
        {
            return new ShiftGenerator<T>(generator, timeSpan);
        }

        public static IGenerator<TimeInterval<double>> Quantize<T>(
            this IGenerator<TimeInterval> g,
            ITimeMeasure<T> timeMeasure)
            where T : ITimeUnit<T>
        {
            return Time.Quantize(timeMeasure, g);
        } 
            
    }
}
