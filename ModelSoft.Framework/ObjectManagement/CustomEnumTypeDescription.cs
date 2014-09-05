using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public class CustomEnumTypeDescription : 
    CustomPrimitiveTypeDescription,
    IEnumTypeDescription
  {
    public CustomEnumTypeDescription()
    {
    }

    public CustomEnumTypeDescription(string _Identifier, string _Name, 
      params CustomEnumLiteralDescription[] _Literals)
      : base(_Identifier, _Name)
    {
      if (_Literals != null)
        _Literals.ForEach(e => Literals.Add(e));
    }

    private IPrimitiveTypeDescription baseType;
    public IPrimitiveTypeDescription BaseType
    {
      get { return baseType; }
      set 
      {
        CheckModifyFrozen();
        baseType = value; 
      }
    }

    private EnumLiteralDescriptionCollection literals;
    public IEnumLiteralDescriptionCollection Literals
    {
      get { return literals ?? (literals = new EnumLiteralDescriptionCollection(this, "Literals")); }
    }

    public override object DefaultValue
    {
      get 
      {
        if (BaseType.SupportsCompare)
        {
          var minLiteral = Literals.First();
          foreach (var literal in literals.Skip(1))
          {
            if (BaseType.Compare(minLiteral.BaseValue, literal.BaseValue) > 0)
            {
              minLiteral = literal;
            }
          }
          return minLiteral.BaseValue;
        }
        else
        {
          return Literals.First().BaseValue;
        }
      }
    }

    public override bool IsConformant(object value)
    {
      return BaseType.IsConformant(value);
    }

    public override bool SupportsStringSerialization { get { return true; } }
    
    public override bool SupportsBinarySerialization { get { return BaseType.SupportsBinarySerialization; } }

    public override bool SupportsIncrementDecrement { get { return false; } }

    public override bool SupportsCompare { get { return BaseType.SupportsCompare; } }

    public override string SerializeValueToString(object value)
    {
      var literal = Literals
        .Where(e => BaseType.AreEquals(e.BaseValue, value))
        .FirstOrDefault();
      if (literal != null)
        return literal.Name;
      return BaseType.SerializeValueToString(value);
    }

    public override object DeserializeValueFromString(string serializedValue)
    {
      var literal = Literals
        .Where(e => e.Name == serializedValue)
        .FirstOrDefault();
      if (literal != null)
        return literal.BaseValue;
      return BaseType.DeserializeValueFromString(serializedValue);
    }

    public override byte[] SerializeValueToBinary(object value)
    {
      if (!SupportsBinarySerialization)
        return base.SerializeValueToBinary(value);
      return BaseType.SerializeValueToBinary(value);
    }

    public override object DeserializeValueFromBinary(byte[] serializedValue)
    {
      if (!SupportsBinarySerialization)
        return base.DeserializeValueFromBinary(serializedValue);
      return BaseType.DeserializeValueFromBinary(serializedValue);
    }

    public override bool AreEquals(object value1, object value2)
    {
      return BaseType.AreEquals(value1, value2);
    }

    public override int GetHashCode()
    {
      return BaseType.GetHashCode();
    }

    public override string ToString(object value)
    {
      return SerializeValueToString(value);
    }

    public override int Compare(object value1, object value2)
    {
      if (!SupportsCompare)
        return base.Compare(value1, value2);
      return BaseType.Compare(value1, value2);
    }

    public override string ToString()
    {
      return string.Format("enum {0}: {1} {{{2}}}", base.ToString(), baseType == null ? "Int32" : baseType.ToString(), Literals.ToStringList(", "));
    }

    protected override void OnFreeze()
    {
      if (!Literals.Any())
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_EnumTypeRequiresAtLeastOneLiteral(this.ToString()));
      //Literals.CheckDuplicatedIdentifiersInCollection("Literals");
      //Literals.CheckDuplicatedNamesInCollection("Literals");

      if (BaseType == null)
        BaseType = CustomCLRTypes.Int32;

      if (BaseType is IEnumTypeDescription)
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_EnumTypeCannotHaveTypeAsBaseType(this.ToString(), BaseType.ToString()));

      foreach (var item in Literals.Where(e => e.BaseValue != null && !baseType.IsConformant(e.BaseValue)))
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_WrongEnumLiteralValue(item.BaseValue.ToString(), item.Identifier));

      if (!BaseType.SupportsIncrementDecrement)
      {
        foreach (var item in Literals.Where(l => l.BaseValue == null))
          throw new InvalidFreezingOperationException(FormattedResources.ExMsg_MissingEnumLiteralValue(item.Identifier));
      }
      else
      {
        object startValue = BaseType.DefaultValue;
        foreach (var item in Literals.Cast<CustomEnumLiteralDescription>().Where(l => l.BaseValue == null))
        {
          while (Literals.Any(e => e.BaseValue != null && baseType.AreEquals(e.BaseValue, startValue)))
            startValue = baseType.Increment(startValue);
          item.BaseValue = startValue;
        }
      }

      BaseType.Freeze();
      Literals.Freeze();

      base.OnFreeze();
    }

    class EnumLiteralDescriptionCollection : 
      CustomOwnedNamedDescriptionCollection<IEnumLiteralDescription, CustomEnumLiteralDescription, CustomEnumTypeDescription>,
      IEnumLiteralDescriptionCollection
    {
      public EnumLiteralDescriptionCollection(CustomEnumTypeDescription _Owner, string _CollectionName)
        : base(_Owner, _CollectionName)
      {
      
      }
    }
  }
}
