using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserSpecTests : TestBase
    {
        public static TestCaseData TestCases
        {
            get
            {
                var parser = new YamlParser();
                var testCases = new List<TestCase>();

                var specFiles = Directory.GetFiles(@".\specs", "*.yaml", SearchOption.AllDirectories);
                foreach (var specFile in specFiles)
                {
                    using (StreamReader reader = File.OpenText(specFile))
                        testCases.AddRange(parser.ParseTestCases(reader));
                }

                return new TestCaseData(testCases);
            }
        }

        private TimeParser _timeParser = new TimeParser();
        private DateTime _now = new DateTime(2022, 1, 1, 10, 30, 0);

        public TimeParserSpecTests()
        {
            Time.SetNow(_now);
        }


        [Theory]
        [MemberData(nameof(TestCases))]
        public void Test(TestCase testCase)
        {
            if (testCase.Debug)
            {
            }

            var parseResult = _timeParser.ParseTimeIntervals2(testCase.Input);
            Assert.True(parseResult.IsSuccess);

            var intervals = parseResult.Value
                .Generate(testCase.GenerateFrom, testCase.GenerateTo)
                .ToArray();

            Assert.Equal(testCase.Expected, intervals);
        }
    }
}