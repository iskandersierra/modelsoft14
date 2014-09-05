using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class CustomClassDescription :
    CustomDataTypeDescription,
    IClassDescription
  {
    public CustomClassDescription()
    {
    }

    public CustomClassDescription(
      string _Identifier,
      string _Name,
      params CustomPropertyDescription[] _Properties)
      : base(_Identifier, _Name)
    {
      if (_Properties != null)
        foreach (var item in _Properties)
        {
          Properties.Add(item);
        }
    }

    public CustomClassDescription(
      string _Identifier,
      string _Name,
      CustomClassDescription[] _BaseClasses,
      params CustomPropertyDescription[] _Properties)
      : this(_Identifier, _Name, _Properties)
    {
      if (_BaseClasses != null)
        foreach (var item in _BaseClasses)
        {
          BaseClasses.Add(item);
        }
    }

    public override object DefaultValue { get { return null; } }

    CustomPropertyDescriptionCollection properties;
    public IPropertyDescriptionCollection Properties
    {
      get
      {
        return properties ?? (properties = new CustomPropertyDescriptionCollection(this, "Properties"));
      }
    }

    private CustomClassRefDescriptionCollection baseClasses;
    public IClassDescriptionCollection BaseClasses
    {
      get { return baseClasses ?? (baseClasses = new CustomClassRefDescriptionCollection(this, "BaseClasses")); }
    }

    protected override void OnFreeze()
    {
      //Properties.CheckDuplicatedIdentifiersInCollection("Properties");
      //Properties.CheckDuplicatedNamesInCollection("Properties");

      BaseClasses.CheckDuplicatedObjectInCollection("BaseClasses");
      if (this.CyclesBack<IClassDescription>(e => e.BaseClasses))
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_BaseClassCycle(this.Name));

      Properties.Freeze();
      BaseClasses.Freeze();

      base.OnFreeze();
    }

    public override bool IsConformant(object value)
    {
      if (value is IObjectManagerContainer)
      {
        var om = ((IObjectManagerContainer)value).ObjectManager;
        return om.MetaClass == this || om.MetaClass.BaseClasses.Contains(this);
      }
      return false;
    }

    #region [ Collections ]
    class CustomPropertyDescriptionCollection :
      CustomOwnedNamedDescriptionCollection<IPropertyDescription, CustomPropertyDescription, CustomClassDescription>,
      IPropertyDescriptionCollection
    {
      public CustomPropertyDescriptionCollection(CustomClassDescription _Owner, string _CollectionName)
        : base(_Owner, _CollectionName)
      {
      }
    }
    class CustomClassRefDescriptionCollection :
      CustomReferencedDescriptionCollection<IClassDescription, CustomClassDescription, CustomClassDescription>,
      IClassDescriptionCollection
    {
      public CustomClassRefDescriptionCollection(CustomClassDescription _Owner, string _CollectionName)
        : base(_Owner, _CollectionName)
      {
      }
    }
    #endregion
  }

}
