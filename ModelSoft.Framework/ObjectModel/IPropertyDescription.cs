using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public interface IPropertyDescription
  {
    string Name { get; }

    string FriendlyName { get; }

    bool IsReadOnly { get; }

    Type PropertyType { get; }

    object DefaultValue { get; }

    bool HasDefaultValue { get; }

    void ComputeValue(PropertyDescriptionComputeContext context);

    bool HasComputeValue { get; }

    void CoerceValue(PropertyDescriptionCoerceContext context);

    void ChangingValue(PropertyDescriptionChangingValueContext context);

    void ChangeValue(PropertyDescriptionChangeValueContext context);

    IPropertyDescription AsReadOnlyProperty { get; }

    object GetValue(object instance);

    void SetValue(object instance, object value);
  }

  public interface IPropertyDescription<T> :
    IPropertyDescription
  {
    new IPropertyDescription<T> AsReadOnlyProperty { get; }

    new T GetValue(object instance);

    new void SetValue(object instance, T value);

    new T DefaultValue { get; }
  }

  internal interface IPropertyDescriptionImpl
  {
    void SetDeclaringClass(IClassDescription declaringClass);
  }

}
