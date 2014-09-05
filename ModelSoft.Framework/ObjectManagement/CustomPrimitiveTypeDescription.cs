using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public abstract class CustomPrimitiveTypeDescription : 
    CustomDataTypeDescription,
    IPrimitiveTypeDescription
  {
    public CustomPrimitiveTypeDescription()
    {
    }

    public CustomPrimitiveTypeDescription(string _Identifier, string _Name)
      : base(_Identifier, _Name)
    {
    }

    public virtual bool SupportsStringSerialization { get { return false; } }

    public virtual bool SupportsBinarySerialization { get { return false; } }

    public virtual bool SupportsIncrementDecrement { get { return false; } }

    public virtual string SerializeValueToString(object value)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportStringSerialization(this.ToString()));
    }

    public virtual object DeserializeValueFromString(string serializedValue)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportStringSerialization(this.ToString()));
    }

    public virtual byte[] SerializeValueToBinary(object value)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportBinarySerialization(this.ToString()));
    }

    public virtual object DeserializeValueFromBinary(byte[] serializedValue)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportBinarySerialization(this.ToString()));
    }

    public virtual object Increment(object value)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportIncrementDecrement(this.ToString()));
    }

    public virtual object Decrement(object value)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportIncrementDecrement(this.ToString()));
    }

    public virtual bool AreEquals(object value1, object value2)
    {
      return object.Equals(value1, value2);
    }

    public virtual int GetHashCode(object value)
    {
      return value.GetHashCode();
    }

    public virtual string ToString(object value)
    {
      return "" + value;
    }

    public virtual bool SupportsCompare { get { return false; } }

    public virtual int Compare(object value1, object value2)
    {
      throw new NotSupportedException(FormattedResources.ExMsg_DoNotSupportCompare(this.ToString()));
    }
  }
}
