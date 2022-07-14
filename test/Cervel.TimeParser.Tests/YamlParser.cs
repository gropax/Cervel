using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Tests
{
    public class YamlParser
    {
        public IEnumerable<TestCase> ParseTestCases(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var result = deserializer.Deserialize(reader);

            foreach (var obj in (List<object>)result)
            {
                var dict = (Dictionary<object, object>)obj;
                var inputs = ((List<object>)dict["input"]).Cast<string>().ToArray();

                var generate = (Dictionary<object, object>)dict["generate"];
                var fromStr = (string)generate["from"];
                var toStr = (string)generate["to"];

                var expectStr = ((List<object>)dict["expect"]).Cast<string>().ToArray();

                var from = ParseTimeInterval(fromStr).Start;
                var to = ParseTimeInterval(toStr).Start;
                var expect = expectStr.Select(s => ParseTimeInterval(s)).ToArray();

                bool debug = false;
                if (dict.ContainsKey("debug"))
                    debug = (string)dict["debug"] == "true";

                foreach (var input in inputs)
                {
                    var testCase = new TestCase(input, from, to, expect, debug);
                    yield return testCase;
                }
            }
        }

        public TimeInterval ParseTimeInterval(string s)
        {
            var parts = s.Split('-');
            if (parts.Length == 1)
                return new Year(int.Parse(parts[0])).ToTimeInterval();
            else
                throw new Exception($"Unsupport interval expression [{s}].");
        }
    }
}
