using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace ModelSoft.Framework.XML
{
  public class SimpleValueSerializerContext : IValueSerializerContext
  {
    Type componentType;
    string propertyName;
    ValueSerializer localSerializer;

    public static readonly SimpleValueSerializerContext Default = new SimpleValueSerializerContext();

    public SimpleValueSerializerContext()
    {
    }

    public SimpleValueSerializerContext(Type componentType, string propertyName, ValueSerializer localSerializer)
    {
      componentType.RequireNotNull("componentType");
      propertyName.RequireNotWhitespace("propertyName");
      localSerializer.RequireNotNull("localSerializer");

      this.componentType = componentType;
      this.propertyName = propertyName;
      this.localSerializer = localSerializer;
    }

    public ValueSerializer GetValueSerializerFor(System.ComponentModel.PropertyDescriptor descriptor)
    {
      if (componentType == descriptor.ComponentType && propertyName == descriptor.Name)
        return localSerializer;
      return ValueSerializer.GetSerializerFor(descriptor);
    }

    public ValueSerializer GetValueSerializerFor(Type type)
    {
      return ValueSerializer.GetSerializerFor(type);
    }

    public System.ComponentModel.IContainer Container
    {
      get { return null; }
    }

    public object Instance
    {
      get { return null; }
    }

    public void OnComponentChanged()
    {
    }

    public bool OnComponentChanging()
    {
      return false;
    }

    public System.ComponentModel.PropertyDescriptor PropertyDescriptor
    {
      get { return null; }
    }

    public object GetService(Type serviceType)
    {
      if (typeof(IValueSerializerContext).IsAssignableFrom(serviceType))
        return this;
      return null;
    }
  }
}
