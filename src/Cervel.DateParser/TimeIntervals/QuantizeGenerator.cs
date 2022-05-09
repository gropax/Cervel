using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class QuantizeGenerator : IGenerator<TimeInterval<double>>
    {
        public string Name { get; }
        private IGenerator<TimeInterval> _partition;
        private IGenerator<TimeInterval> _generator;

        public QuantizeGenerator(
            ITimeMeasure timeMeasure,
            IGenerator<TimeInterval> generator,
            string name = null)
        {
            _generator = generator;
            _partition = Time.Frequency(timeMeasure).ToPartition();
            Name = name ?? $"Quantize({timeMeasure.Name})<{generator.Name}>";
        }

        public IEnumerable<TimeInterval<double>> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            var quantEnum = _partition.Generate(fromDate).GetEnumerator();

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

        public IEnumerable<TimeInterval<double>> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    if (enumerator.Current.Start < toDate)
                        yield return new TimeInterval<double>(enumerator.Current.Start, toDate, enumerator.Current.Value);

                    yield break;
                }
            }
        }
    }
}
