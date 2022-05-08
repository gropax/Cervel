using Cervel.TimeParser.DateTimes;
using Cervel.TimeParser.Extensions;
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

        public static IGenerator<DateTime> Start() => new OnceGenerator();


        private static DateTime? _now;
        public static void SetNow(DateTime date) => _now = date;
        public static DateTime GetNow() => _now ?? DateTime.Now;

        public static IGenerator<DateTime> Now() => new OnceGenerator(GetNow());

        public static IGenerator<DateTime> Yesterday() => Today().ShiftDay(-1);
        public static IGenerator<DateTime> Today() => Now().StartOfDay();
        public static IGenerator<DateTime> Tomorrow() => Today().ShiftDay(1);

        public static IGenerator<DateTime> EveryDay() => Start().Daily();

        public static IGenerator<DateTime> Next(DayOfWeek dow) => Tomorrow().Next(dow);
        public static IGenerator<DateTime> Each(DayOfWeek dow) => Start().Next(dow).Weekly();
        public static IGenerator<DateTime> Union(params IGenerator<DateTime>[] generators) => new UnionGenerator(generators);
        public static IGenerator<DateTime> Since(IGenerator<DateTime> scope, IGenerator<DateTime> generator) => new SinceGenerator(scope, generator);
        public static IGenerator<DateTime> Until(IGenerator<DateTime> scope, IGenerator<DateTime> generator) => new UntilGenerator(scope, generator);

        public static IGenerator<TimeInterval> DayScopes() => new FrequencyGenerator(new DayMeasure()).ToScopes(TimeSpan.FromDays(1));

        public static IGenerator<TimeInterval> EveryDayOfWeekInterval(DayOfWeek dow)
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }

        public static Func<IGenerator<DateTime>, IGenerator<DateTime>> DayShift(int n) => (g) => g.ShiftDay(n);



        #region Month related methods

        /// <summary>
        /// Séquence de jour-dates espacés d'un mois
        /// </summary>
        /// <returns></returns>
        public static IGenerator<DateTime> Monthly(int n = 1) => new FrequencyGenerator(new MonthMeasure(n), name: n == 1 ? "Monthly" : $"Monthly({n})");

        /// <summary>
        /// Séquence de jour-dates espacés d'un mois
        /// </summary>
        //public static IGenerator<DateTime> MonthesLater(int n) => Monthly(n).Skip(1, name: $"MonthesLater({n})");

        // NextStartOfMonth() -> le mois-date suivant
        public static IGenerator<DateTime> PrevStartOfMonth() => Start().StartOfMonth().First($"PrevSOMonth");
        public static IGenerator<DateTime> NextStartOfMonth() => Start().StartOfMonth().ShiftMonth(1).First($"NextSOMonth");

        // EveryStartOfMonth() -> premier jour-date de chaque mois (mois-date)
        // NextStartOf(Month) -> premier jour-date de chaque mois de <Month>
        public static IGenerator<DateTime> Next(Month month) => Start().StartOfMonth().Monthly().Where(month).First($"Next<{month}>");
        // EveryStartOf(Month) -> premier jour-date de chaque mois de <Month>

        // ThisStartOfMonth() -> premier jour de ce mois-ci
        // FromThisStartOfMonth(int) -> premier jour de ce mois-ci, +int mois

        // EveryMonth() -> partition de mois-durée (scope)
        // ThisMonth() -> le mois-durée en cours, du premier jour (possiblement passé) jusqu'au dernier
        // NextMonth() -> le mois-durée suivant

        /// <summary>
        /// Premier jour-date du mois de chaque date
        /// </summary>
        public static IGenerator<DateTime> StartOfMonth(IGenerator<DateTime> g) => 
            g.Map(Maps.StartOfMonth(), $"StartOfMonth<{g.Name}>");

        /// <summary>
        /// Décale un séquence en multiples de mois
        ///     Attention ! Du fait de l'irrégularité des mois, un shift de mois peut générer des doublons
        ///     (e.g. le 30 et 31 janviers seront rapportés au 28 février). Il faut donc nettoyer le flux.
        /// </summary>
        public static IGenerator<DateTime> ShiftMonth(IGenerator<DateTime> g, int n) =>
            g.Map(Maps.ShiftMonth(n), $"ShiftMonth({n})");
            
        /// <summary>
        /// Allonge une séquence de dates en intervalles d'une durée d'un mois
        /// </summary>
        public static IGenerator<TimeInterval> DuringMonthes(IGenerator<DateTime> g, int n) =>
            g.ToIntervals(new MonthMeasure(n));

        /// <summary>
        /// Return all month-intervals containing dates.
        /// </summary>
        public static IGenerator<TimeInterval> AllMonth(IGenerator<DateTime> g) =>
            g.StartOfMonth().ToIntervals(new MonthMeasure(1));

        public static IGenerator<DateTime> EveryMonth() => Start().StartOfMonth().Monthly();
        public static IGenerator<DateTime> Each(Month month) => Next(month).YearlySince();

        #endregion
    }
}
