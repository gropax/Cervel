﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class SinceGenerator : DateTimeGenerator
    {
        private IGenerator<DateTime> _scope;
        private IGenerator<DateTime> _generator;

        public SinceGenerator(IGenerator<DateTime> scope, IGenerator<DateTime> generator)
        {
            _scope = scope;
            _generator = generator;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                yield break;

            foreach (var date in _generator.Generate(scopeEnum.Current))
                yield return date;
        }
    }
}
