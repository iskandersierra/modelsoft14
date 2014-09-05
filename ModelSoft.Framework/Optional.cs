using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
  [Serializable]
  public struct Optional<T> : ICloneable, IEquatable<Optional<T>>, ISerializable
  {
    public static string NoHasValueToString { get { return Resources.Optional_NoHasValueToString; } }

    private readonly bool hasValue;
    private readonly T value;

    public Optional(T value)
      : this()
    {
      this.value = value;
      hasValue = true;
    }

    public static Optional<T> Null
    {
      get
      {
        return new Optional<T>();
      }
    }

    public T Value
    {
      get
      {
        if (!hasValue)
          throw new InvalidOperationException(Resources.ExMsg_Optional_HasNoValueException);
        return value;
      }
    }

    public T ValueOrDefault
    {
      get
      {
        if (!hasValue)
          return default(T);
        return value;
      }
    }

    public bool HasValue
    {
      get
      {
        return hasValue;
      }
    }

    public T GetValueOr(T defaultValue)
    {
      if (!hasValue)
        return defaultValue;
      return value;
    }

    public static implicit operator Optional<T>(T val)
    {
      if (object.ReferenceEquals(val, null)) return Null;
      return new Optional<T>(val);
    }

    public static explicit operator T(Optional<T> val)
    {
      return val.Value;
    }

    #region [ Clone ]
    public Optional<T> Clone()
    {
      return (Optional<T>)MemberwiseClone();
    }

    object ICloneable.Clone()
    {
      return Clone();
    }
    #endregion

    #region [ ToString ]
    public override string ToString()
    {
      if (HasValue)
        return String.Empty + Value;
      return NoHasValueToString;
    }
    #endregion

    #region [ Equals / GetHashCode ]
    public bool Equals(Optional<T> obj)
    {
      return obj.hasValue.Equals(hasValue) && Equals(obj.value, value);
    }

    public override bool Equals(object obj)
    {
      if (obj.GetType() != typeof(Optional<T>)) return false;
      return Equals((Optional<T>)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (hasValue.GetHashCode() * 397) ^ (ReferenceEquals(value, null) ? 0 : value.GetHashCode());
      }
    }

    public static bool operator ==(Optional<T> left, Optional<T> right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Optional<T> left, Optional<T> right)
    {
      return !left.Equals(right);
    }
    #endregion

    #region ISerializable Members

    private Optional(SerializationInfo info, StreamingContext context)
    {
      hasValue = info.GetBoolean("H");
      if (hasValue)
        value = (T)info.GetValue("V", typeof(T));
      else
        value = default(T);
    }

    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (!hasValue)
        info.AddValue("H", false);
      else
      {
        info.AddValue("H", true);
        info.AddValue("V", value);
      }
    }

    #endregion
  }
}
