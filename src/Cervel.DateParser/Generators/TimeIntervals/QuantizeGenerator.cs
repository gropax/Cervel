using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class QuantizeGenerator<T> : IGenerator<TimeInterval<double>>
        where T : ITimeUnit<T>
    {
        public string Name { get; }
        private IGenerator<T> _partition;
        private IGenerator<TimeInterval> _generator;

        public QuantizeGenerator(
            ITimeMeasure<T> timeMeasure,
            IGenerator<TimeInterval> generator,
            string name = null)
        {
            _generator = generator;
            _partition = new EveryUnitGenerator<T>(timeMeasure);
            Name = name ?? $"Quantize<{generator.Name}>";
        }

        public IEnumerable<TimeInterval<double>> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = _generator.Generate(fromDate, toDate).GetEnumerator();
            var quantEnum = _partition.Generate(fromDate, toDate).GetEnumerator();

            if (!quantEnum.MoveNext())
                yield break;

            var quant = quantEnum.Current;
            var intersection = TimeSpan.Zero;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                while (quant.End <= current.Start)
                {
                    yield return new TimeInterval<double>(quant.Start, quant.End, intersection / quant.Length);
                    intersection = TimeSpan.Zero;

                    if (quantEnum.MoveNext())
                        quant = quantEnum.Current;
                    else
                        yield break;
                }

                while (quant.End < current.End)
                {
                    var interStart = Time.Max(current.Start, quant.Start);
                    var interEnd = Time.Min(current.End, quant.End);
                    intersection += interEnd - interStart;

                    yield return new TimeInterval<double>(quant.Start, quant.End, intersection / quant.Length);
                    intersection = TimeSpan.Zero;

                    if (quantEnum.MoveNext())
                        quant = quantEnum.Current;
                    else
                        yield break;
                } 

                if (quant.End == current.End)
                {
                    var interStart = Time.Max(current.Start, quant.Start);
                    var interEnd = Time.Min(current.End, quant.End);
                    intersection += interEnd - interStart;

                    yield return new TimeInterval<double>(quant.Start, quant.End, intersection / quant.Length);
                    intersection = TimeSpan.Zero;

                    if (quantEnum.MoveNext())
                        quant = quantEnum.Current;
                    else
                        yield break;

                    continue;
                }

                if (quant.End > current.End)
                {
                    var interStart = Time.Max(current.Start, quant.Start);
                    var interEnd = Time.Min(current.End, quant.End);
                    intersection += interEnd - interStart;

                    continue;
                }
            }

            do
            {
                quant = quantEnum.Current;
                yield return new TimeInterval<double>(quant.Start, quant.End, intersection / quant.Length);
                intersection = TimeSpan.Zero;
            } while (quantEnum.MoveNext());
        }

        public IEnumerable<TimeInterval<double>> Generate(DateTime fromDate) => Generate(fromDate, DateTime.MaxValue);
    }
}
