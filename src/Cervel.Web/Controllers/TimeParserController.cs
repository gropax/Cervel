using Cervel.TimeParser;
using Cervel.TimeParser.Extensions;
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

            var result = parser.ParseTimeIntervals(timeExpr);

            var intervals = result.IsSuccess
                ? result.Value.Generate(_fromDate, _toDate).ToArray()
                : new TimeInterval[0];

            var dayHighlights = GetDayHighlights(result.Value);

            return Ok(new ParseResultDto()
            {
                TimeExpr = timeExpr,
                IsSuccess = result.IsSuccess,
                Intervals = intervals,
                DayHighlights = dayHighlights,
            });
        }

        private DayHighlightsDto[] GetDayHighlights(IGenerator<TimeInterval> generator)
        {
            var dayCutGenerator = generator.Partition(Time.Start().Date().Daily());
            var dayCuts = dayCutGenerator.Generate(_fromDate, _toDate);
            var dayHighlights = dayCuts.Select(i => GetDayHighlight(i)).ToArray();
            return dayHighlights;
        }

        private DayHighlightsDto GetDayHighlight(TimeInterval i)
        {
            
            throw new NotImplementedException();
        }
    }
}
