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
        public double StartValue { get; }
        public double EndValue { get; }

        public bool IsZero => StartValue == 0 && EndValue == 0;

        public FuzzyInterval(DateTime start, DateTime end, double startValue, double endValue)
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

        private static void ValidateFuzzyValue(double startValue)
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
            double endValue;
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

        public double ValueAt(DateTime date)
        {
            var fromStart = (date - Start).TotalMilliseconds;
            var toEnd = (End - date).TotalMilliseconds;

            if (fromStart * toEnd < 0)
                throw new Exception("Date do not belong to interval");

            return (StartValue * fromStart + EndValue * toEnd) / (fromStart + toEnd);
        }
    }
}
