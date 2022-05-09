using Cervel.TimeParser;
using Cervel.TimeParser.Extensions;
using Cervel.TimeParser.TimeIntervals.Fuzzy;
using Cervel.Web.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Controllers
{
    [Route("api/time-parser")]
    [ApiController]
    public class TimeParserController : ControllerBase
    {
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _toDate = new DateTime(2023, 1, 1);
        
        /// <summary>
        /// Parse a time expression describing time intevals
        /// </summary>
        [HttpGet("intervals")]
        public IActionResult ParseIntervals([FromQuery] string timeExpr)
        {
            var parser = new TimeParser.TimeParser();

            var result = parser.ParseTimeIntervals(timeExpr, parserVersion: 2);

            var intervals = result.IsSuccess
                ? result.Value.Generate(_fromDate, _toDate).ToArray()
                : new TimeInterval[0];

            var dayHighlights = result.IsSuccess
                ? GetDayHighlights(result.Value)
                : new Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>>();

            return Ok(new ParseResultDto()
            {
                TimeExpr = timeExpr,
                IsSuccess = result.IsSuccess,
                Intervals = intervals,
                Highlights = dayHighlights,
            });
        }

        private Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>> GetDayHighlights(
            IGenerator<TimeInterval> generator)
        {
            var highlightDict = new Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>>();

            var dayCuts = generator
                .Quantize(new DayMeasure())
                .Generate(_fromDate, _toDate)
                .ToArray();

            foreach (var dayCut in dayCuts)
                AddDayHighlight(highlightDict, dayCut);

            return highlightDict;
        }

        private void AddDayHighlight(
            Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>> highlightDict,
            TimeInterval<double> i)
        {
            double value = i.Value > 0 ? 1 : 0;
            if (value == 0)
                return;

            var highlights = new HighlightDto[]
            {
                new HighlightDto()
                {
                    Fraction = 1,
                    StartValue = value,
                    EndValue = value,
                },
            };

            if (!highlightDict.TryGetValue(i.Start.Year, out var yearHighlights))
            {
                yearHighlights = new Dictionary<int, Dictionary<int, HighlightDto[]>>();
                highlightDict[i.Start.Year] = yearHighlights;
            }

            if (!yearHighlights.TryGetValue(i.Start.Month, out var monthHighlights))
            {
                monthHighlights = new Dictionary<int, HighlightDto[]>();
                yearHighlights[i.Start.Month] = monthHighlights;
            }

            monthHighlights[i.Start.Day] = highlights;
        }

        private void AddDayHighlight(
            Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>> highlightDict,
            FuzzyInterval[] intervals)
        {
            var fst = intervals[0];
            var lst = intervals[^1];
            var total = lst.End.Ticks - fst.Start.Ticks;

            var highlights = intervals
                .Where(i => !i.IsZero)
                .Select(i =>
                    new HighlightDto()
                    {
                        Fraction = (i.End.Ticks - i.Start.Ticks) / total,
                        StartValue = i.StartValue,
                        EndValue = i.EndValue,
                    })
                .ToArray();

            if (highlights.Length == 0)
                return;

            if (!highlightDict.TryGetValue(fst.Start.Year, out var yearHighlights))
            {
                yearHighlights = new Dictionary<int, Dictionary<int, HighlightDto[]>>();
                highlightDict[fst.Start.Year] = yearHighlights;
            }

            if (!yearHighlights.TryGetValue(fst.Start.Month, out var monthHighlights))
            {
                monthHighlights = new Dictionary<int, HighlightDto[]>();
                yearHighlights[fst.Start.Month] = monthHighlights;
            }

            monthHighlights[fst.Start.Day] = highlights;
        }
    }
}
