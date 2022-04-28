namespace Cervel.TimeParser
{
    public class ParseResult
    {
        public string Input { get; }
        public ITimeSpanGenerator TimeSpanGenerator { get; }

        public ParseResult(string input, ITimeSpanGenerator timeSpanGenerator)
        {
            Input = input;
            TimeSpanGenerator = timeSpanGenerator;
        }
    }
}