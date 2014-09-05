using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class FieldManager
  {
    private ObjectManager owner;
    private IPropertyDescription property;
    private bool isSet;
    private object value;

    internal FieldManager(ObjectManager owner, IPropertyDescription property)
    {
      owner.RequireNotNull("owner");
      property.RequireNotNull("property");

      this.owner = owner;
      SetProperty(property);
    }

    public ObjectManager Owner
    {
      get { return owner; }
    }

    public IPropertyDescription Property
    {
      get { return property; }
    }

    public bool IsSet
    {
      get { return isSet; }
    }

    public object Value
    {
      get 
      {
        if (!isSet)
          return property.DefaultValue;
        return value; 
      }
      set 
      {
        if (value == null)
        {
          this.value = null;
          isSet = false;
        }
        else
        {
          if (!Property.DataType.IsConformant(value))
            throw new ArgumentOutOfRangeException(FormattedResources.ExMsg_ValueIsNotConformantWithDataType(value, Property.DataType.Name));
          this.value = value;
          isSet = true;
        }
      }
    }

    internal void SetProperty(IPropertyDescription property)
    {
      property.RequireNotNull("property");

      this.property = property;
    }

    public void Unset()
    {
      isSet = false;
      value = property.DefaultValue;
    }
  }
}
