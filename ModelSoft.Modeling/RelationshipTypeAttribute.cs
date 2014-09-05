using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling
{
  public enum ERelationshipType
  {
    Value       = 0,
    Association = 1,
    Content     = 2,
    Container   = 3,
  }

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class RelationshipTypeAttribute : Attribute
  {
    public RelationshipTypeAttribute(ERelationshipType type = ERelationshipType.Value)
    {
      this.Type = type;
    }

    public ERelationshipType Type { get; set; }
  }
}
