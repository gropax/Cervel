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
        public ParseResult<IDateTimeGenerator> ParseDateTimes(string input)
        {
            return ParseSymbol(input, true, (p) => p.dateTimes(), (l) => l.DateTimeGenerator);
        }

        public ParseResult<ITimeSpanGenerator> ParseTimeSpans(string input)
        {
            return ParseSymbol(input, false, (p) => p.timeSpans(), (l) => l.TimeSpanGenerator);
        }

        private ParseResult<TResult> ParseSymbol<TResult>(
            string input,
            bool parseDateTime,
            Func<TimeSpanParser, ParserRuleContext> contextSelector,
            Func<TimeSpanListener, TResult> resultSelector)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new TimeSpanLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new TimeSpanParser(commonTokenStream);

            var listener = new TimeSpanListener(parseDateTime);
            var walker = new ParseTreeWalker();
            var context = contextSelector(parser);
            //var context = parser.timeSpans();
            walker.Walk(listener, context);

            if (context.exception != null)
                throw new ParseError();

            //string tree = context.ToStringTree();

            var result = resultSelector(listener);
            bool isSuccess = result != null;

            return new ParseResult<TResult>(input, isSuccess, result);
        }
    }
}
