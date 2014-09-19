using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;

namespace ModelSoft.Framework.ObjectModel
{
  //public class PropertyInfoDescription<T> : 
  //  PropertyDescription<T>
  //{

  //  public PropertyInfoDescription(PropertyInfo propertyInfo, PropertyDescriptionSetup setup = null)
  //    : base(setup)
  //  {
  //    propertyInfo.RequireNotNull("propertyInfo");
  //    propertyInfo.CanRead.RequireCondition("propertyInfo", @"Property ""{0}"" on type ""{1}"" cannot be read".Fmt(propertyInfo.Name, propertyInfo.DeclaringType.FullName));
  //    propertyInfo.CanWrite.RequireCondition("propertyInfo", @"Property ""{0}"" on type ""{1}"" cannot be writen".Fmt(propertyInfo.Name, propertyInfo.DeclaringType.FullName));

  //    this.propertyInfo = propertyInfo;
  //  }

  //  protected internal PropertyInfoDescription(PropertyInfoDescription<T> property)
  //    : base(property)
  //  {
  //    this.propertyInfo = property.propertyInfo;
  //  }

  //  protected PropertyInfo propertyInfo;

  //  public override Type PropertyType { get { return propertyInfo.PropertyType; } }


  //  protected override T GetValueOverride(object instance)
  //  {
  //    var result = (T)propertyInfo.GetValue(instance, null);
      
  //    return result;
  //  }

  //  protected override void SetValueOverride(object instance, T value)
  //  {
  //    propertyInfo.SetValue(instance, value, null);
  //  }

  //  protected override PropertyDescription<T> CreateReadOnlyProperty()
  //  {
  //    return new PropertyInfoDescription<T>(this);
  //  }
  //}
}
