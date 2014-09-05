using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class ObjectManager :
    IObjectManagerContainer
  {
    private Dictionary<IPropertyDescription, FieldManager> fields;
    private IObjectManagerHost host;
    private IClassDescription metaClass;
    private IObjectManagerContainer parentObject;
    private IObjectManagerCollection containerCollection;

    public ObjectManager(IObjectManagerHost host = null, IClassDescription metaClass = null)
    {
      this.host = host;

      fields = new Dictionary<IPropertyDescription, FieldManager>();
      SetMetaClass(metaClass);
    }

    [KeywordAccess("host")]
    public IObjectManagerHost Host
    {
      get { return host; }
    }

    [KeywordAccess("class")]
    public IClassDescription MetaClass
    {
      get { return metaClass; }
    }

    [KeywordAccess("parent")]
    public IObjectManagerContainer ParentObject
    {
      get { return parentObject; }
      internal set 
      { 
        parentObject = value; 
      }
    }

    public IObjectManagerCollection ContainerCollection
    {
      get { return containerCollection; }
      set { containerCollection = value; }
    }

    public IEnumerable<FieldManager> Fields
    {
      get
      {
        return fields.Values.AsEnumerable();
      }
    }

    public bool HasField(IPropertyDescription property)
    {
      property.RequireNotNull("property");

      return fields.ContainsKey(property);
    }

    public FieldManager Field(IPropertyDescription property)
    {
      property.RequireNotNull("property");

      FieldManager field;
      if (fields.TryGetValue(property, out field))
        return field;
      throw new ArgumentOutOfRangeException("Property {0} not found.".Fmt(property));
    }

    private void SetMetaClass(IClassDescription metaClass)
    {
      if (metaClass == null)
      {
        fields.Clear();
      }
      else
      {
        if (!metaClass.IsFrozen)
          throw new InvalidFreezingOperationException(FormattedResources.ExMsg_MetaClassMustBeFrozen(metaClass.ToString()));

        foreach (var field in fields.Values.ToArray())
        {
          var name = field.Property.Name;
          if (!metaClass.Properties.Any(p => p.Name == name))
            fields.Remove(field.Property);
        }

        foreach (var property in metaClass.Properties)
        {
          FieldManager field;
          var name = property.Name;
          if (!fields.TryGetValue(property, out field))
          {
            field = CreateFieldManager(property);
            fields.Add(property, field);
          }
          else
          {
            field.SetProperty(property);
          }
        }
      }
      this.metaClass = metaClass;
    }

    private FieldManager CreateFieldManager(IPropertyDescription property)
    {
      var field = new FieldManager(this, property);

      return field;
    }

    ObjectManager IObjectManagerContainer.ObjectManager
    {
      get { return this; }
    }
  }
}
