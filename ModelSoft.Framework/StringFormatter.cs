using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework
{
  public class StringFormatter
  {
    private readonly string _nullString;
    private readonly string[] _captionProperties;
    private readonly Func<Type, bool> _skipType;
    private readonly ISimpleCache<Type, Func<object, string>> _toStringProperties;

    public StringFormatter(IEnumerable<string> captionProperties, Func<Type, bool> skipType, string nullString)
    {
      this._captionProperties = captionProperties != null ? captionProperties.ToArray() : null;
      this._skipType = skipType;
      this._nullString = nullString;

      _toStringProperties = new SimpleConcurrentCache<Type, Func<object, string>>(CreateToStringFunction);
    }

    public string Format(object obj)
    {
      if (obj == null)
        return _nullString;

      var type = obj.GetType();
      var func = _toStringProperties.Get(type);
      var result = func(obj);
      return result;
    }

    private Func<object, string> CreateToStringFunction(Type currentType)
    {

      var allTypes = currentType
        .Unfold(e => e.BaseType)
        .Where(e => _skipType == null || !_skipType(e))
        .Reverse()
        .ToArray();

      var allProperties = allTypes
        .SelectMany(e => e.GetProperties().Where(p => p.GetGetMethod() != null && p.GetIndexParameters().Length == 0))
        .OrderBy(e => e.Name.Length)
        .ThenBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
        .ToArray();

      var registeredProperty = _captionProperties != null
        ? allProperties
          .Where(p => _captionProperties.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
          .OrderBy(p => _captionProperties.IndexOf(p.Name, StringComparer.OrdinalIgnoreCase))
          .FirstOrDefault()
        : null;

      var stringProperty = registeredProperty ?? allProperties.FirstOrDefault(p => p.PropertyType == typeof(string));

      stringProperty = stringProperty ?? allProperties.FirstOrDefault();

      // e => string.Format("{0}: {1}", "CurrentType", "" + ((CurrentType)e).Property);
      var parameter = Expression.Parameter(typeof(object), "e");
      var stringType = typeof(string);
      var objectType = typeof(object);
      Expression<Func<object, string>> expression;
      if (stringProperty != null)
        expression = Expression.Lambda<Func<object, string>>(
          Expression.Call(
            stringType.GetMethod("Format", BindingFlags.Public | BindingFlags.Static, null, new[] { stringType, objectType, objectType }, null),
            Expression.Constant("{0}: {1}"),
            Expression.Constant(currentType.Name),
            Expression.Convert(Expression.MakeMemberAccess(Expression.Convert(parameter, currentType), stringProperty), objectType)
          ),
          parameter
        );
      else
        expression = Expression.Lambda<Func<object, string>>(
          Expression.Call(
            stringType.GetMethod("Format", BindingFlags.Public | BindingFlags.Static, null, new[] { stringType, objectType, objectType }, null),
            Expression.Constant("{0}: {1}"),
            Expression.Constant(currentType.Name),
            Expression.Call(
              Expression.Call(parameter, objectType.GetMethod("GetHashCode")),
              typeof(int).GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance, null, new[] { stringType }, null),
              Expression.Constant("X"))
          ),
          parameter
        );

      var result = expression.Compile();
      return result;
    }

  }
}
