using Cervel.TimeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Contracts
{
    public class ParseResultDto
    {
        public string TimeExpr { get; set; }
        public bool IsSuccess { get; set; }
        public TimeInterval[] Intervals { get; set; }
        public DayHighlightsDto[] DayHighlights { get; set; }
    }

    public class DayHighlightsDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public Highlight[] Highlights { get; set; }
    }

    public class Highlight
    {
        public double StartFraction { get; set; }
        public double StartValue { get; set; }
        public double EndFraction { get; set; }
        public double EndValue { get; set; }
    }
}
