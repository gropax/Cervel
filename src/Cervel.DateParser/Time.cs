using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;
using Cervel.TimeParser.Generic;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class Time
    {
        public static DateTime Min(DateTime dateTime, DateTime other)
        {
            return dateTime <= other ? dateTime : other;
        }

        public static DateTime Max(DateTime dateTime, DateTime other)
        {
            return dateTime >= other ? dateTime : other;
        }

        public static IGenerator<T> Never<T>() where T : ITimeInterval<T> => new NeverGenerator<T>();

        public static IGenerator<Date> Start() => new OnceGenerator();


        private static DateTime? _now;
        public static void SetNow(DateTime date) => _now = date;
        public static Date GetNow() => _now.HasValue ? new Date(_now.Value) : new Date(DateTime.Now);

        public static IGenerator<Date> Now() => new OnceGenerator(GetNow());

        public static IGenerator<DayInterval> Yesterday() => Today().Increment(-1);
        public static IGenerator<DayInterval> Today() => Now().CoveringDays();
        public static IGenerator<DayInterval> Tomorrow() => Today().Increment(1);

        public static IGenerator<Date> EveryDay() => Start().Daily();

        public static IGenerator<Date> Each(DayOfWeek dow) => Start().Next(dow).Weekly();
        public static IGenerator<Date> Each(int dayOfMonth) => new DayOfMonthGenerator(dayOfMonth);

        public static IGenerator<Date> Nth(int n, IGenerator<Date> g) =>
            g.Skip(n - 1).Take(1, name: $"Nth<{n}, {g.Name}>");

        public static IGenerator<Date> NEveryM(int n, int m, IGenerator<Date> g) => g.NEveryM(n, m);

        public static IGenerator<Date> Union(params IGenerator<Date>[] generators) => new UnionGenerator(generators);
        public static IGenerator<T> Since<T>(
            IGenerator<Date> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            => new SinceGenerator<T>(scope, generator);

        public static IGenerator<T> Until<T>(
            IGenerator<Date> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            => new UntilGenerator<T>(scope, generator);

        public static IGenerator<TimeInterval> Complement<T>(
            IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new ComplementGenerator<T>(generator);
        } 

        public static IGenerator<Date> Inside(IGenerator<TimeInterval> scope, IGenerator<Date> generator) => new ScopeGenerator(scope, generator);
        public static IGenerator<Date> Outside<T>(
            IGenerator<T> scope,
            IGenerator<Date> generator)
            where T : ITimeInterval<T>
        {
            return new ScopeGenerator(Complement(scope), generator);
        } 

        public static IGenerator<TimeInterval> DayScopes() => new FrequencyGenerator(new DayMeasure()).ToScopes(TimeSpan.FromDays(1));

        public static Func<IGenerator<Date>, IGenerator<Date>> DayShift(int n) => (g) => g.ShiftDay(n);

        public static IGenerator<Date> Scope(IGenerator<TimeInterval> scopes, IGenerator<Date> dates)
        {
            return new ScopeGenerator(scopes, dates);
        }

        #region Interval related combinators
        public static IGenerator<TimeInterval<double>> Quantize(ITimeMeasure timeMeasure, IGenerator<TimeInterval> g) =>
            new QuantizeGenerator(timeMeasure, g);

        #endregion

        #region Day related methods

        public static IGenerator<Date> Frequency(ITimeMeasure timeMeasure) => new FrequencyGenerator(timeMeasure);

        /// <summary>
        /// Séquence de jour-dates espacés d'un mois
        /// </summary>
        /// <returns></returns>
        public static IGenerator<Date> Daily(int n = 1) => new FrequencyGenerator(new DayMeasure(n), name: n == 1 ? "Daily" : $"Daily({n})");

        public static IGenerator<TimeInterval> ToPartition(IGenerator<Date> g) => new ToPartitionGenerator(g);

        #endregion

        #region Month related methods

        /// <summary>
        /// Séquence de jour-dates espacés d'un mois
        /// </summary>
        /// <returns></returns>
        public static IGenerator<Date> Monthly(int n = 1) => new FrequencyGenerator(new MonthMeasure(n), name: n == 1 ? "Monthly" : $"Monthly({n})");

        /// <summary>
        /// Le mois-date précédent (ou la date en cours)
        /// </summary>
        public static IGenerator<Date> StartOfCurrentMonth() => Start().StartOfMonth().First($"SOMonth");

        /// <summary>
        /// Le mois-date suivant
        /// </summary>
        public static IGenerator<Date> StartOfNextMonth() => Start().StartOfMonth().ShiftMonth(1).First($"NextSOMonth");

        /// <summary>
        /// Premier jour-date de chaque mois (mois-date)
        /// </summary>
        public static IGenerator<Date> StartOfEveryMonth() => StartOfCurrentMonth().Monthly($"StartOfEveryMonth");

        /// <summary>
        /// Premier jour-date du mois spécifié (peut avoir lieu dans le passé)
        /// </summary>
        public static IGenerator<Date> StartOf(Month month) => Start().StartOfMonth().Monthly().Where(month).First($"SO<{month}>");

        /// <summary>
        /// Premier jour-date du prochain <Month> (après la fin du mois en cours)
        /// </summary>
        public static IGenerator<Date> StartOfNext(Month month) => Start().StartOfMonth().ShiftMonth(1).Monthly().Where(month).First($"NextSO<{month}>");

        /// <summary>
        /// </summary>
        public static IGenerator<Date> StartOfEvery(Month month) => StartOf(month).Yearly($"EverySO<{month}>");

        // ThisStartOfMonth() -> premier jour de ce mois-ci
        // FromThisStartOfMonth(int) -> premier jour de ce mois-ci, +int mois

        /// <summary>
        /// Partition de mois-durée (scope)
        /// </summary>
        public static IGenerator<TimeInterval> EveryMonth() => StartOfCurrentMonth().Monthly().DuringMonthes(1);

        /// <summary>
        /// Le mois-durée en cours, du premier jour (possiblement passé) jusqu'au dernier
        /// </summary>
        public static IGenerator<TimeInterval> CurrentMonth() => StartOfCurrentMonth().DuringMonthes(1);

        /// <summary>
        /// </summary>
        public static IGenerator<TimeInterval> NextMonth() => StartOfNextMonth().DuringMonthes(1);

        /// <summary>
        /// Premier jour-date du mois de chaque date
        /// </summary>
        public static IGenerator<Date> StartOfMonth(IGenerator<Date> g) => 
            g.Map(Maps.StartOfMonth(), $"StartOfMonth<{g.Name}>");

        /// <summary>
        /// Décale un séquence en multiples de mois
        ///     Attention ! Du fait de l'irrégularité des mois, un shift de mois peut générer des doublons
        ///     (e.g. le 30 et 31 janviers seront rapportés au 28 février). Il faut donc nettoyer le flux.
        /// </summary>
        public static IGenerator<Date> ShiftMonth(IGenerator<Date> g, int n) =>
            g.Map(Maps.ShiftMonth(n), $"ShiftMonth({n})");
            
        /// <summary>
        /// Allonge une séquence de dates en intervalles d'une durée d'un mois
        /// </summary>
        public static IGenerator<TimeInterval> DuringMonthes(IGenerator<Date> g, int n) =>
            g.ToIntervals(new MonthMeasure(n)).Coalesce();

        /// <summary>
        /// Return all month-intervals containing dates.
        /// </summary>
        public static IGenerator<TimeInterval> AllMonth(IGenerator<Date> g) =>
            g.StartOfMonth().ToIntervals(new MonthMeasure(1));

        //public static IGenerator<Date> EveryMonth() => Start().StartOfMonth().Monthly();
        //public static IGenerator<Date> Each(Month month) => StartOfNext(month).YearlySince();

        #endregion
    }
}
