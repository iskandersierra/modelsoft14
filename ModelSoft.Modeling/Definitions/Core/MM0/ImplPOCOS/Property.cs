using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Modeling.Definitions.Common;
using ModelSoft.Modeling.Definitions.Common.ImplPOCOS;
using ModelSoft.Modeling.Definitions.Core.Expressions;

namespace ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS
{
  public class Property :
    ComplexTypeMember,
    IProperty
  {
    public Property()
    {
      RelationshipType = RelationshipType.Value;
      IsRequired = true;
      IsReadOnly = false;
    }

    public RelationshipType RelationshipType { get; set; }

    public bool IsRequired { get; set; }
    
    public bool IsReadOnly { get; set; }

    [RelationshipType(ERelationshipType.Association)]
    public IProperty Opposite { get; set; }

    [IsComputed]
    [RelationshipType(ERelationshipType.Container)]
    public IANamespace ParentNamespace
    {
      get { return this.GetParentNamespaceEx(); }
    }

    [IsComputed]
    public string FullName
    {
      get { return this.GetFullNameEx(); }
    }

    [RelationshipType(ERelationshipType.Container)]
    public ITypedExpression ComputedValue
    {
      get { return _ComputedValue; }
      set 
      {
        if (_ComputedValue == value) return;
        if (!(value is ModelElement))
          throw new ArgumentException();
        if (_ComputedValue != null)
        {

        }
        _ComputedValue = value; 
      }
    }
    private ITypedExpression _ComputedValue;

  }
}
