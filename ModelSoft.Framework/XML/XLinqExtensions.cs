using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelSoft.Framework.XML
{
  public static class XLinqExtensions
  {
    public static string GetValueOr(this XAttribute attr, string defaultValue)
    {
      if (attr == null) return defaultValue;
      return (string)attr;
    }
    public static DateTime? GetValueOr(this XAttribute attr, DateTime? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (DateTime?)attr;
    }
    public static DateTime GetValueOr(this XAttribute attr, DateTime defaultValue)
    {
      if (attr == null) return defaultValue;
      return (DateTime)attr;
    }
    public static bool GetValueOr(this XAttribute attr, bool defaultValue)
    {
      if (attr == null) return defaultValue;
      return (bool)attr;
    }
    public static bool? GetValueOr(this XAttribute attr, bool? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (bool?)attr;
    }
    public static Guid GetValueOr(this XAttribute attr, Guid defaultValue)
    {
      if (attr == null) return defaultValue;
      return (Guid)attr;
    }
    public static Guid? GetValueOr(this XAttribute attr, Guid? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (Guid?)attr;
    }
    public static int GetValueOr(this XAttribute attr, int defaultValue)
    {
      if (attr == null) return defaultValue;
      return (int)attr;
    }
    public static int? GetValueOr(this XAttribute attr, int? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (int?)attr;
    }
    public static TimeSpan? GetValueOr(this XAttribute attr, TimeSpan? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (TimeSpan?)attr;
    }
    public static TimeSpan GetValueOr(this XAttribute attr, TimeSpan defaultValue)
    {
      if (attr == null) return defaultValue;
      return (TimeSpan)attr;
    }
    public static DateTimeOffset? GetValueOr(this XAttribute attr, DateTimeOffset? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (DateTimeOffset?)attr;
    }
    public static DateTimeOffset GetValueOr(this XAttribute attr, DateTimeOffset defaultValue)
    {
      if (attr == null) return defaultValue;
      return (DateTimeOffset)attr;
    }
    public static long? GetValueOr(this XAttribute attr, long? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (long?)attr;
    }
    public static long GetValueOr(this XAttribute attr, long defaultValue)
    {
      if (attr == null) return defaultValue;
      return (long)attr;
    }
    public static ulong? GetValueOr(this XAttribute attr, ulong? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (ulong?)attr;
    }
    public static ulong GetValueOr(this XAttribute attr, ulong defaultValue)
    {
      if (attr == null) return defaultValue;
      return (ulong)attr;
    }
    public static float? GetValueOr(this XAttribute attr, float? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (float?)attr;
    }
    public static float GetValueOr(this XAttribute attr, float defaultValue)
    {
      if (attr == null) return defaultValue;
      return (float)attr;
    }
    public static decimal? GetValueOr(this XAttribute attr, decimal? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (decimal?)attr;
    }
    public static decimal GetValueOr(this XAttribute attr, decimal defaultValue)
    {
      if (attr == null) return defaultValue;
      return (decimal)attr;
    }
    public static double? GetValueOr(this XAttribute attr, double? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (double?)attr;
    }
    public static double GetValueOr(this XAttribute attr, double defaultValue)
    {
      if (attr == null) return defaultValue;
      return (double)attr;
    }
    public static uint? GetValueOr(this XAttribute attr, uint? defaultValue)
    {
      if (attr == null) return defaultValue;
      return (uint?)attr;
    }
    public static uint GetValueOr(this XAttribute attr, uint defaultValue)
    {
      if (attr == null) return defaultValue;
      return (uint)attr;
    }

    public static string GetValueOr(this XElement element, string defaultValue)
    {
      if (element == null) return defaultValue;
      return (string)element;
    }
    public static DateTime? GetValueOr(this XElement element, DateTime? defaultValue)
    {
      if (element == null) return defaultValue;
      return (DateTime?)element;
    }
    public static DateTime GetValueOr(this XElement element, DateTime defaultValue)
    {
      if (element == null) return defaultValue;
      return (DateTime)element;
    }
    public static bool GetValueOr(this XElement element, bool defaultValue)
    {
      if (element == null) return defaultValue;
      return (bool)element;
    }
    public static bool? GetValueOr(this XElement element, bool? defaultValue)
    {
      if (element == null) return defaultValue;
      return (bool?)element;
    }
    public static Guid GetValueOr(this XElement element, Guid defaultValue)
    {
      if (element == null) return defaultValue;
      return (Guid)element;
    }
    public static Guid? GetValueOr(this XElement element, Guid? defaultValue)
    {
      if (element == null) return defaultValue;
      return (Guid?)element;
    }
    public static int GetValueOr(this XElement element, int defaultValue)
    {
      if (element == null) return defaultValue;
      return (int)element;
    }
    public static int? GetValueOr(this XElement element, int? defaultValue)
    {
      if (element == null) return defaultValue;
      return (int?)element;
    }
    public static TimeSpan? GetValueOr(this XElement element, TimeSpan? defaultValue)
    {
      if (element == null) return defaultValue;
      return (TimeSpan?)element;
    }
    public static TimeSpan GetValueOr(this XElement element, TimeSpan defaultValue)
    {
      if (element == null) return defaultValue;
      return (TimeSpan)element;
    }
    public static DateTimeOffset? GetValueOr(this XElement element, DateTimeOffset? defaultValue)
    {
      if (element == null) return defaultValue;
      return (DateTimeOffset?)element;
    }
    public static DateTimeOffset GetValueOr(this XElement element, DateTimeOffset defaultValue)
    {
      if (element == null) return defaultValue;
      return (DateTimeOffset)element;
    }
    public static long? GetValueOr(this XElement element, long? defaultValue)
    {
      if (element == null) return defaultValue;
      return (long?)element;
    }
    public static long GetValueOr(this XElement element, long defaultValue)
    {
      if (element == null) return defaultValue;
      return (long)element;
    }
    public static ulong? GetValueOr(this XElement element, ulong? defaultValue)
    {
      if (element == null) return defaultValue;
      return (ulong?)element;
    }
    public static ulong GetValueOr(this XElement element, ulong defaultValue)
    {
      if (element == null) return defaultValue;
      return (ulong)element;
    }
    public static float? GetValueOr(this XElement element, float? defaultValue)
    {
      if (element == null) return defaultValue;
      return (float?)element;
    }
    public static float GetValueOr(this XElement element, float defaultValue)
    {
      if (element == null) return defaultValue;
      return (float)element;
    }
    public static decimal? GetValueOr(this XElement element, decimal? defaultValue)
    {
      if (element == null) return defaultValue;
      return (decimal?)element;
    }
    public static decimal GetValueOr(this XElement element, decimal defaultValue)
    {
      if (element == null) return defaultValue;
      return (decimal)element;
    }
    public static double? GetValueOr(this XElement element, double? defaultValue)
    {
      if (element == null) return defaultValue;
      return (double?)element;
    }
    public static double GetValueOr(this XElement element, double defaultValue)
    {
      if (element == null) return defaultValue;
      return (double)element;
    }
    public static uint? GetValueOr(this XElement element, uint? defaultValue)
    {
      if (element == null) return defaultValue;
      return (uint?)element;
    }
    public static uint GetValueOr(this XElement element, uint defaultValue)
    {
      if (element == null) return defaultValue;
      return (uint)element;
    }
  }
}
