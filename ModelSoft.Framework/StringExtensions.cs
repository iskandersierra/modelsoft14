using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
    public enum TextJustification { Left, Center, Right, Justify }

    public static class StringExtensions
    {
        #region [ IsEmpty, IsWS, Fmt, EncodeString ]
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        public static bool IsWS(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static bool IsNotWS(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        public static string IfWS(this string str, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue : str;
        }
        public static string IfWS(this string str, Func<string> defaultValue)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue() : str;
        }
        public static string IfEmpty(this string str, string defaultValue)
        {
            return string.IsNullOrEmpty(str) ? defaultValue : str;
        }
        public static string IfEmpty(this string str, Func<string> defaultValue)
        {
            return string.IsNullOrEmpty(str) ? defaultValue() : str;
        }
        public static string Fmt(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
        public static string EncodeString(this string str)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (ch >= 0x20 && ch <= 0x7F) sb.Append(ch);
                else switch (ch)
                    {
                        case '\r': sb.Append("\\r"); break;
                        case '\n': sb.Append("\\n"); break;
                        case '\t': sb.Append("\\t"); break;
                        case '\f': sb.Append("\\f"); break;
                        case '\a': sb.Append("\\a"); break;
                        case '\b': sb.Append("\\b"); break;
                        default: sb.Append("\\u").Append(((int)ch).ToString("X4")); break;
                    }
            }
            return sb.ToString();
        }
        #endregion

        #region [ ToSpacedWords ]
        internal static readonly Regex SpacedWordsRegex = new Regex("[A-Z]+[a-z0-9]*", RegexOptions.Compiled);
        public static string ToSpacedWords(this string value)
        {
            var matches = SpacedWordsRegex.Matches(value).Cast<Match>();
            var builder = new StringBuilder();
            foreach (var match in matches)
            {
                if (builder.Length > 0) builder.Append(" ");
                builder.Append(match.Value);
            }
            return builder.ToString();
        }
        #endregion

        #region [ AsVerbatim ]
        public static string AsVerbatim(this string str)
        {
            if (str == null) return null;
            return str.Replace(@"""", @"""""");
        }
        #endregion

        #region [ Substring After/Before ]
        // Falta agregar StartIndex y Count
        public static string SubstringBeforeOr(this string mainString, string substring, string defaultValue)
        {
            int pos = mainString.IndexOf(substring);
            if (pos < 0) return defaultValue;
            return mainString.Substring(0, pos);
        }
        public static string SubstringBefore(this string mainString, string substring)
        {
            return mainString.SubstringBeforeOr(substring, null);
        }
        public static string SubstringBeforeOrSame(this string mainString, string substring)
        {
            return mainString.SubstringBeforeOr(substring, mainString);
        }
        public static string SubstringAfterOr(this string mainString, string substring, string defaultValue)
        {
            int pos = mainString.IndexOf(substring);
            if (pos < 0) return defaultValue;
            return mainString.Substring(pos + substring.Length);
        }
        public static string SubstringAfter(this string mainString, string substring)
        {
            return mainString.SubstringAfterOr(substring, null);
        }
        public static string SubstringAfterOrSame(this string mainString, string substring)
        {
            return mainString.SubstringAfterOr(substring, mainString);
        }
        public static string SubstringBeforeLastOr(this string mainString, string substring, string defaultValue)
        {
            int pos = mainString.LastIndexOf(substring);
            if (pos < 0) return defaultValue;
            return mainString.Substring(0, pos);
        }
        public static string SubstringBeforeLast(this string mainString, string substring)
        {
            return mainString.SubstringBeforeLastOr(substring, null);
        }
        public static string SubstringBeforeLastOrSame(this string mainString, string substring)
        {
            return mainString.SubstringBeforeLastOr(substring, mainString);
        }
        public static string SubstringAfterLastOr(this string mainString, string substring, string defaultValue)
        {
            int pos = mainString.LastIndexOf(substring);
            if (pos < 0) return defaultValue;
            return mainString.Substring(pos + substring.Length);
        }
        public static string SubstringAfterLast(this string mainString, string substring)
        {
            return mainString.SubstringAfterLastOr(substring, null);
        }
        public static string SubstringAfterLastOrSame(this string mainString, string substring)
        {
            return mainString.SubstringAfterLastOr(substring, mainString);
        }
        #endregion

        #region [ CutWithPeriods ]

        /// <summary>
        /// Returns a string no longer than the given maximum length. The period string is used at the 
        /// end of the string if the original text is cut to ensure that.
        /// </summary>
        /// <param name="text">The string to cut if it is longer than maxLength</param>
        /// <param name="maxLength">The maximum length of the resulting string</param>
        /// <param name="periods"></param>
        /// <returns></returns>
        public static string CutWithPeriods(this string text, int maxLength, string periods = "...")
        {
            //Contract.Requires(maxLength >= 0, "maxLength >= 0");
            //Contract.Requires(periods != null, "periods != null");

            //Contract.Ensures((text == null) == (Contract.Result<string>() == null), "text == null <==> Result == null");
            //Contract.Ensures(Contract.Result<string>() == null || Contract.Result<string>().Length <= maxLength, "Result != null ==> Result.Length <= maxLength");

            return CutWithPeriods(text, maxLength, 0, periods);
            //if (text == null) return null;
            //if (text.Length <= maxLength) return text;
            //if (periods.Length > maxLength) return periods.Substring(0, maxLength);
            //return text.Substring(0, maxLength - periods.Length) + periods;
        }
        /// <summary>
        /// Returns a string no longer than the given maximum length. The period string is used at the 
        /// end of the string if the original text is cut to ensure that.
        /// </summary>
        /// <param name="text">The string to cut if it is longer than maxLength</param>
        /// <param name="maxLength">The maximum length of the resulting string</param>
        /// <param name="backLength"></param>
        /// <param name="periods"></param>
        /// <returns></returns>
        public static string CutWithPeriods(this string text, int maxLength, int backLength, string periods = "...")
        {
            //Contract.Requires(maxLength >= 0, "maxLength >= 0");
            //Contract.Requires(periods != null, "periods != null");

            //Contract.Ensures((text == null) == (Contract.Result<string>() == null), "text == null <==> Result == null");
            //Contract.Ensures(Contract.Result<string>() == null || Contract.Result<string>().Length <= maxLength, "Result != null ==> Result.Length <= maxLength");

            if (text == null) return null;
            if (text.Length <= maxLength) return text;
            if (periods.Length > maxLength) return periods.Substring(0, maxLength);
            if (backLength + periods.Length > maxLength) backLength = maxLength - periods.Length;   
            return text.Substring(0, maxLength - periods.Length - backLength) + 
                periods +
                text.Substring(maxLength - backLength, backLength);
        }
        #endregion

        #region [ Append ]
        public static string AppendIfNotEmpty(this string str, string toAppend)
        {
            return str.IsEmpty() ? str : str + toAppend;
        }
        public static string AppendComma(this string str)
        {
            return str.AppendIfNotEmpty(", ");
        }
        public static StringBuilder AppendIfNotEmpty(this StringBuilder str, string toAppend)
        {
            return str.Length == 0 ? str : str.Append(toAppend);
        }
        public static StringBuilder AppendComma(this StringBuilder str)
        {
            return str.AppendIfNotEmpty(", ");
        }
        #endregion

        #region [ ToCammel/Pascal/Property/VariableCase ]
        public static string ToCammelCase(this string str)
        {
            if (str == null) return null;
            if (str.Length == 0) return str;
            if (char.IsLower(str[0])) return str;
            return char.ToLower(str[0]) + str.Substring(1);
        }
        public static string ToPascalCase(this string str)
        {
            if (str == null) return null;
            if (str.Length == 0) return str;
            if (char.IsUpper(str[0])) return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }
        public static string ToPropertyCase(this string str)
        {
            return str.ToPascalCase();
        }
        public static string ToVariableCase(this string str)
        {
            var cammel = str.ToCammelCase();
            var pascal = str.ToPascalCase();
            if (cammel != pascal) return cammel;
            return "_" + cammel;
        }
        #endregion

        #region [ JustifyText ]
        public static string JustifyText(this string text,
          int width,
          bool breakLines = false,
          TextJustification justification = TextJustification.Justify,
          char fillChar = ' ')
        {
            if (width < text.Length)
                if (!breakLines)
                    return text;
                else
                {
                    /// TODO: Implementar cuando hay cambio de lineas
                    return text;
                }
            else
            {
                justification.RequireIsValidEnum("justification");

                if (width == text.Length) return text;
                switch (justification)
                {
                    case TextJustification.Left:
                        return text.PadRight(width, fillChar);
                    case TextJustification.Center:
                        return text.PadLeft((text.Length + width) / 2, fillChar).PadRight(width, fillChar);
                    case TextJustification.Right:
                        return text.PadLeft(width, fillChar);
                    case TextJustification.Justify:
                        /// TODO: Implementar la justificación del texto en una sola linea
                        return text;
                }
                throw new NotSupportedException();
            }
        }
        #endregion

        #region [ ToMatrixString ]
        private enum Sep { H = 0, V = 1, TL = 2, TR = 3, BL = 4, BR = 5, L = 6, R = 7, T = 8, B = 9, C = 10 };
        public const string SimpleMatrixSeparators = "─│┌┐└┘├┤┬┴┼";
        public const string DoubleMatrixSeparators = "═║╔╗╚╝╠╣╦╩╬";

        public static string ToMatrixString<T>(this T[,] array,
          Func<T, string> toString = null,
          string[] columnHeaders = null,
          string[] rowHeaders = null,
          string horizontalSpan = " ",
          int verticalSpan = 0,
          string separators = SimpleMatrixSeparators,
          bool rowsAreRankZero = true,
          Func<string, int, string> justifyFunc = null,
          TextJustification justification = TextJustification.Left,
          bool sameColumnWidth = false,
          string caption = "")
        {
            if (toString == null) toString = t => "" + t;
            if (justifyFunc == null) justifyFunc = (s, w) => s.JustifyText(w, breakLines: false, justification: justification, fillChar: ' ');
            int rowRank = rowsAreRankZero ? 0 : 1;
            int rowCount = array.GetLength(rowRank);
            int colCount = array.GetLength(1 - rowRank);
            int extraRow = columnHeaders != null ? 1 : 0;
            int extraCol = rowHeaders != null ? 1 : 0;

            string[,] text = new string[rowCount + extraRow, colCount + extraCol];
            text[0, 0] = caption ?? String.Empty; // only showed when column and row headers are present
            if (columnHeaders != null)
                for (int i = 0; i < colCount; i++)
                    text[0, i + extraCol] = i < columnHeaders.Length ? columnHeaders[i] : String.Empty;
            for (int j = 0; j < rowCount; j++)
            {
                if (rowHeaders != null)
                    text[j + extraRow, 0] = j < rowHeaders.Length ? rowHeaders[j] : String.Empty;
                for (int i = 0; i < colCount; i++)
                    text[j + extraRow, i + extraCol] = toString(rowRank == 0 ? array[j, i] : array[i, j]);
            }

            int[] colWidths = 0.ToCount(colCount + extraCol).Select(i => 0.ToCount(rowCount + extraRow).Select(j => text[j, i].Length).Max<int>()).ToArray();
            if (sameColumnWidth)
            {
                int maxColWidth = colWidths.Max<int>();
                colWidths = 0.ToCount(colCount + extraCol).Select(_ => maxColWidth).ToArray();
            }

            int hSpanWidth = horizontalSpan.Length;
            for (int j = 0; j < rowCount + extraRow; j++)
                for (int i = 0; i < colCount + extraCol; i++)
                    text[j, i] = justifyFunc(horizontalSpan + text[j, i] + horizontalSpan, colWidths[i] + 2 * hSpanWidth);

            StringBuilder sb = new StringBuilder();

            string[] horLines = 0.ToCount(colCount + extraCol).Select(i => new string(separators[(int)Sep.H], colWidths[i] + 2 * hSpanWidth)).ToArray();
            string[] horSpaces = verticalSpan > 0 ? 0.ToCount(colCount + extraCol).Select(i => new string(' ', colWidths[i] + 2 * hSpanWidth)).ToArray() : null;
            for (int j = 0; j < rowCount + extraRow; j++)
            {
                #region paint horizontal lines and crosses
                for (int i = 0; i < colCount + extraCol; i++)
                {
                    if (j == 0)
                        if (i == 0) sb.Append(separators[(int)Sep.TL]);
                        else sb.Append(separators[(int)Sep.T]);
                    else
                        if (i == 0) sb.Append(separators[(int)Sep.L]);
                        else sb.Append(separators[(int)Sep.C]);
                    sb.Append(horLines[i]);
                }
                if (j == 0) sb.Append(separators[(int)Sep.TR]);
                else sb.Append(separators[(int)Sep.R]);
                sb.AppendLine();

                #endregion
                #region paint vertical span
                for (int k = 0; k < verticalSpan; k++)
                {
                    for (int i = 0; i < colCount + extraCol; i++)
                    {
                        sb.Append(separators[(int)Sep.V]);
                        sb.Append(horSpaces[i]);
                    }
                    sb.Append(separators[(int)Sep.V]);
                    sb.AppendLine();
                }

                #endregion
                #region paint content
                for (int i = 0; i < colCount + extraCol; i++)
                {
                    sb.Append(separators[(int)Sep.V]);
                    sb.Append(text[j, i]);
                }
                sb.Append(separators[(int)Sep.V]);
                sb.AppendLine();

                #endregion
                #region paint vertical span
                for (int k = 0; k < verticalSpan; k++)
                {
                    for (int i = 0; i < colCount + extraCol; i++)
                    {
                        sb.Append(separators[(int)Sep.V]);
                        sb.Append(horSpaces[i]);
                    }
                    sb.Append(separators[(int)Sep.V]);
                    sb.AppendLine();
                }
                #endregion
            }
            #region paint horizontal lines and crosses
            for (int i = 0; i < colCount + extraCol; i++)
            {
                if (i == 0) sb.Append(separators[(int)Sep.BL]);
                else sb.Append(separators[(int)Sep.B]);
                sb.Append(horLines[i]);
            }
            sb.Append(separators[(int)Sep.BR]);
            sb.AppendLine();

            #endregion

            return sb.ToString();
        }
        #endregion

        #region [ FormatNamed ]
        private static Regex FormatNamedRegex =
          new Regex(
            @"\G(?'Text'([^\{]|(\{\{))+)|(?:\{)(?'NameRef'([\p{L}][\w]*))(?:\})|(?:\{)(?'NumRef'([\d]{1,9}))(?:\})|(?'Text'{)",
            RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
        public static string FormatNamed(this string formatString, IDictionary namedSource, params object[] args)
        {
            return formatString.FormatNamed(namedSource, (IEnumerable)args);
        }
        public static string FormatNamed(this string formatString, IDictionary namedSource, IEnumerable indexedSource)
        {
            var arr = indexedSource == null ? new object[0] : indexedSource.Cast<object>().ToArray();
            return formatString.FormatNamed(s => namedSource[s], i => arr[i]);
        }
        public static string FormatNamed(this string formatString, Func<string, object> namedSource, Func<int, object> indexedSource)
        {
            if (namedSource == null)
                namedSource = s => { throw new IndexOutOfRangeException("Unknown named parameter"); };
            if (indexedSource == null)
                indexedSource = s => { throw new IndexOutOfRangeException("Index out of bounds"); };
            var sb = new StringBuilder();
            var matches = FormatNamedRegex.Matches(formatString);
            foreach (var match in matches.Cast<Match>())
            {
                var text = match.Groups["Text"].Success ? match.Groups["Text"].Value : null;
                var nameRef = match.Groups["NameRef"].Success ? match.Groups["NameRef"].Value : null;
                var numRef = match.Groups["NumRef"].Success ? match.Groups["NumRef"].Value : null;
                if (text != null)
                    sb.Append(text);
                else
                    if (nameRef != null)
                        sb.Append(namedSource(nameRef));
                    else
                        if (numRef != null)
                            sb.Append(indexedSource(int.Parse(numRef)));
                        else
                            throw new ArgumentException("Illegat format.");
            }
            return sb.ToString();
        }
        #endregion

        #region [ Save/Load CSV ]
        public static readonly Tup<char, char>[] DefaultQuotationChars = new[] { Tup.Create('"', '"'), Tup.Create('\'', '\'') };
        public static readonly Tup<char, char>[] NoQuotationChars = new Tup<char, char>[0];

        public static string ConvertToString(this IEnumerable values, IEnumerable<IItemConverter> converters = null, string separator = ", ", IItemConverter defaultConverter = null)
        {
            Lazy<IItemConverter> lazyDefConv = new Lazy<IItemConverter>(() => defaultConverter ?? (new DelegateItemConverter<object, string>(o => "" + o)));
            var tuples = Tup.Enumerate(values.Cast<object>(), converters ?? Enumerable.Empty<IItemConverter>(), StopsWith.First)
              .Select(t => Tup.Create(t.Item1, t.Item2 ?? lazyDefConv.Value));
            return tuples.ToStringList(separator, t => (string)t.Item2.Convert(t.Item1));
        }
        public static IEnumerable ConvertFromString(this string line, IEnumerable<IItemConverter> converters = null, IItemConverter<string, string[]> splitter = null, IItemConverter defaultConverter = null)
        {
            if (splitter == null)
                splitter = StringSpliterConverter.Default;
            Lazy<IItemConverter> lazyDefConv = new Lazy<IItemConverter>(() => defaultConverter ?? (new DelegateItemConverter<string, string>(s => s)));
            var tuples = Tup.Enumerate(splitter.Convert(line), converters ?? Enumerable.Empty<IItemConverter>(), StopsWith.First)
              .Select(t => Tup.Create(t.Item1, t.Item2 ?? lazyDefConv.Value));
            return tuples.Select(t => t.Item2.Convert(t.Item1));
        }

        public static void SaveCSV(string fileName, IEnumerable<string> content, string headers = null)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
                SaveCSV(writer, content, headers);
        }
        public static void SaveCSV(Stream stream, IEnumerable<string> content, string headers = null)
        {
            using (StreamWriter writer = new StreamWriter(stream))
                SaveCSV(writer, content, headers);
        }
        public static void SaveCSV(TextWriter writer, IEnumerable<string> content, string headers = null)
        {
            if (headers != null)
                writer.WriteLine(headers);
            foreach (var line in content)
                writer.WriteLine(line);
        }

        public static SimpleTable LoadCSV(string fileName, IEnumerable<IItemConverter> converters = null, IItemConverter<string, string[]> splitter = null, IItemConverter defaultConverter = null, bool hasHeaders = true)
        {
            using (StreamReader reader = new StreamReader(fileName))
                return LoadCSV(reader, converters, splitter, defaultConverter, hasHeaders);
        }
        public static SimpleTable LoadCSV(Stream stream, IEnumerable<IItemConverter> converters = null, IItemConverter<string, string[]> splitter = null, IItemConverter defaultConverter = null, bool hasHeaders = true)
        {
            using (StreamReader reader = new StreamReader(stream))
                return LoadCSV(reader, converters, splitter, defaultConverter, hasHeaders);
        }
        public static SimpleTable LoadCSV(TextReader reader, IEnumerable<IItemConverter> converters = null, IItemConverter<string, string[]> splitter = null, IItemConverter defaultConverter = null, bool hasHeaders = true)
        {
            SimpleTable result = new SimpleTable();
            if (splitter == null)
                splitter = StringSpliterConverter.Default;

            string line;
            if (hasHeaders)
            {
                line = reader.ReadLine();
                if (line == null) return result;
                result.Headers = splitter.Convert(line);
            }
            while (true)
            {
                line = reader.ReadLine();
                if (line == null) return result;
                if (result.Content == null) result.Content = new List<IEnumerable>();
                result.Content.Add(ConvertFromString(line, converters, splitter, defaultConverter));
            }
        }

        public class SimpleTable
        {
            public string[] Headers;
            public List<IEnumerable> Content;
        }
        #endregion

        #region [ ToPlainText ]
        private static object ToPlainTextLookupLock = new object();
        private static Dictionary<char, string> ToPlainTextLookup;
        /// <summary>
        /// Convert latin letters with tildes and accents to plain letters
        /// </summary>
        /// <param name="text">Receive a text with any character set</param>
        /// <returns>The same input text after substituting accentuated letters and letters with marks with ist plain latin letters</returns>
        public static string ToPlainText(this string text)
        {
            if (text == null) return null;
            if (text.Length == 0) return text;
            EnsureToPlainTextLookup();
            StringBuilder sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                string str;
                if (ToPlainTextLookup.TryGetValue(ch, out str))
                    sb.Append(str);
                else
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        private static void EnsureToPlainTextLookup()
        {
            if (ToPlainTextLookup == null)
                lock (ToPlainTextLookupLock)
                    if (ToPlainTextLookup == null)
                    {
                        var dict = new Dictionary<char, string>();

                        AddCharSet(dict, "àáâãäåæāăą", "a");
                        AddCharSet(dict, "ÀÁÂÃÄÅÆĀĂĄ", "A");
                        AddCharSet(dict, "èéêëēĕėęě", "e");
                        AddCharSet(dict, "ÈÉÊËĒĔĖĘĚƎƏƐ", "E");
                        AddCharSet(dict, "ìíîïĩīĭįı", "i");
                        AddCharSet(dict, "ÌÍÎÏĨĪĬĮİƖƗ", "I");
                        AddCharSet(dict, "ĳ", "ij");
                        AddCharSet(dict, "Ĳ", "IJ");
                        AddCharSet(dict, "ðòóôõöøōŏőơ", "o");
                        AddCharSet(dict, "ÒÓÔÕÖØŌŎŐƟƠ", "O");
                        AddCharSet(dict, "œ", "oe");
                        AddCharSet(dict, "Œ", "OE");
                        AddCharSet(dict, "ƣ", "oi");
                        AddCharSet(dict, "Ƣ", "OI");
                        AddCharSet(dict, "ùúûüũūŭůűųư", "u");
                        AddCharSet(dict, "ÙÚÛÜŨŪŬŮŰŲƯ", "U");

                        AddCharSet(dict, "ƀƃƅ", "b");
                        AddCharSet(dict, "ƁƂƄ", "B");
                        AddCharSet(dict, "çćĉċčƈ", "c");
                        AddCharSet(dict, "ÇĆĈĊČƆƇ", "C");
                        AddCharSet(dict, "ďđƌƍ", "d");
                        AddCharSet(dict, "ÐĎĐƉƊƋ", "D");
                        AddCharSet(dict, "ƒ", "f");
                        AddCharSet(dict, "Ƒ", "F");
                        AddCharSet(dict, "ĝğġģ", "g");
                        AddCharSet(dict, "ĜĞĠĢƓƔ", "G");
                        AddCharSet(dict, "ĥħ", "h");
                        AddCharSet(dict, "ƕ", "hv");
                        AddCharSet(dict, "ĤĦ", "H");
                        AddCharSet(dict, "ĵ", "j");
                        AddCharSet(dict, "Ĵ", "J");
                        AddCharSet(dict, "ķĸƙ", "k");
                        AddCharSet(dict, "ĶƘ", "K");
                        AddCharSet(dict, "ĺļľŀłƚƛ", "l");
                        AddCharSet(dict, "ĹĻĽĿŁ", "L");
                        AddCharSet(dict, "Ɯ", "m");
                        AddCharSet(dict, "ñńņňŉŋƞ", "n");
                        AddCharSet(dict, "ÑŃŅŇŊƝ", "N");
                        AddCharSet(dict, "ƥ", "p");
                        AddCharSet(dict, "Ƥ", "P");
                        AddCharSet(dict, "ŕŗř", "r");
                        AddCharSet(dict, "ŔŖŘ", "R");
                        AddCharSet(dict, "śŝşšßſƨ", "s");
                        AddCharSet(dict, "ŚŜŞŠƧƩƪ", "S");
                        AddCharSet(dict, "þţťŧƫƭ", "t");
                        AddCharSet(dict, "ÞŢŤŦƬƮ", "T");
                        AddCharSet(dict, "Ʋ", "V");
                        AddCharSet(dict, "ŵ", "w");
                        AddCharSet(dict, "Ŵ", "W");
                        AddCharSet(dict, "ýŷÿƴ", "y");
                        AddCharSet(dict, "ÝŶŸƳ", "Y");
                        AddCharSet(dict, "Ʀ", "yr");
                        AddCharSet(dict, "źżžƶƹƺ", "z");
                        AddCharSet(dict, "ŹŻŽƵƷƸ", "Z");

                        ToPlainTextLookup = dict;
                    }
        }

        private static void AddCharSet(Dictionary<char, string> dict, string charset, string substitution)
        {
            foreach (var ch in charset)
                dict.Add(ch, substitution);
        }
        #endregion

        #region [ IsValidCSharpIdentifier ]
        // http://msdn.microsoft.com/en-us/library/aa664670(v=vs.71).aspx
        public const string CSharpLetterCharPattern = @"\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}";
        public const string CSharpCombiningCharPattern = @"\p{Mn}\p{Mc}";
        public const string CSharpDecimalDigitCharPattern = @"\p{Nd}";
        public const string CSharpConnectingCharPattern = @"\p{Pc}";
        public const string CSharpFormattingCharPattern = @"\p{Cf}";
        public const string CSharpIdentifierStartCharPattern = "[" + CSharpLetterCharPattern + "_" + "]";
        public const string CSharpIdentifierPartCharPattern = "[" + CSharpLetterCharPattern + CSharpDecimalDigitCharPattern + CSharpConnectingCharPattern + CSharpCombiningCharPattern + CSharpFormattingCharPattern + "]";
        public const string PartCSharpIdentifierPattern = CSharpIdentifierStartCharPattern + CSharpIdentifierPartCharPattern + @"*";

        public static readonly Regex PartCSharpIdentifierRegex = new Regex(PartCSharpIdentifierPattern, RegexOptions.Compiled);
        public static readonly Regex AloneCSharpIdentifierRegex = new Regex("^" + PartCSharpIdentifierPattern + "$", RegexOptions.Compiled);

        public static bool IsValidCSharpIdentifier(this string text)
        {
            return text != null && AloneCSharpIdentifierRegex.IsMatch(text);
        }
        public static bool ValidateCSharpIdentifier(this string text, bool throwOnError = true)
        {
            if (!text.IsValidCSharpIdentifier())
                if (throwOnError)
                    throw new ArgumentException(string.Format(Resources.InvalidCSharpIdentifier, text));
                else
                    return false;

            return true;
        }
        #endregion

        #region [ Regex ]

        public const string RegexSpecialChars = @".$^{[(|)*+?\";
        public const string RegexSpecialCharsPattern = @"[\.\$\^\{\[\(\|\)\*\+\?\\]";
        public static readonly Regex RegexSpecialCharsRegex = new Regex(@"(?<special>" + RegexSpecialCharsPattern + @")", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static string EscapeForRegex(this char ch)
        {
            return ch.ToString().EscapeForRegex();
        }
        public static string EscapeForRegex(this string text)
        {
            if (text == null) return null;
            var result = RegexSpecialCharsRegex.Replace(text, @"\${special}");
            return result;
        }
        #endregion [ Regex ]

    }
}
