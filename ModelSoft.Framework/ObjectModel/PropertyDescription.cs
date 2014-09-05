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
  public abstract class PropertyDescription<T> :
    FreezableBase,
    IPropertyDescription<T>,
    IPropertyDescriptionImpl
  {
    private static readonly Type TypeOfT = typeof(T);
    private readonly bool isReadOnly;
    private readonly string name;
    private readonly string resourceName;
    private readonly Func<string, string> friendlyNameFunc;
    private readonly T defaultValue;
    private readonly bool hasDefaultValue;
    private IClassDescription declaringClass;
    private Action<PropertyDescriptionComputeContext> computeValue;
    private Action<PropertyDescriptionCoerceContext> coerceValue;
    private Action<PropertyDescriptionChangeValueContext> changeValue;
    private Action<PropertyDescriptionChangingValueContext> changingValue;
    private IPropertyDescription<T> asReadOnlyProperty;

    protected internal PropertyDescription(PropertyDescriptionSetup setup = null)
    {
      this.isReadOnly = false;
      if (setup != null)
      {
        if (setup.HasDefaultValue)
        {
          (setup.DefaultValue is T).RequireCondition("DefaultValue", @"Default value must be a value of type ""{0}""".Fmt(typeof(T).FullName));
          this.defaultValue = (T)setup.DefaultValue;
          this.hasDefaultValue = true;
        }
        
        if (setup.ComputeValue != null)
          this.computeValue = setup.ComputeValue;
        
        if (setup.CoerceValue != null)
          this.coerceValue = setup.CoerceValue;

        if (setup.ChangeValue != null)
          this.changeValue = setup.ChangeValue;

        if (setup.ChangingValue != null)
          this.changingValue = setup.ChangingValue;
      }
    }

    protected internal PropertyDescription(PropertyDescription<T> property)
    {
      property.RequireNotNull("property");

      this.isReadOnly = true;
      this.defaultValue = property.DefaultValue;
      this.hasDefaultValue = property.HasDefaultValue;
      this.coerceValue = property.coerceValue;
      this.computeValue = property.computeValue;
      this.changeValue = property.changeValue;
      this.changingValue = property.changingValue;
      this.declaringClass = property.declaringClass;
    }

    public string Name
    {
      get { return name; }
    }

    public string ResourceName
    {
      get { return resourceName; }
    }

    public string FriendlyName
    {
      get { return friendlyNameFunc(resourceName); }
    }

    public bool IsReadOnly { get { return isReadOnly; } }

    public abstract Type PropertyType { get; }

    public T DefaultValue { get { return defaultValue; } }

    public bool HasDefaultValue { get { return hasDefaultValue; } }

    public void ComputeValue(PropertyDescriptionComputeContext context)
    {
      if (computeValue == null)
        throw new InvalidOperationException("This property is not computed");
      computeValue(context);
    }

    public bool HasComputeValue { get { return computeValue != null; } }

    public void CoerceValue(PropertyDescriptionCoerceContext context)
    {
      if (coerceValue == null)
        context.SetCoercedValue(context.Value);
      else
        coerceValue(context);
    }

    public void ChangeValue(PropertyDescriptionChangeValueContext context)
    {
      if (changeValue != null)
        changeValue(context);
    }

    public void ChangingValue(PropertyDescriptionChangingValueContext context)
    {
      if (changingValue == null)
        context.AllowChange = true;
      else
        changingValue(context);
    }

    public IPropertyDescription<T> AsReadOnlyProperty
    {
      get
      {
        if (IsReadOnly) return this;
        if (asReadOnlyProperty == null)
        {
          asReadOnlyProperty = CreateReadOnlyProperty();
        }
        return asReadOnlyProperty;
      }
    }

    protected abstract PropertyDescription<T> CreateReadOnlyProperty();

    public T GetValue(object instance)
    {
      instance.RequireNotNull("instance");

      var result = GetValueOverride(instance);

      return result;
    }

    public void SetValue(object instance, T value)
    {
      instance.RequireNotNull("instance");

      SetValueOverride(instance, value);
    }

    protected abstract T GetValueOverride(object instance);

    protected abstract void SetValueOverride(object instance, T value);

    public override bool Equals(object obj)
    {
      return object.ReferenceEquals(this, obj);
    }

    public override int GetHashCode()
    {
      return RuntimeHelpers.GetHashCode(this);
    }

    public override string ToString()
    {
      if (declaringClass != null)
        return @"{0}.{1}".Fmt(declaringClass, Name);
      return Name;
    }

    protected override void OnFreeze()
    {
      if (declaringClass == null)
        throw new InvalidFreezingOperationException(@"Cannot freeze property ""{0}"" without a declaring class".Fmt(this.Name));
      base.OnFreeze();
    }

    void IPropertyDescriptionImpl.SetDeclaringClass(IClassDescription declaringClass)
    {
      declaringClass.RequireNotNull("declaringClass");
      if (this.declaringClass != null && declaringClass == this.declaringClass) return;
      CheckModifyFrozen();
      this.declaringClass = declaringClass;
    }

    object IPropertyDescription.DefaultValue
    {
      get { return this.DefaultValue; }
    }

    IPropertyDescription IPropertyDescription.AsReadOnlyProperty
    {
      get { return this.AsReadOnlyProperty; }
    }


    object IPropertyDescription.GetValue(object instance)
    {
      return this.GetValue(instance);
    }

    void IPropertyDescription.SetValue(object instance, object value)
    {
      if (value == null && TypeOfT.IsValueType)
        throw new ArgumentNullException("value", @"Cannot set null to property ""{0}""".Fmt(this.Name));
      if (value == null)
        this.SetValue(instance, default(T));
      else
        this.SetValue(instance, (T)value);
    }
  }

}
