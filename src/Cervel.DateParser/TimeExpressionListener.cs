using Antlr4.Runtime;
using Cervel.TimeParser;

namespace Cervel.TimeParser
{
    public class TimeExpressionListener : TimeExpressionBaseListener
    {
        private bool _parseDateTime;
        public TimeExpressionListener(bool parseDateTime)
        {
            _parseDateTime = parseDateTime;
        }

        public ITimeIntervalGenerator TimeIntervalGenerator { get; set; }
        public IDateTimeGenerator DateTimeGenerator { get; set; }
        
        private ITimeIntervalGenerator _timeIntervalGenerator;
        private IDateTimeGenerator _dateTimeGenerator;

        public override void ExitDateTimes(TimeExpressionParser.DateTimesContext context)
        {
            DateTimeGenerator = _dateTimeGenerator;
        }

        public override void ExitTimeIntervals(TimeExpressionParser.TimeIntervalsContext context)
        {
            TimeIntervalGenerator = _timeIntervalGenerator;
        }

        public override void ExitAlways(TimeExpressionParser.AlwaysContext context)
        {
            _timeIntervalGenerator = new TimeIntervals.AlwaysGenerator();
        }

        public override void ExitNever(TimeExpressionParser.NeverContext context)
        {
            if (_parseDateTime)
                _dateTimeGenerator = new DateTimes.NeverGenerator();
            else
                _timeIntervalGenerator = new TimeIntervals.NeverGenerator();
        }

        
        private void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }
}
