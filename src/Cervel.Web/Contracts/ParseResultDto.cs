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
        public Dictionary<int, Dictionary<int, Dictionary<int, HighlightDto[]>>> Highlights { get; set; }
    }

    public class HighlightDto
    {
        public double Fraction { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
    }
}
