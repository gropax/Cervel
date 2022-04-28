namespace Cervel.TimeParser
{
    public class ParseResult<T>
    {
        public string Input { get; }
        public bool IsSuccess { get; }
        public T Value { get; }

        public ParseResult(string input, bool isSuccess, T value)
        {
            Input = input;
            IsSuccess = isSuccess;
            Value = value;
        }
    }
}