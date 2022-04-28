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
        public ParseResult Parse(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new TimeSpanLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new TimeSpanParser(commonTokenStream);

            var listener = new TimeSpanListener();
            var walker = new ParseTreeWalker();
            var context = parser.timeSet();
            walker.Walk(listener, context);

            if (context.exception != null)
                throw new ParseError();

            //string tree = context.ToStringTree();

            return new ParseResult(input, listener.TimeSpanGenerator);
        }
    }
}
