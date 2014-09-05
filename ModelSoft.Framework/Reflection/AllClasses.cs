using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Reflection
{
  public static class AllClasses
  {
    private const bool DefaultSkipOnError = true;
    private const bool DefaultIsConcreteClass = true;
    private const bool DefaultIsGenericDefinitionClass = false;

    #region [ GetAllTypes ]
    public static IEnumerable<Type> GetAllTypes(
      bool skipOnError, 
      params Assembly[] assemblies)
    {
      return GetAllTypes((IEnumerable<Assembly>)assemblies, skipOnError);
    }

    public static IEnumerable<Type> GetAllTypes(params Assembly[] assemblies)
    {
      return GetAllTypes((IEnumerable<Assembly>)assemblies);
    }

    public static IEnumerable<Type> GetAllTypes(
      this IEnumerable<Assembly> assemblies, 
      bool skipOnError = DefaultSkipOnError)
    {
      if (assemblies == null)
        return Enumerable.Empty<Type>();

      return assemblies.SelectMany(a => a.GetAllTypes(skipOnError));
    }

    public static IEnumerable<Type> GetAllTypes(
      this Assembly assembly, 
      bool skipOnError = DefaultSkipOnError)
    {
      if (assembly == null)
        return Enumerable.Empty<Type>();

      IEnumerable<TypeInfo> source;
      try
      {
        source = assembly.DefinedTypes;
      }
      catch (ReflectionTypeLoadException ex)
      {
        if (!skipOnError)
        {
          throw;
        }
        // at least return the types that were loaded
        source = from t in ex.Types.TakeWhile((Type t) => t != null)
                 select t.GetTypeInfo();
      }
#if TRACE
      source = source.ToList();
#endif
      return source;
    }
    #endregion

    #region [ GetAllClasses ]
    public static IEnumerable<Type> GetAllClasses(
      this IEnumerable<Type> types, 
      Type baseType = null,
      bool? isConcrete = DefaultIsConcreteClass,
      bool? isGenericDefinition = DefaultIsGenericDefinitionClass, 
      bool skipOnError = DefaultSkipOnError)
    {
      if (types == null) return CommonTypes.EmptyArrayOfType;

      types = types.Where(c => c.IsClass);

      if (isConcrete != null)
        if (isConcrete.Value)
          types = types.Where(c => !c.IsAbstract);
        else
          types = types.Where(c => c.IsAbstract);

      if (isGenericDefinition != null)
        if (isGenericDefinition.Value)
          types = types.Where(c => c.IsGenericTypeDefinition);
        else
          types = types.Where(c => !c.IsGenericTypeDefinition);

      if (baseType != null)
        types = types.Where(baseType.IsAssignableFrom);

#if TRACE
      types = types.ToList();
#endif

      return types;
    }

    public static IEnumerable<Type> GetAllClasses<TBase>(
      this IEnumerable<Type> types, 
      bool? isConcrete = DefaultIsConcreteClass,
      bool? isGenericDefinition = DefaultIsGenericDefinitionClass, 
      bool skipOnError = DefaultSkipOnError)
    {
      return types.GetAllClasses(typeof(TBase), isConcrete, isGenericDefinition, skipOnError);
    }
    #endregion

    #region [ GetAllInterfaces ]
    public static IEnumerable<Type> GetAllInterfaces(
      this IEnumerable<Type> types,
      Type baseType = null,
      bool? isGenericDefinition = DefaultIsGenericDefinitionClass,
      bool skipOnError = DefaultSkipOnError)
    {
      if (types == null) return CommonTypes.EmptyArrayOfType;

      types = types.Where(c => c.IsInterface);

      if (isGenericDefinition != null)
        if (isGenericDefinition.Value)
          types = types.Where(c => c.IsGenericTypeDefinition);
        else
          types = types.Where(c => !c.IsGenericTypeDefinition);

      if (baseType != null)
        types = types.Where(c => baseType.IsAssignableFrom(c));

#if TRACE
      types = types.ToList();
#endif

      return types;
    }
    public static IEnumerable<Type> GetAllInterfaces<TBase>(
      this IEnumerable<Type> types,
      bool? isGenericDefinition = DefaultIsGenericDefinitionClass,
      bool skipOnError = DefaultSkipOnError)
    {
      return types.GetAllInterfaces(typeof(TBase), isGenericDefinition, skipOnError);
    }
    #endregion

    #region [ GetExtensionsOf ]
    public static IEnumerable<Type> GetExtensionsOf<TInterface>(this Type classType, Type withAttributeType = null)
    {
      return classType.GetExtensionsOf(typeof(TInterface), withAttributeType);
    }
    public static IEnumerable<Type> GetExtensionsOf<TInterface, TAttribute>(this Type classType) where TAttribute : Attribute
    {
      return classType.GetExtensionsOf(typeof(TInterface), typeof(TAttribute));
    }
    public static IEnumerable<Type> GetExtensionsOf(this Type classType, Type interfaceType, Type withAttributeType = null)
    {
      classType.RequireNotNull("classType");
      interfaceType.RequireNotNull("interfaceType");
      Require.Condition(classType.IsClass, "classType");
      Require.Condition(interfaceType.IsInterface, "interfaceType");
      if (withAttributeType != null)
        Require.Condition(CommonTypes.TypeOfAttribute.IsAssignableFrom(withAttributeType), "withAttributeType");

      var result = classType
        .GetInterfaces()
        .Where(t => 
          interfaceType != t && 
          interfaceType.IsAssignableFrom(t) && 
          !t.IsGenericTypeDefinition &&
          (withAttributeType == null || t.HasAttribute(withAttributeType)));
#if TRACE
      result = result.ToList();
#endif
      return result;
    }
    #endregion

    #region [ WithAttribute ]
    public static IEnumerable<Type> WithAttribute<TAttribute>(this IEnumerable<Type> types)
      where TAttribute : Attribute
    {
      return types.WithAttribute(typeof(TAttribute));
    }
    public static IEnumerable<Type> WithAttribute(this IEnumerable<Type> types, Type attributeType)
    {
      if (types == null) return Enumerable.Empty<Type>();

      return types.Where(t => t.HasAttribute(attributeType));
    }
    #endregion
  }
}
