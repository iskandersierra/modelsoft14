using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public abstract class InjectParameterValueBase
  {
  }

  public class InjectOptionalParameterValue :
    InjectParameterValueBase
  {
    public InjectOptionalParameterValue()
    {
    }

    public InjectOptionalParameterValue(Type service)
      : this(service, null)
    {
    }

    public InjectOptionalParameterValue(Type service, string key)
    {
      this.Service = service;
      this.Key = key;
    }

    public Type Service { get; set; }
    public string Key { get; set; }
  }
  
  public class InjectParameterValue :
    InjectParameterValueBase
  {
    public InjectParameterValue()
    {
    }

    public InjectParameterValue(object parameterValue)
    {
      this.ParameterValue = parameterValue;
    }

    public object ParameterValue { get; set; }
  }

  public class InjectResolvedParameterValue :
    InjectParameterValueBase
  {
    public InjectResolvedParameterValue()
    {
    }

    public InjectResolvedParameterValue(Type service)
      : this(service, null)
    {
    }

    public InjectResolvedParameterValue(Type service, string key)
    {
      this.Service = service;
      this.Key = key;
    }

    public Type Service { get; set; }
    public string Key { get; set; }
  }

  public class InjectResolveArrayParameterValue :
    InjectParameterValueBase
  {
    public InjectResolveArrayParameterValue()
    {
    }

    public InjectResolveArrayParameterValue(Type elementType, params object[] elementValues)
      : this(null, elementType, elementValues)
    {
    }

    public InjectResolveArrayParameterValue(Type arrayType, Type elementType, params object[] elementValues)
    {
      this.ArrayType = arrayType;
      this.ElementType = elementType;
      this.ElementValues = elementValues;
    }

    public Type ArrayType { get; set; }
    public Type ElementType { get; set; }
    public object[] ElementValues { get; set; }
  }

  public class InjectGenericResolveArrayParameterValue :
    InjectParameterValueBase
  {
    public InjectGenericResolveArrayParameterValue()
    {
    }

    public InjectGenericResolveArrayParameterValue(string genericParameterName, params object[] elementValues)
    {
      this.GenericParameterName = genericParameterName;
      this.ElementValues = elementValues;
    }

    public string GenericParameterName { get; set; }
    public object[] ElementValues { get; set; }
  }

  public class InjectGenericParameterValue :
    InjectParameterValueBase
  {
    public InjectGenericParameterValue()
    {
    }

    public InjectGenericParameterValue(string genericParameterName)
      : this(genericParameterName, null)
    {
    }


    public InjectGenericParameterValue(string genericParameterName, string key)
    {
      this.GenericParameterName = genericParameterName;
      this.Key = key;
    }

    public string GenericParameterName { get; set; }
    public string Key { get; set; }
  }

  public class InjectOptionalGenericParameterValue :
    InjectParameterValueBase
  {
    public InjectOptionalGenericParameterValue()
    {
    }

    public InjectOptionalGenericParameterValue(string genericParameterName)
      : this(genericParameterName, null)
    {
    }


    public InjectOptionalGenericParameterValue(string genericParameterName, string key)
    {
      this.GenericParameterName = genericParameterName;
      this.Key = key;
    }

    public string GenericParameterName { get; set; }
    public string Key { get; set; }
  }
}
