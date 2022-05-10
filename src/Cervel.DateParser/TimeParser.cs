using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class TimeParser
    {
        public ParseResult<IGenerator<DateTime>> ParseDateTimes(string input, int parserVersion = 1)
        {
            if (parserVersion == 1)
                return ParseSymbol(input, true, (p) => p.dateTimes(), (l) => l.DateTimeGenerator);
            else if (parserVersion == 2)
                return ParseSymbolV2(input, true, (p) => p.dateDist(), (l) => l.DateDistribution);
            else
                throw new NotImplementedException($"Unsupported parser version [{parserVersion}].");
        }

        public ParseResult<IGenerator<TimeInterval>> ParseTimeIntervals(string input, int parserVersion = 1)
        {
            if (parserVersion == 1)
                return ParseSymbol(input, false, (p) => p.timeIntervals(), (l) => l.TimeIntervalGenerator);
            else if (parserVersion == 2)
                return ParseSymbolV2(input, false, (p) => p.intvDist(), (l) => l.IntervalDistribution);
            else
                throw new NotImplementedException($"Unsupported parser version [{parserVersion}].");
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

        private ParseResult<TResult> ParseSymbol<TResult>(
            string input,
            bool parseDateTime,
            Func<TimeExpressionParser, ParserRuleContext> contextSelector,
            Func<TimeExpressionListener, TResult> resultSelector)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new TimeExpressionLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new TimeExpressionParser(commonTokenStream);

            var listener = new TimeExpressionListener(parseDateTime);
            var walker = new ParseTreeWalker();
            var context = contextSelector(parser);
            //var context = parser.timeSpans();
            walker.Walk(listener, context);

            if (context.exception != null)
            {
                return new ParseResult<TResult>(input, false);
            }
            else
            {
                //string tree = context.ToStringTree();
                var result = resultSelector(listener);
                return new ParseResult<TResult>(input, true, result);
            }
        }

        private string PreprocessInput(string input)
        {
            return input.Trim()
                .Replace('-', ' ');
        }
    }
}
