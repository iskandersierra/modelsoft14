using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public abstract class InjectParameterValueAttribute : 
    Attribute
  {
    public int Order { get; set; }
  }
  
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public abstract class InjectSimpleParameterValueAttribute :
    InjectParameterValueAttribute
  {
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public abstract class InjectArrayParameterValueAttribute :
    InjectParameterValueAttribute
  {
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public class InjectOptionalParameterAttribute :
    InjectSimpleParameterValueAttribute
  {
    public InjectOptionalParameterAttribute()
    {
    }

    public InjectOptionalParameterAttribute(Type service)
      : this(service, null)
    {
    }

    public InjectOptionalParameterAttribute(Type service, string key)
    {
      this.Service = service;
      this.Key = key;
    }

    public Type Service { get; set; }
    public string Key { get; set; }
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public class InjectResolvedParameterAttribute :
    InjectSimpleParameterValueAttribute
  {
    public InjectResolvedParameterAttribute()
    {
    }

    public InjectResolvedParameterAttribute(Type service)
      : this(service, null)
    {
    }

    public InjectResolvedParameterAttribute(Type service, string key)
    {
      this.Service = service;
      this.Key = key;
    }

    public Type Service { get; set; }
    public string Key { get; set; }
  }

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
  public class InjectResolveArrayParameterAttribute :
    InjectArrayParameterValueAttribute
  {
    public InjectResolveArrayParameterAttribute()
    {
    }

    public InjectResolveArrayParameterAttribute(Type elementType)
      : this(null, elementType)
    {
    }

    public InjectResolveArrayParameterAttribute(Type arrayType, Type elementType)
    {
      this.ArrayType = arrayType;
      this.ElementType = elementType;
    }

    public Type ArrayType { get; set; }
    public Type ElementType { get; set; }
  }

}
