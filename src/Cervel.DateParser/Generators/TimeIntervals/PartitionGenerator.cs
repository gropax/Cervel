using Cervel.TimeParser.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class PartitionGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<TimeInterval> _generator;
        private IGenerator<Date> _cutGenerator;

        public PartitionGenerator(
            IGenerator<TimeInterval> generator,
            IGenerator<Date> cutGenerator,
            string name = null)
        {
            _generator = generator;
            _cutGenerator = cutGenerator;
            Name = name ?? $"Partition<{cutGenerator.Name}, {generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            var cutEnumerator = _cutGenerator.Generate(fromDate).GetEnumerator();

            DateTime cut;
            DateTime? pendingCut = null;

            while (enumerator.MoveNext())
            {
                var interval = enumerator.Current;
                var cuts = new List<DateTime>() { interval.Start };

                while (true)
                {
                    if (pendingCut.HasValue)
                    {
                        cut = pendingCut.Value;
                        pendingCut = null;
                    }
                    else if (cutEnumerator.MoveNext())
                        cut = cutEnumerator.Current.DateTime;
                    else
                        break;

                    if (cut <= interval.Start)
                        continue;
                    else if (cut >= interval.End)
                    {
                        if (cut > interval.End)
                            pendingCut = cut;
                        break;
                    }
                    else
                        cuts.Add(cut);
                }

                cuts.Add(interval.End);

                for (int i = 0; i < cuts.Count - 1; i++)
                    yield return new TimeInterval(cuts[i], cuts[i + 1]);

                cuts.Clear();
            }
        }

        private DateTime Max(DateTime fst, DateTime snd)
        {
            return fst >= snd ? fst : snd;
        }

        private DateTime Min(DateTime fst, DateTime snd)
        {
            return fst <= snd ? fst : snd;
        }
    }
}
