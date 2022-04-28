using Antlr4.Runtime;
using Cervel.TimeParser.TimeSpans;

namespace Cervel.TimeParser
{
    public class TimeSpanListener : TimeSpanBaseListener
    {
        public ITimeSpanGenerator TimeSpanGenerator { get; set; }

        public override void ExitTimeSet(TimeSpanParser.TimeSetContext context)
        {
            //EnsureSuccess(context);
            TimeSpanGenerator = _generator;
        }
        
        private ITimeSpanGenerator _generator;
        public override void ExitAlways(TimeSpanParser.AlwaysContext context)
        {
            _generator = new AlwaysGenerator();
        }

        public override void ExitNever(TimeSpanParser.NeverContext context)
        {
            _generator = new NeverGenerator();
        }

        
        private void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }
}
