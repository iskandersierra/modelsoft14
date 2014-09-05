using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ModelSoft.Framework
{
  public class StringSpliterConverter : ItemConverterBase<string, string[]>
  {
    public static readonly StringSpliterConverter Default = new StringSpliterConverter();

    char listSeparator;
    char quoteChar;
    bool allowEscapeSequences;

    public StringSpliterConverter()
      : this(',', '"', true)
    {
    }

    public StringSpliterConverter(char listSeparator, char quoteChar, bool allowEscapeSequences)
    {
      this.listSeparator = listSeparator;
      this.quoteChar = quoteChar;
      this.allowEscapeSequences = allowEscapeSequences;
    }

    public override bool TryConvert(string item, out string[] result)
    {
      if (item == null)
      {
        result = null;
        return false;
      }
      StringBuilder sb = new StringBuilder();
      List<string> splits = new List<string>();
      bool isOpenQuote = false;
      bool isEscapedChar = false;
      for (int i = 0; i < item.Length; i++)
      {
        char ch = item[i];
        if (isEscapedChar)
        {
          if (ch == quoteChar)
          {
            sb.Append(quoteChar);
            isEscapedChar = false;
          }
          else
          {
            switch (ch)
            {
              case 'a' : sb.Append('\a'); break;
              case 'b' : sb.Append('\b'); break;
              case 'f' : sb.Append('\f'); break;
              case 'n' : sb.Append('\n'); break;
              case 'r' : sb.Append('\r'); break;
              case 't' : sb.Append('\t'); break;
              case 'v' : sb.Append('\v'); break;
              case '\'': sb.Append('\''); break;
              case '"' : sb.Append('"');  break;
              case '\\': sb.Append('\\'); break;
              case '?' : sb.Append('?'); break;
              default  : sb.Append('\\'); i--; break;
            }
            isEscapedChar = false; 
          }
        }
        else
          if (isOpenQuote)
          {
            if (ch == '\\' && allowEscapeSequences)
              isEscapedChar = true;
            else if (ch == quoteChar)
              isOpenQuote = false;
            else 
              sb.Append(ch);
          }
          else
          {
            if (ch == listSeparator)
            {
              splits.Add(sb.ToString());
              sb.Clear();
            }
            else if (ch == quoteChar)
              isOpenQuote = true;
            else
              sb.Append(ch);
          }
      }
      if (isEscapedChar)
        sb.Append('\\');
      splits.Add(sb.ToString());
      result = splits.ToArray();
      return !isOpenQuote;
    }
  }
}
