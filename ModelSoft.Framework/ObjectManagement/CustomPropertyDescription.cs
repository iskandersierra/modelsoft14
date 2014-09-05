using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class CustomPropertyDescription : 
    CustomNamedDescription, 
    IPropertyDescription,
    IDescriptionWithParent
  {
    public CustomPropertyDescription()
    {
    }

    public CustomPropertyDescription(string _Identifier, string _Name, IDataTypeDescription _DataType)
      : base(_Identifier, _Name)
    {
      this.DataType = _DataType;
    }

    public CustomPropertyDescription(string _Identifier, string _Name, IDataTypeDescription _DataType, object _DefaultValue)
      : base(_Identifier, _Name)
    {
      this.DataType = _DataType;
      this.DefaultValue = _DefaultValue;
    }

    public CustomPropertyDescription(string _Identifier, string _Name, IDataTypeDescription _DataType, Func<ObjectManager, object> _ComputeValueEvaluator)
      : base(_Identifier, _Name)
    {
      this.DataType = _DataType;
      this.ComputeValueEvaluator = _ComputeValueEvaluator;
    }

    public IClassDescription DeclaringClass
    {
      get { return ((IDescriptionWithParent)this).Parent as IClassDescription; }
    }

    private IDataTypeDescription dataType;
    public IDataTypeDescription DataType
    {
      get { return dataType; }
      set 
      {
        CheckModifyFrozen();
        dataType = value; 
      }
    }

    private PropertyKind kind = PropertyKind.Default;
    public PropertyKind Kind
    {
      get { return kind; }
      set 
      {
        CheckModifyFrozen();
        kind = value; 
      }
    }

    private PropertyMultiplicity multiplicity = PropertyMultiplicity.Single;
    public PropertyMultiplicity Multiplicity
    {
      get { return multiplicity; }
      set 
      {
        CheckModifyFrozen();
        multiplicity = value; 
      }
    }

    public bool HasDefaultValue
    {
      get { return defaultValue != null; }
    }

    private object defaultValue;
    public object DefaultValue
    {
      get 
      {
        if (defaultValue == null)
          return DataType.DefaultValue;
        return defaultValue; 
      }
      set 
      {
        CheckModifyFrozen();
        defaultValue = value; 
      }
    }

    public bool HasComputedValue
    {
      get { return computeValueEvaluator != null; }
    }

    private Func<ObjectManager, object> computeValueEvaluator;
    public Func<ObjectManager, object> ComputeValueEvaluator
    {
      get { return computeValueEvaluator; }
      set 
      {
        CheckModifyFrozen();
        computeValueEvaluator = value; 
      }
    }

    public object ComputeValue(ObjectManager instance)
    {
      if (computeValueEvaluator == null)
        throw new InvalidOperationException("Property is not computable");
      if (instance == null)
        throw new ArgumentNullException("instance");
      return computeValueEvaluator(instance);
    }

    protected override void OnFreeze()
    {
      if (dataType == null)
        throw new InvalidFreezingOperationException("Property data type cannot be null: " + this.ToString());

      if (defaultValue != null && !DataType.IsConformant(defaultValue))
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_ValueIsNotConformantWithDataType(defaultValue, dataType.ToString()));

        if (dataType is IFreezable)
          ((IFreezable)dataType).Freeze();

      base.OnFreeze();
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.Append("property ").Append(base.ToString());
      if (dataType != null)
        sb.Append(": ").Append(dataType.ToString());
      return base.ToString();
    }

    object IDescriptionWithParent.Parent { get; set; }

    object IDescriptionWithParent.ParentCollection { get; set; }
  }
}
