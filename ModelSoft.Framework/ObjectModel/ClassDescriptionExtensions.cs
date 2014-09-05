using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.ObjectModel
{
  public static class ClassDescriptionExtensions
  {
    public static IPropertyDescription RegisterProperty(this IClassDescription classDescription, Type type, PropertyInfo property, PropertyDescriptionSetup setup = null)
    {
      classDescription.RequireNotNull("classDescription");

      type.RequireNotNull("type");
      type.IsClass.RequireCondition("type", @"Type ""{0}"" is not a class".Fmt(type.FullName));

      property.RequireNotNull("property");
      property.CanRead.RequireCondition("property", @"Property ""{0}"" on type ""{1}"" must be readable".Fmt(property.Name, type.FullName));
      (!property.GetMethod.IsStatic).RequireCondition("property", @"Property ""{0}"" on type ""{1}"" cannot be static".Fmt(property.Name, type.FullName));
      property.DeclaringType.IsAssignableFrom(type).RequireCondition("type", @"Type ""{1}"" must be derived from the property's ""{0}"" declaring type ""{2}""".Fmt(property.Name, type.FullName, property.DeclaringType.FullName));

      if (setup == null)
        setup = CreatePropertyInfoSetup(property);

      var propertyType = property.PropertyType;
      var propDescType = typeof(PropertyInfoDescription<>).MakeGenericType(propertyType);
      var propDesc = (IPropertyDescription)Activator.CreateInstance(propDescType, property, setup);

      classDescription.RegisterProperty(propDesc);

      return propDesc;
    }

    private static PropertyDescriptionSetup CreatePropertyInfoSetup(PropertyInfo property)
    {
      return new PropertyDescriptionSetup();
    }
    public static IPropertyDescription RegisterProperty(this IClassDescription classDescription, PropertyInfo property, PropertyDescriptionSetup setup = null)
    {
      var propDesc = classDescription.RegisterProperty(property.DeclaringType, property, setup);

      return propDesc;
    }

    public static IPropertyDescription RegisterProperty<TType, TValue>(this IClassDescription classDescription, Expression<Func<TType, TValue>> propertyAccessor, PropertyDescriptionSetup setup = null)
    {
      propertyAccessor.RequireNotNull("propertyAccessor");

      var type = typeof(TType);
      var property = propertyAccessor.GetPropertyInfo();

      return classDescription.RegisterProperty(type, property, setup);
    }
  }
}
