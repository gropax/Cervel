using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class PartitionGenerator : TimeIntervalGenerator
    {
        private IGenerator<TimeInterval> _generator;
        private IGenerator<DateTime> _cutGenerator;

        public PartitionGenerator(
            IGenerator<TimeInterval> generator,
            IGenerator<DateTime> cutGenerator,
            string name = null)
            : base(name ?? $"Partition<{cutGenerator.Name}, {generator.Name}>")
        {
            _generator = generator;
            _cutGenerator = cutGenerator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
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
                        cut = cutEnumerator.Current;
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
