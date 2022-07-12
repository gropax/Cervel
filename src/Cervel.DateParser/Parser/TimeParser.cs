using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Cervel.TimeParser.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class TimeParser
    {
        public ParseResult<IGenerator<Date>> ParseDateTimes(string input)
        {
            return ParseSymbolV2(input, true, (p) => p.dates(), (l) => l.DateDistribution);
        }

        public ParseResult<IGenerator<TimeInterval>> ParseTimeIntervals(string input)
        {
            return ParseSymbolV2(input, false, (p) => p.intervals(), (l) => l.IntervalDistribution);
        }

        private ParseResult<TResult> ParseSymbolV2<TResult>(
            string input,
            bool parseDateTime,
            Func<TimeExpressionV2Parser, ParserRuleContext> contextSelector,
            Func<TimeExpressionV2Listener, TResult> resultSelector)
        {
            var preprocessed = PreprocessInput(input);

            var inputStream = new AntlrInputStream(preprocessed);
            var lexer = new TimeExpressionV2Lexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new TimeExpressionV2Parser(commonTokenStream);

            var listener = new TimeExpressionV2Listener();
            var walker = new ParseTreeWalker();
            var context = contextSelector(parser);
            //var context = parser.timeSpans();
            string tree = context.ToStringTree();
            walker.Walk(listener, context);

            bool completelyParsed =
                context.Start.StartIndex == 0 &&
                context.Stop.StopIndex == preprocessed.Length - 1;

            if (context.exception != null || !completelyParsed)
            {
                return new ParseResult<TResult>(input, false);
            }
            else
            {
                var result = resultSelector(listener);
                return new ParseResult<TResult>(input, true, result);
            }
        }

        private Regex _duRegex = new Regex(@"\bdu\b");
        private Regex _desRegex = new Regex(@"\bdes\b");
        private Regex _auRegex = new Regex(@"\bau\b");
        private Regex _auxRegex = new Regex(@"\baux\b");
        private string PreprocessInput(string input)
        {
            var clean = input.Trim()
                .Replace('-', ' ')
                .Replace('é', 'e')
                .Replace('è', 'e')
                .Replace('ê', 'e');

            clean = _duRegex.Replace(clean, "de le");
            clean = _desRegex.Replace(clean, "de les");
            clean = _auRegex.Replace(clean, "a le");
            clean = _auxRegex.Replace(clean, "a les");

            return clean;
        }
    }
}
