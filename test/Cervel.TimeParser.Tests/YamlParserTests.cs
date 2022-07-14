using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class YamlParserTests : TestBase
    {
        private YamlParser _parser;
        public YamlParserTests()
        {
            _parser = new YamlParser();
        }

        [Fact]
        public void TestParse()
        {
            string specs = @"
-
    input:
        - chaque année
        - tous les ans
    generate:
        from: 2022
        to: 2027
    expect:
        - 2022
        - 2023
        - 2024
        - 2025
        - 2026
";

            using var reader = new StringReader(specs);
            var testCases = _parser.ParseTestCases(reader).ToList();

            var generateFrom = new DateTime(2022, 01, 01);
            var generateTo = new DateTime(2027, 01, 01);
            var expectedIntervals = new[]
            {
                new Year(2022),
                new Year(2023),
                new Year(2024),
                new Year(2025),
                new Year(2026),
            }
            .Select(i => i.ToTimeInterval()).ToArray();

            var expected = new List<TestCase>()
            {
                new TestCase("chaque année", generateFrom, generateTo, expectedIntervals, false),
                new TestCase("tous les ans", generateFrom, generateTo, expectedIntervals, false),
            };

            Assert.Equal(expected, testCases);
        }
    }
}