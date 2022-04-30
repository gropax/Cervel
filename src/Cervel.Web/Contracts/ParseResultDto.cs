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
    }
}
