using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSoft.Framework
{
  public static class EnumExtensions
  {
    public static E Parse<E>(string value)
      where E : struct
    {
      return (E)Enum.Parse(typeof(E), value);
    }

    public static bool TryParse<E>(string value, out E result)
      where E : struct
    {
      try
      {
        result = Parse<E>(value);
        return true;
      }
      catch
      {
        result = default(E);
        return false;
      }
    }
  }
}
