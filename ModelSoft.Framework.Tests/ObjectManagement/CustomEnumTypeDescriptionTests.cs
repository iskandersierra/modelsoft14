using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.ObjectManagement;

namespace ModelSoft.Framework.Tests.ObjectManagement
{
  [TestClass]
  public class CustomEnumTypeDescriptionTests
  {
    [TestMethod]
    public void CustomEnumTypeDescription_AutoNumbering()
    {
      var enumType = new CustomEnumTypeDescription(
        "DayOfWeekId", "DayOfWeek",
        new CustomEnumLiteralDescription("Monday", "Monday"),
        new CustomEnumLiteralDescription("Tuesday", "Tuesday"),
        new CustomEnumLiteralDescription("Wednesday", "Wednesday"),
        new CustomEnumLiteralDescription("Thursday", "Thursday"),
        new CustomEnumLiteralDescription("Friday", "Friday"),
        new CustomEnumLiteralDescription("Saturday", "Saturday"),
        new CustomEnumLiteralDescription("Sunday", "Sunday")
      ).AsFrozen();

      Assert.AreEqual("DayOfWeekId", enumType.Identifier);
      Assert.AreEqual("DayOfWeek", enumType.Name);
      Assert.AreEqual(CustomCLRTypes.Int32, enumType.BaseType);
      Assert.AreEqual(7, enumType.Literals.Count);
      for (int i = 0; i < 7; i++)
      {
        Assert.AreEqual(i, enumType.Literals[i].BaseValue);
      }

      Assert.AreEqual("Thursday", enumType.ToString(3));
      Assert.AreEqual("Thursday", enumType.SerializeValueToString(3));
      Assert.AreEqual(5, enumType.DeserializeValueFromString("Saturday"));

      var serializedValue = enumType.SerializeValueToBinary(6);
      Assert.IsNotNull(serializedValue);
      Assert.AreEqual(4, serializedValue.Length);
      Assert.AreEqual(6, enumType.DeserializeValueFromBinary(serializedValue));
    }
  }
}
