using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.DomainObjects.Properties;
using ModelSoft.Framework.Logging;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.DomainObjects
{
    //[LocalizedDisplayName("ModelElement_DisplayName")]
    //public abstract class ModelElementBase : IModelElement
    //{
    //    static ModelElementBase()
    //    {
    //        ModelClasses = new ConcurrentDictionary<Type, ModelClass>();
    //        ContainerModelPropertyKey = RegisterContainerProperty<ModelElementBase, IModelElement, IModelElement>("Container");
    //        ContainerModelProperty = RegisterReadOnlyProperty(ContainerModelPropertyKey);
    //    }

    //    #region [ Registers ]

    //    private static readonly ConcurrentDictionary<Type, ModelClass> ModelClasses;

    //    public static IEnumerable<IModelClass> GetRegisteredClasses()
    //    {
    //        return ModelClasses.Values.ToArray();
    //    }

    //    #region [ RegisterProperty ]
    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        [NotNull] Expression<Func<TInterface, TProperty>> propertyAccessor)
    //        where TInterface : IModelElement
    //        where TElement : ModelElementBase, TInterface
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        var displayName = GetDisplayNameFunc(property);

    //        var description = GetDescriptionFunc(property);

    //        var category = GetCategoryFunc(property);

    //        var defaultValue = GetPropertyDefaultValue(property);

    //        if (defaultValue != null)
    //            return RegisterProperty<TElement, TProperty>(property.Name, displayName, (TProperty)defaultValue, description, category);
    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName, description, category);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor, string displayName, string description = "", string category = "")
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        var defaultValue = GetPropertyDefaultValue(property);

    //        if (defaultValue != null)
    //            return RegisterProperty<TElement, TProperty>(property.Name, displayName, (TProperty)defaultValue, description, category);
    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName, description, category);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor, Func<CultureInfo, string> displayName, Func<CultureInfo, string> description = null, Func<CultureInfo, string> category = null)
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        var defaultValue = GetPropertyDefaultValue(property);

    //        if (defaultValue != null)
    //            return RegisterProperty<TElement, TProperty>(property.Name, displayName, (TProperty)defaultValue, description, category);
    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor, TProperty defaultValue)
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        var displayName = GetDisplayNameFunc(property);

    //        var description = GetDescriptionFunc(property);

    //        var category = GetCategoryFunc(property);

    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName, defaultValue, description, category);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor, string displayName, TProperty defaultValue, string description = "", string category = "")
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName, defaultValue, description, category);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor, Func<CultureInfo, string> displayName, TProperty defaultValue)
    //    {
    //        if (propertyAccessor == null) throw new ArgumentNullException("propertyAccessor");

    //        var property = propertyAccessor.GetPropertyInfo();

    //        return RegisterProperty<TElement, TProperty>(property.Name, displayName, defaultValue);
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TProperty>(
    //        string propertyName, string displayName, string description = "", string category = "")
    //    {
    //        return (IModelProperty<TProperty>) RegisterProperty<TElement>(propertyName,
    //            () => new ModelProperty<TProperty>(propertyName, typeof (TProperty), displayName, description, category, null, null, false));
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TProperty>(
    //        string propertyName, string displayName, TProperty defaultValue, string description = "", string category = "")
    //    {
    //        return (IModelProperty<TProperty>)RegisterProperty<TElement>(propertyName,
    //            () => new ModelProperty<TProperty>(propertyName, typeof(TProperty), displayName, description, category, defaultValue, null, false));
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TProperty>(
    //        string propertyName, Func<CultureInfo, string> displayName, Func<CultureInfo, string> description = null, Func<CultureInfo, string> category = null)
    //    {
    //        return (IModelProperty<TProperty>)RegisterProperty<TElement>(propertyName,
    //            () => new ModelProperty<TProperty>(propertyName, typeof(TProperty), displayName, description, category, null, null, false));
    //    }

    //    protected static IModelProperty<TProperty> RegisterProperty<TElement, TProperty>(
    //        string propertyName, Func<CultureInfo, string> displayName, TProperty defaultValue, Func<CultureInfo, string> description = null, Func<CultureInfo, string> category = null)
    //    {
    //        return (IModelProperty<TProperty>)RegisterProperty<TElement>(propertyName,
    //            () => new ModelProperty<TProperty>(propertyName, typeof(TProperty), displayName, description, category, defaultValue, null, false));
    //    }

    //    #endregion

    //    #region [ RegisterComputedProperty ]

    //    protected static IModelProperty<TProperty> RegisterComputedProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor,
    //        Func<TElement, TProperty> computeProperty)
    //    {
    //        return null;
    //    }

    //    #endregion

    //    #region [ RegisterContainerProperty ]

    //    protected static IModelProperty<TProperty> RegisterContainerProperty<TElement, TInterface, TProperty>(
    //        string propertyName)
    //    {
    //        return null;
    //    }

    //    protected static IModelProperty<TProperty> RegisterContainerProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor)
    //    {
    //        return null;
    //    }

    //    #endregion

    //    #region [ RegisterChildProperty ]

    //    protected static IModelProperty<TProperty> RegisterChildProperty<TElement, TInterface, TProperty>(
    //        string propertyName)
    //    {
    //        return null;
    //    }

    //    protected static IModelProperty<TProperty> RegisterChildProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, TProperty>> propertyAccessor)
    //    {
    //        return null;
    //    }

    //    #endregion

    //    #region [ RegisterChildrenProperty ]

    //    protected static IModelProperty<IList<TProperty>> RegisterChildrenProperty<TElement, TInterface, TProperty>(
    //        string propertyName)
    //    {
    //        return null;
    //    }

    //    protected static IModelProperty<IList<TProperty>> RegisterChildrenProperty<TElement, TInterface, TProperty>(
    //        Expression<Func<TInterface, IList<TProperty>>> propertyAccessor)
    //    {
    //        return null;
    //    }

    //    protected static IModelProperty<IIndexedList<TKey, TProperty>> RegisterChildrenProperty
    //        <TElement, TInterface, TKey, TProperty>(
    //        string propertyName)
    //    {
    //        return null;
    //    }

    //    protected static IModelProperty<IIndexedList<TKey, TProperty>> RegisterChildrenProperty
    //        <TElement, TInterface, TKey, TProperty>(
    //        Expression<Func<TInterface, IIndexedList<TKey, TProperty>>> propertyAccessor)
    //    {
    //        return null;
    //    }

    //    #endregion

    //    protected static IModelProperty<TProperty> RegisterReadOnlyProperty<TProperty>(IModelProperty<TProperty> privateProperty)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #region [ Internals ]

    //    private static IModelProperty RegisterProperty<TElement>(string propertyName,
    //        Func<ModelProperty> propertyCreator)
    //    {
    //        return RegisterProperty(propertyName, typeof (TElement), propertyCreator);
    //    }
        
    //    [LogTrace(LoggingType.Enter)]
    //    private static IModelProperty RegisterProperty(string propertyName, 
    //        Type elementType,
    //        Func<ModelProperty> propertyCreator)
    //    {
    //        var modelClass = GetModelClass(elementType);
    //        var property = propertyCreator();
    //        modelClass.Properties.Add(property);
    //        return property;
    //    }

    //    private static ModelClass GetModelClass(Type elementType)
    //    {
    //        return ModelClasses.GetOrAdd(elementType, CreateModelClass);
    //    }

    //    [LogTrace(LoggingType.Enter)]
    //    private static ModelClass CreateModelClass(Type elementType)
    //    {
    //        if (!typeof(ModelElementBase).IsAssignableFrom(elementType))
    //            throw new ArgumentException(string.Format(Resources.CannotCreateModelClass, elementType.FullName), "elementType");

    //        ModelClass baseModelClass = null;
    //        if (elementType != typeof (ModelElementBase))
    //            baseModelClass = GetModelClass(elementType.BaseType);

    //        var modelClass = new ModelClass(elementType, GetDisplayNameFunc(elementType), baseModelClass, GetDescriptionFunc(elementType), GetCategoryFunc(elementType));
    //        return modelClass;
    //    }

    //    private static Func<CultureInfo, string> GetDisplayNameFunc(ICustomAttributeProvider attrProv)
    //    {
    //        Func<CultureInfo, string> displayName = null;
    //        if (attrProv.HasLocalizedValue<LocalizedDisplayNameAttribute>())
    //            displayName = attrProv.GetLocalizedString<LocalizedDisplayNameAttribute>;
    //        return displayName;
    //    }

    //    private static Func<CultureInfo, string> GetDescriptionFunc(ICustomAttributeProvider attrProv)
    //    {
    //        Func<CultureInfo, string> description = null;
    //        if (attrProv.HasLocalizedValue<LocalizedDescriptionAttribute>())
    //            description = attrProv.GetLocalizedString<LocalizedDescriptionAttribute>;
    //        return description;
    //    }

    //    private static Func<CultureInfo, string> GetCategoryFunc(ICustomAttributeProvider attrProv)
    //    {
    //        Func<CultureInfo, string> category = null;
    //        if (attrProv.HasLocalizedValue<LocalizedCategoryAttribute>())
    //            category = attrProv.GetLocalizedString<LocalizedCategoryAttribute>;
    //        return category;
    //    }

    //    private static object GetPropertyDefaultValue(PropertyInfo property)
    //    {
    //        object defaultValue = null;
    //        var defAttr = property.GetAttribute<DefaultValueAttribute>();
    //        if (defAttr != null)
    //        {
    //            if (defAttr.Value != null && property.PropertyType.IsInstanceOfType(defAttr.Value))
    //            {
    //                defaultValue = defAttr.Value;
    //            }
    //            else if (defAttr.Value != null && property.DeclaringType != null)
    //            {
    //                var properties = TypeDescriptor.GetProperties(property.DeclaringType);
    //                var propDesc = properties.Find(property.Name, false);
    //                if (propDesc != null)
    //                {
    //                    var typeConverter = propDesc.Converter;
    //                    if (typeConverter != null && typeConverter.CanConvertFrom(defAttr.Value.GetType()))
    //                    {
    //                        defaultValue = typeConverter.ConvertFrom(defAttr.Value);
    //                    }
    //                }
    //            }
    //        }
    //        return defaultValue;
    //    }

    //    #endregion

    //    #endregion

    //    protected T GetValue<T>(IModelProperty<T> modelProperty)
    //    {
    //        return default(T);
    //    }

    //    protected void SetValue<T>(IModelProperty<T> modelProperty, T value)
    //    {
            
    //    }

    //    protected bool IsSetValue(IModelProperty modelProperty)
    //    {
    //        return false;
    //    }

    //    protected void ResetValue(IModelProperty modelProperty)
    //    {
    //    }

    //    private static readonly IModelProperty<IModelElement> ContainerModelPropertyKey;
    //    public static readonly IModelProperty<IModelElement> ContainerModelProperty;
    //    [Container, LocalizedDisplayName("ModelElement_Container_DisplayName")]
    //    public IModelElement Container
    //    {
    //        get
    //        {
    //            return GetValue(ContainerModelProperty);
    //        }
    //        internal set
    //        {
    //            SetValue(ContainerModelPropertyKey, value);
    //        }
    //    }
    //}
}