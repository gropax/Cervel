﻿using Antlr4.Runtime;
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

        public ParseResult<ITimeIntervalGenerator> ParseTimeIntervals(string input)
        {
            return ParseSymbol(input, false, (p) => p.timeIntervals(), (l) => l.TimeIntervalGenerator);
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
                throw new ParseError();

            //string tree = context.ToStringTree();

            var result = resultSelector(listener);
            bool isSuccess = result != null;

            return new ParseResult<TResult>(input, isSuccess, result);
        }
    }
}