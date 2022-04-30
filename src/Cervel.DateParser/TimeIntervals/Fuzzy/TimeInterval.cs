using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals.Fuzzy
{
    [DebuggerDisplay("[{Start}, {End}]")]
    public struct FuzzyInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public decimal StartValue { get; }
        public decimal EndValue { get; }

        public FuzzyInterval(DateTime start, DateTime end, decimal startValue, decimal endValue)
        {
            ValidateInterval(start, end);
            ValidateFuzzyValue(startValue);

            Start = start;
            End = end;
            StartValue = startValue;
            EndValue = endValue;
        }

        private static void ValidateInterval(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new Exception($"Interval start must be < to its end.");
        }

        private static void ValidateFuzzyValue(decimal startValue)
        {
            if (startValue < 0 || startValue > 1)
                throw new Exception($"Fuzzy value must be in [0, 1] but was {startValue}.");
        }

        public override string ToString()
        {
            return $"[{Start}, {End}]";
        }

        public FuzzyInterval LeftCut(DateTime cut)
        {
            DateTime end;
            decimal endValue;
            if (cut < End)
            {
                end = cut;
                endValue = ValueAt(cut);
            }
            else
            {
                end = End;
                endValue = EndValue;
            }
            return new FuzzyInterval(Start, end, StartValue, endValue);
        }

        public decimal ValueAt(DateTime date)
        {
            var fromStart = (date - Start).Ticks;
            var toEnd = (End - date).Ticks;

            if (fromStart * toEnd < 0)
                throw new Exception("Date do not belong to interval");

            return (StartValue * fromStart + EndValue * toEnd) / (fromStart + toEnd);
        }
    }
}
