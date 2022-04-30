using Cervel.TimeParser;
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
        
        /// <summary>
        /// Parse a time expression describing time intevals
        /// </summary>
        [HttpGet("intervals")]
        public IActionResult ParseIntervals([FromQuery] string timeExpr)
        {
            var parser = new TimeParser.TimeParser();

            var result = parser.ParseTimeIntervals(timeExpr);

            var intervals = result.IsSuccess
                ? result.Value.Generate(new DateTime(2022, 1, 1)).ToArray()
                : new TimeInterval[0];

            return Ok(new ParseResultDto()
            {
                TimeExpr = timeExpr,
                IsSuccess = result.IsSuccess,
                Intervals = intervals,
            });
        }

    }
}
