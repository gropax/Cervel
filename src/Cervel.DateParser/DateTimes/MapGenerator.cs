﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class MapGenerator : DateTimeGenerator
    {
        private readonly IGenerator<DateTime> _generator;
        private readonly Func<IEnumerable<DateTime>, IEnumerable<DateTime>> _modifier;

        public MapGenerator(
            IGenerator<DateTime> generator,
            Func<IEnumerable<DateTime>, IEnumerable<DateTime>> modifier,
            string name = null)
            : base(name ?? $"Map<{generator.Name}>")
        {
            _generator = generator;
            _modifier = modifier;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            return _modifier(_generator.Generate(fromDate));
        }
    }
}