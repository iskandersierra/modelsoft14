using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework
{
  public static class CommonTypes
  {
    private static Type _TypeOfArray;
    public static Type TypeOfArray { get { return _TypeOfArray ?? (_TypeOfArray = typeof(global::System.Array)); } }

    private static Type _TypeOfBoolean;
    public static Type TypeOfBoolean { get { return _TypeOfBoolean ?? (_TypeOfBoolean = typeof(global::System.Boolean)); } }

    private static Type _TypeOfByte;
    public static Type TypeOfByte { get { return _TypeOfByte ?? (_TypeOfByte = typeof(global::System.Byte)); } }

    private static Type _TypeOfChar;
    public static Type TypeOfChar { get { return _TypeOfChar ?? (_TypeOfChar = typeof(global::System.Char)); } }

    private static Type _TypeOfDateTime;
    public static Type TypeOfDateTime { get { return _TypeOfDateTime ?? (_TypeOfDateTime = typeof(global::System.DateTime)); } }

    private static Type _TypeOfDateTimeOffset;
    public static Type TypeOfDateTimeOffset { get { return _TypeOfDateTimeOffset ?? (_TypeOfDateTimeOffset = typeof(global::System.DateTimeOffset)); } }

    private static Type _TypeOfDBNull;
    public static Type TypeOfDBNull { get { return _TypeOfDBNull ?? (_TypeOfDBNull = typeof(global::System.DBNull)); } }

    private static Type _TypeOfDecimal;
    public static Type TypeOfDecimal { get { return _TypeOfDecimal ?? (_TypeOfDecimal = typeof(global::System.Decimal)); } }

    private static Type _TypeOfDouble;
    public static Type TypeOfDouble { get { return _TypeOfDouble ?? (_TypeOfDouble = typeof(global::System.Double)); } }

    private static Type _TypeOfEnum;
    public static Type TypeOfEnum { get { return _TypeOfEnum ?? (_TypeOfEnum = typeof(global::System.Enum)); } }

    private static Type _TypeOfGuid;
    public static Type TypeOfGuid { get { return _TypeOfGuid ?? (_TypeOfGuid = typeof(global::System.Guid)); } }
    
    private static Type _TypeOfInt16;
    public static Type TypeOfInt16 { get { return _TypeOfInt16 ?? (_TypeOfInt16 = typeof(global::System.Int16)); } }

    private static Type _TypeOfInt32;
    public static Type TypeOfInt32 { get { return _TypeOfInt32 ?? (_TypeOfInt32 = typeof(global::System.Int32)); } }

    private static Type _TypeOfInt64;
    public static Type TypeOfInt64 { get { return _TypeOfInt64 ?? (_TypeOfInt64 = typeof(global::System.Int64)); } }

    private static Type _TypeOfIntPtr;
    public static Type TypeOfIntPtr { get { return _TypeOfIntPtr ?? (_TypeOfIntPtr = typeof(global::System.IntPtr)); } }

    private static Type _TypeOfNullable;
    public static Type TypeOfNullable { get { return _TypeOfNullable ?? (_TypeOfNullable = typeof(global::System.Nullable)); } }

    private static Type _TypeOfNullable1;
    public static Type TypeOfNullable1 { get { return _TypeOfNullable1 ?? (_TypeOfNullable1 = typeof(global::System.Nullable<>)); } }

    private static Type _TypeOfObject;
    public static Type TypeOfObject { get { return _TypeOfObject ?? (_TypeOfObject = typeof(global::System.Object)); } }

    private static Type _TypeOfSByte;
    public static Type TypeOfSByte { get { return _TypeOfSByte ?? (_TypeOfSByte = typeof(global::System.SByte)); } }

    private static Type _TypeOfSingle;
    public static Type TypeOfSingle { get { return _TypeOfSingle ?? (_TypeOfSingle = typeof(global::System.Single)); } }

    private static Type _TypeOfString;
    public static Type TypeOfString { get { return _TypeOfString ?? (_TypeOfString = typeof(global::System.String)); } }

    private static Type _TypeOfTimeSpan;
    public static Type TypeOfTimeSpan { get { return _TypeOfTimeSpan ?? (_TypeOfTimeSpan = typeof(global::System.TimeSpan)); } }

    private static Type _TypeOfType;
    public static Type TypeOfType { get { return _TypeOfType ?? (_TypeOfType = typeof(global::System.Type)); } }
    
    private static Type _TypeOfUInt16;
    public static Type TypeOfUInt16 { get { return _TypeOfUInt16 ?? (_TypeOfUInt16 = typeof(global::System.UInt16)); } }

    private static Type _TypeOfUInt32;
    public static Type TypeOfUInt32 { get { return _TypeOfUInt32 ?? (_TypeOfUInt32 = typeof(global::System.UInt32)); } }

    private static Type _TypeOfUInt64;
    public static Type TypeOfUInt64 { get { return _TypeOfUInt64 ?? (_TypeOfUInt64 = typeof(global::System.UInt64)); } }

    private static Type _TypeOfUIntPtr;
    public static Type TypeOfUIntPtr { get { return _TypeOfUIntPtr ?? (_TypeOfUIntPtr = typeof(global::System.UIntPtr)); } }

    private static Type _TypeOfValueType;
    public static Type TypeOfValueType { get { return _TypeOfValueType ?? (_TypeOfValueType = typeof(global::System.ValueType)); } }

    private static Type _TypeOfVoid;
    public static Type TypeOfVoid { get { return _TypeOfVoid ?? (_TypeOfVoid = typeof(void)); } }

    private static Type _TypeOfEnumerable;
    public static Type TypeOfEnumerable { get { return _TypeOfEnumerable ?? (_TypeOfEnumerable = typeof(global::System.Linq.Enumerable)); } }

    private static Type _TypeOfQueryable;
    public static Type TypeOfQueryable { get { return _TypeOfQueryable ?? (_TypeOfQueryable = typeof(global::System.Linq.Queryable)); } }

    private static Type _TypeOfIEnumerable;
    public static Type TypeOfIEnumerable { get { return _TypeOfIEnumerable ?? (_TypeOfIEnumerable = typeof(global::System.Collections.IEnumerable)); } }

    private static Type _TypeOfIEnumerable1;
    public static Type TypeOfIEnumerable1 { get { return _TypeOfIEnumerable1 ?? (_TypeOfIEnumerable1 = typeof(global::System.Collections.Generic.IEnumerable<>)); } }

    private static Type _TypeOfIQueryable;
    public static Type TypeOfIQueryable { get { return _TypeOfIQueryable ?? (_TypeOfIQueryable = typeof(global::System.Linq.IQueryable)); } }

    private static Type _TypeOfIQueryable1;
    public static Type TypeOfIQueryable1 { get { return _TypeOfIQueryable1 ?? (_TypeOfIQueryable1 = typeof(global::System.Linq.IQueryable<>)); } }

    private static Type _TypeOfAttribute;
    public static Type TypeOfAttribute { get { return _TypeOfAttribute ?? (_TypeOfAttribute = typeof(global::System.Attribute)); } }

    public static readonly object[] EmptyArrayOfObject = new object[0];
    public static readonly string[] EmptyArrayOfString = new string[0];
    public static readonly Type[] EmptyArrayOfType = Type.EmptyTypes;
    public static readonly Attribute[] EmptyArrayOfAttribute = new Attribute[0];

    private static ISimpleCache<Type, Type> GetNullableOfTCache = new SimpleConcurrentCache<Type, Type>(t => TypeOfNullable1.MakeGenericType(t));
    public static Type GetNullableOfT(Type type)
    {
      return GetNullableOfTCache.Get(type);
    }

    private static ISimpleCache<Type, Type> GetIEnumerableOfTCache = new SimpleConcurrentCache<Type, Type>(t => TypeOfIEnumerable1.MakeGenericType(t));
    public static Type GetIEnumerableOfT(Type type)
    {
      return GetIEnumerableOfTCache.Get(type);
    }

    private static ISimpleCache<Type, Type> GetIQueryableOfTCache = new SimpleConcurrentCache<Type, Type>(t => TypeOfIQueryable1.MakeGenericType(t));
    public static Type GetIQueryableOfT(Type type)
    {
      return GetIQueryableOfTCache.Get(type);
    }
  }
}
