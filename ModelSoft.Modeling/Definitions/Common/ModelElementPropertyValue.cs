using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework;

namespace ModelSoft.Modeling.Definitions.Common
{
  [DebuggerDisplay("{PropertyName} = {Value}")]
  public sealed class ModelElementPropertyValue
  {
    public ModelElementPropertyValue(PropertyInfo _Property, object _Value, ERelationshipType _RelationshipType)
    {
      _Property.RequireNotNull("_Property");
      this.Property = _Property;
      this.PropertyName = _Property.Name;
      this.Value = _Value;
      this.RelationshipType = RelationshipType;
    }

    public ModelElementPropertyValue(string _PropertyName, object _Value, ERelationshipType _RelationshipType)
    {
      _PropertyName.RequireNotWhitespace("_PropertyName");
      this.PropertyName = _PropertyName;
      this.Value = _Value;
      this.RelationshipType = RelationshipType;
    }

    public PropertyInfo Property { get; private set; }

    public string PropertyName { get; private set; }

    public object Value { get; private set; }

    public IEnumerable<IModelElement> ModelValues
    {
      get
      {
        var result = Value is IEnumerable<IModelElement> ? ((IEnumerable<IModelElement>)Value) : (Value as IModelElement).Singleton();
        result = result.NotNull();

        return result;
      }
    }

    public ERelationshipType RelationshipType { get; set; }
  }
}
