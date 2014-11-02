using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Unity;
using ModelSoft.Framework;
using ModelSoft.Framework.Logging;
using ModelSoft.Framework.NLogAddapter;

namespace TestConsoleApp
{
    class Program
    {
        private const string FirstTestModelUrl = @"http://www.imodelsoft.com/models/tests/FirstModel.model";

        static void Main(string[] args)
        {
            LogManager.SetService(new NLogChannelProvider());

            //TestLogAttributes();
            //TestModelElementBase();
            WriteCommonDataLists();

            //Console.WriteLine(new string('-', 80));
            //var memLog = (MemoryTarget)NLog.LogManager.Configuration.FindTargetByName("memory");
            //foreach (var log in memLog.Logs)
            //{
            //    Console.WriteLine(log);
            //}

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to continue ...");
                Console.ReadKey();
            }

        }

        private static void WriteCommonDataLists()
        {
            var encoding = Encoding.Unicode;
            var culture = CultureInfo.InvariantCulture;

            #region Cultures
            LogManager.Default.Default.Log("Writing Cultures");
            CultureInfo.GetCultures(CultureTypes.AllCultures)
                .SaveAsCSV("AllCultures.csv", encoding, '|', addQuotesAnyway: true);

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(c => new
                {
                    c.Name,
                    Parent = c.Parent.Name,
                    c.LCID,
                    c.KeyboardLayoutId,
                    c.IetfLanguageTag,
                    ISO639_1 = c.TwoLetterISOLanguageName,
                    ISO639_2 = c.ThreeLetterISOLanguageName,
                    Win3 = c.ThreeLetterWindowsLanguageName,
                    //Display names
                    c.DisplayName,
                    c.NativeName,
                    c.EnglishName,
                    // Culture type
                    IsInstalled = ((c.CultureTypes & CultureTypes.InstalledWin32Cultures) == CultureTypes.InstalledWin32Cultures) ? "Yes" : "",
                    IsNeutral = ((c.CultureTypes & CultureTypes.NeutralCultures) == CultureTypes.NeutralCultures) ? "Yes" : "",
                    IsSpecific = ((c.CultureTypes & CultureTypes.SpecificCultures) == CultureTypes.SpecificCultures) ? "Yes" : "",
                    IsUser = ((c.CultureTypes & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture) ? "Yes" : "",
                    IsReplacement = ((c.CultureTypes & CultureTypes.ReplacementCultures) == CultureTypes.ReplacementCultures) ? "Yes" : "",
                    // Text
                    IsRightToLeft = c.TextInfo.IsRightToLeft ? "Yes" : "",
                    c.TextInfo.ListSeparator,
                    c.TextInfo.ANSICodePage,
                    c.TextInfo.EBCDICCodePage,
                    c.TextInfo.OEMCodePage,
                    c.TextInfo.MacCodePage,
                    // Number
                    NativeDigits = c.NumberFormat.NativeDigits.ToStringList(","),
                    c.NumberFormat.NumberDecimalDigits,
                    c.NumberFormat.NumberDecimalSeparator,
                    c.NumberFormat.NumberGroupSeparator,
                    NumberGroupSizes = c.NumberFormat.NumberGroupSizes.ToStringList(","),
                    c.NumberFormat.NumberNegativePattern,
                    // Number - Currency
                    c.NumberFormat.CurrencyDecimalDigits, 
                    c.NumberFormat.CurrencyDecimalSeparator, 
                    c.NumberFormat.CurrencyGroupSeparator, 
                    CurrencyGroupSizes = c.NumberFormat.CurrencyGroupSizes.ToStringList(","), 
                    c.NumberFormat.CurrencyNegativePattern, 
                    c.NumberFormat.CurrencyPositivePattern, 
                    c.NumberFormat.CurrencySymbol,
                    // Number - Percent
                    c.NumberFormat.PercentDecimalDigits,
                    c.NumberFormat.PercentDecimalSeparator,
                    c.NumberFormat.PercentGroupSeparator,
                    PercentGroupSizes = c.NumberFormat.PercentGroupSizes.ToStringList(","),
                    c.NumberFormat.PercentNegativePattern,
                    c.NumberFormat.PercentPositivePattern,
                    c.NumberFormat.PercentSymbol,
                    // Number - Other
                    c.NumberFormat.PositiveSign,
                    c.NumberFormat.NegativeSign,
                    c.NumberFormat.PositiveInfinitySymbol,
                    c.NumberFormat.NegativeInfinitySymbol,
                    c.NumberFormat.NaNSymbol,
                    c.NumberFormat.PerMilleSymbol,
                    c.NumberFormat.DigitSubstitution,
                    // DateTime
                    ShortestDayNames = c.DateTimeFormat.ShortestDayNames.ToStringList(","),
                    AbbreviatedDayNames = c.DateTimeFormat.AbbreviatedDayNames.ToStringList(","),
                    DayNames = c.DateTimeFormat.DayNames.ToStringList(","),
                    AbbreviatedMonthGenitiveNames = c.DateTimeFormat.AbbreviatedMonthGenitiveNames.ToStringList(","),
                    AbbreviatedMonthNames = c.DateTimeFormat.AbbreviatedMonthNames.ToStringList(","),
                    MonthGenitiveNames = c.DateTimeFormat.MonthGenitiveNames.ToStringList(","),
                    MonthNames = c.DateTimeFormat.MonthNames.ToStringList(","),
                    c.DateTimeFormat.AMDesignator,
                    c.DateTimeFormat.PMDesignator,
                    c.DateTimeFormat.DateSeparator,
                    c.DateTimeFormat.TimeSeparator,
                    c.DateTimeFormat.FirstDayOfWeek,
                    c.DateTimeFormat.FullDateTimePattern,
                    c.DateTimeFormat.SortableDateTimePattern,
                    c.DateTimeFormat.UniversalSortableDateTimePattern,
                    c.DateTimeFormat.RFC1123Pattern,
                    c.DateTimeFormat.LongDatePattern,
                    c.DateTimeFormat.ShortDatePattern,
                    c.DateTimeFormat.LongTimePattern,
                    c.DateTimeFormat.ShortTimePattern,
                    c.DateTimeFormat.MonthDayPattern,
                    c.DateTimeFormat.YearMonthPattern,

                    // Calendar
                    Calendar = c.Calendar.GetType().Name,
                    c.DateTimeFormat.CalendarWeekRule,
                    c.DateTimeFormat.NativeCalendarName,
                    OptionalCalendars = c.OptionalCalendars.ToStringList(",", e => e.GetType().Name),
                }).ToArray();
            cultures.SaveAsCSV("Cultures.csv", encoding, '|', addQuotesAnyway: true);
            #endregion Cultures

            #region TimeZones
            LogManager.Default.Default.Log("Writing TimeZones");
            TimeZoneInfo.GetSystemTimeZones().SaveAsCSV("SystemTimeZones.csv", encoding);
            #endregion

            #region Encodings
            LogManager.Default.Default.Log("Writing Encodings");
            var encodingInfos = Encoding.GetEncodings();
            encodingInfos.SaveAsCSV("EncodingInfos.csv", encoding);
            encodingInfos.Select(e => e.GetEncoding()).SaveAsCSV("Encodings.csv", encoding);
            #endregion

            #region Calendars
            LogManager.Default.Default.Log("Writing Calendars");
            var calendars = new Calendar[]
            {
                new GregorianCalendar(GregorianCalendarTypes.Localized), 
                new GregorianCalendar(GregorianCalendarTypes.Arabic), 
                new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench), 
                new GregorianCalendar(GregorianCalendarTypes.TransliteratedEnglish), 
                new GregorianCalendar(GregorianCalendarTypes.TransliteratedFrench), 
                new GregorianCalendar(GregorianCalendarTypes.USEnglish), 
                new HebrewCalendar(), 
                new HijriCalendar(), 
                new UmAlQuraCalendar(), 
                new ChineseLunisolarCalendar(), 
                new JapaneseLunisolarCalendar(), 
                new KoreanLunisolarCalendar(), 
                new TaiwanLunisolarCalendar(), 
                new JulianCalendar(), 
                new PersianCalendar(), 
                new JapaneseCalendar(), 
                new KoreanCalendar(), 
                new TaiwanCalendar(), 
                new ThaiBuddhistCalendar(), 
            };
            var calendars2 = calendars.Select(c => new
            {
                Type = c.GetType().Name,
                Subtype = c is GregorianCalendar ? ((GregorianCalendar)c).CalendarType.ToString() : "",
                c.AlgorithmType,
                Eras = c.Eras.ToStringList(","),
                c.MinSupportedDateTime,
                c.MaxSupportedDateTime,
                c.TwoDigitYearMax,
            }).ToArray();
            calendars2.SaveAsCSV("Calendars.csv", encoding);
            #endregion


            #region Unicode characters
            LogManager.Default.Default.Log("Writing Unicode characters");
            var characters = Enumerable.Range(0, char.MaxValue + 1).Select(c => (char)c)
                .Select(c => new
            {
                Code     = (int)c,
                Char     = c,
                Category = CharUnicodeInfo.GetUnicodeCategory(c),
                Decimals = CharUnicodeInfo.GetDecimalDigitValue(c),
                Digit    = CharUnicodeInfo.GetDigitValue(c),
                Number   = CharUnicodeInfo.GetNumericValue(c),
            }).Select(c  => new
            {
                Code     = c.Code,
                Char     = c.Category != UnicodeCategory.Control ? c.Char.ToString(culture) : "",
                Category = c.Category,
                Decimals = c.Decimals == -1 ? "" : c.Decimals.ToString(culture),
                Digit = c.Digit == -1 ? "" : c.Digit.ToString(culture),
                Number = c.Number == -1.0 ? "" : c.Number.ToString(culture),
            }).ToArray();
            characters.SaveAsCSV("Characters.csv", encoding);
            #endregion


            Process.Start(".");
        }

        private static void TestModelElementBase()
        {

        }

        private static void TestLogAttributes()
        {
            var result = TestLog("{0}={1}+{2}", 1234, 7, 3, 4);
            Console.WriteLine("Result: {0}", result);
        }

        [Log(ChannelNameType.Type, ShowFullTypeName = true, ShowParametersName = true, ShowParametersType = true)]
        private static string TestLog(string format, int integer, params object[] array)
        {
            var str = string.Format(format, array);
            str = "[" + integer + "]" + str;
            return str;
        }

    }
}
