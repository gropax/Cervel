using Antlr4.Runtime;
using Cervel.TimeParser;

namespace Cervel.TimeParser
{
    public class TimeSpanListener : TimeSpanBaseListener
    {
        private bool _parseDateTime;
        public TimeSpanListener(bool parseDateTime)
        {
            _parseDateTime = parseDateTime;
        }

        public ITimeSpanGenerator TimeSpanGenerator { get; set; }
        public IDateTimeGenerator DateTimeGenerator { get; set; }
        
        private ITimeSpanGenerator _generator;
        private IDateTimeGenerator _dateTimeGenerator;

        public override void ExitDateTimes(TimeSpanParser.DateTimesContext context)
        {
            DateTimeGenerator = _dateTimeGenerator;
        }

        public override void ExitTimeSpans(TimeSpanParser.TimeSpansContext context)
        {
            TimeSpanGenerator = _generator;
        }

        public override void ExitAlways(TimeSpanParser.AlwaysContext context)
        {
            _generator = new TimeSpans.AlwaysGenerator();
        }

        public override void ExitNever(TimeSpanParser.NeverContext context)
        {
            if (_parseDateTime)
                _dateTimeGenerator = new DateTimes.NeverGenerator();
            else
                _generator = new TimeSpans.NeverGenerator();
        }

        
        private void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }
}
