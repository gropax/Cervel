using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TestCaseData : TheoryData<TestCase>
    {
        public TestCaseData(IEnumerable<TestCase> ruleTests)
        {
            foreach (var ruleTest in ruleTests)
                Add(ruleTest);
        }
    }

    public class TestCase : IEquatable<TestCase>
    {
        public string Input { get; }
        public DateTime GenerateFrom { get; }
        public DateTime GenerateTo { get; }
        public TimeInterval[] Expected { get; }

        public TestCase(
            string input,
            DateTime generateFrom,
            DateTime generateTo,
            TimeInterval[] expected)
        {
            Input = input;
            GenerateFrom = generateFrom;
            GenerateTo = generateTo;
            Expected = expected;
        }

        public bool Equals(TestCase other)
        {
            return Input == other.Input
                && GenerateFrom == other.GenerateFrom
                && GenerateTo == other.GenerateTo
                && Enumerable.SequenceEqual(Expected, other.Expected);
        }

        public override bool Equals(object obj)
        {
            return obj is TestCase other
                && Input == other.Input
                && GenerateFrom == other.GenerateFrom
                && GenerateTo == other.GenerateTo
                && Enumerable.SequenceEqual(Expected, other.Expected);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
