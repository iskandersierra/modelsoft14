using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.Tests.Reflection
{
  [TestClass]
  public class TypeExtensionsGetHierarchyTests
  {
    Type I1 = typeof(TET_Interface1);
    Type I2 = typeof(TET_Interface2);
    Type I3 = typeof(TET_Interface3);
    Type I11 = typeof(TET_Interface11);
    Type I12 = typeof(TET_Interface12);
    Type I123 = typeof(TET_Interface123);
    Type O = typeof(object);
    Type C1 = typeof(TET_Class1);
    Type C2 = typeof(TET_Class2);
    Type C3 = typeof(TET_Class3);

    Type Str = typeof(string);
    Type Int = typeof(int);
    Type Dbl = typeof(double);
    Type Flt = typeof(float);
    Type Chr = typeof(Char);
    Type DTm = typeof(DateTime);

    [TestMethod]
    public void TypeExtensionsGetHierarchyTests_GetHierarchy()
    {
      CheckGetHierarchy(O,    false, O);
      CheckGetHierarchy(I1,   false, I1);
      CheckGetHierarchy(I11,  false, I11);
      CheckGetHierarchy(I12,  false, I12);
      CheckGetHierarchy(I123, false, I123);
      CheckGetHierarchy(C1,   false, C1, O);
      CheckGetHierarchy(C2,   false, C2, C1, O);
      CheckGetHierarchy(C3,   false, C3, C2, C1, O);

      CheckGetHierarchy(O,    true, O);
      CheckGetHierarchy(I1,   true, I1);
      CheckGetHierarchy(I11,  true, I11, I1);
      CheckGetHierarchy(I12,  true, I12, I1, I2);
      CheckGetHierarchy(I123, true, I123, I12, I1, I2, I3);
      CheckGetHierarchy(C1,   true, C1, O, I1);
      CheckGetHierarchy(C2,   true, C2, C1, O, I11, I1, I12, I2);
      CheckGetHierarchy(C3,   true, C3, C2, C1, O, I123, I12, I1, I2, I3, I11);
    }

    [TestMethod]
    public void TypeExtensionsGetHierarchyTests_GetPropertiesHierarchical()
    {
      CheckGetProperties(O, false);
      CheckGetProperties(I1, false, MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1));
      CheckGetProperties(I11, false, MemberInfo<TET_Interface11>.GetProp(e => e.I11_P11));
      CheckGetProperties(I12, false, MemberInfo<TET_Interface12>.GetProp(e => e.I1_P1), 
                                     MemberInfo<TET_Interface12>.GetProp(e => e.I12_P12));
      CheckGetProperties(I123, false, MemberInfo<TET_Interface123>.GetProp(e => e.I1_P1),
                                      MemberInfo<TET_Interface123>.GetProp(e => e.I123_P123));
      CheckGetProperties(C1, false, MemberInfo<TET_Class1>.GetProp(e => e.I1_P1));
      CheckGetProperties(C2, false, MemberInfo<TET_Class2>.GetProp(e => e.I1_P1), 
                                    MemberInfo<TET_Class2>.GetProp(e => e.I2_P2),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I11_P11),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I12_P12),
                                    MemberInfo<TET_Class1>.GetProp(e => e.I1_P1));
      CheckGetProperties(C3, false, MemberInfo<TET_Class3>.GetProp(e => e.I123_P123),
                                    MemberInfo<TET_Class3>.GetProp(e => e.I1_P1),
                                    MemberInfo<TET_Class3>.GetProp(e => e.I3_P3),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I1_P1),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I2_P2),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I11_P11),
                                    MemberInfo<TET_Class2>.GetProp(e => e.I12_P12),
                                    MemberInfo<TET_Class1>.GetProp(e => e.I1_P1));

      CheckGetProperties(O, true);
      CheckGetProperties(I1, true, MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1));
      CheckGetProperties(I11, true, MemberInfo<TET_Interface11>.GetProp(e => e.I11_P11),
                                    MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1));
      CheckGetProperties(I12, true, MemberInfo<TET_Interface12>.GetProp(e => e.I1_P1),
                                    MemberInfo<TET_Interface12>.GetProp(e => e.I12_P12),
                                    MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1),
                                    MemberInfo<TET_Interface2>.GetProp(e => e.I2_P2));
      CheckGetProperties(I123, true, MemberInfo<TET_Interface123>.GetProp(e => e.I1_P1),
                                     MemberInfo<TET_Interface123>.GetProp(e => e.I123_P123), 
                                     MemberInfo<TET_Interface12>.GetProp(e => e.I1_P1),
                                     MemberInfo<TET_Interface12>.GetProp(e => e.I12_P12),
                                     MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1),
                                     MemberInfo<TET_Interface2>.GetProp(e => e.I2_P2),
                                     MemberInfo<TET_Interface3>.GetProp(e => e.I3_P3));
      CheckGetProperties(C1, true, MemberInfo<TET_Class1>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1));
      CheckGetProperties(C2, true, MemberInfo<TET_Class1>.GetProp(e => e.I1_P1), 
                                   MemberInfo<TET_Class2>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I2_P2),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I11_P11),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I12_P12),
                                   MemberInfo<TET_Interface11>.GetProp(e => e.I11_P11),
                                   MemberInfo<TET_Interface12>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface12>.GetProp(e => e.I12_P12),
                                   MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface2>.GetProp(e => e.I2_P2));
      CheckGetProperties(C3, true, MemberInfo<TET_Class1>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I2_P2),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I11_P11),
                                   MemberInfo<TET_Class2>.GetProp(e => e.I12_P12), 
                                   MemberInfo<TET_Class3>.GetProp(e => e.I123_P123),
                                   MemberInfo<TET_Class3>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Class3>.GetProp(e => e.I3_P3),
                                   MemberInfo<TET_Interface11>.GetProp(e => e.I11_P11),
                                   MemberInfo<TET_Interface12>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface12>.GetProp(e => e.I12_P12),
                                   MemberInfo<TET_Interface1>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface2>.GetProp(e => e.I2_P2), 
                                   MemberInfo<TET_Interface123>.GetProp(e => e.I1_P1),
                                   MemberInfo<TET_Interface123>.GetProp(e => e.I123_P123),
                                   MemberInfo<TET_Interface3>.GetProp(e => e.I3_P3));
    }

    [TestMethod]
    public void TypeExtensionsGetHierarchyTests_GetClassAttributes()
    {
      CheckGetAttributes<TET_AttribSNAttribute>(O, false);
    }

    private void CheckGetAttributes<TAttr>(Type type, bool includeInterfaces, params string[] attrValues)
      where TAttr : TET_AttributeBase
    {
      var attributes = type.GetAttributesHierarchical<TAttr>(includeInterfaces).ToArray();
    }

    private void CheckGetProperties(Type type, bool includeInterfaces, params PropertyInfo[] propInfos)
    {
      var props = type.GetPropertiesHierarchical(includeInterfaces).ToList();
      var error = "{0} properties:\r\n{1}".Fmt(type.Name, props.OrderBy(p => p.DeclaringType.Name).ThenBy(p => p.Name).ToStringList(",\r\n", p => @"{1}.{0}".Fmt(p.Name, p.DeclaringType.Name)));
      Assert.AreEqual(propInfos.Length, props.Count, error);
      foreach (var info in propInfos)
      {
        var prop = props.FirstOrDefault(p => p == info);
        Assert.IsNotNull(prop, error);
      }
    }

    private void CheckGetHierarchy(Type type, bool includeInterfaces, params Type[] hierarchy)
    {
      var getHierarchy = type.GetHierarchy(includeInterfaces).ToList();
      Assert.IsTrue(hierarchy.SameSetAs(getHierarchy), "{0} hierarchy:\r\n{1}", type.Name, getHierarchy.ToStringList(",\r\n", t => t.Name));
    }

    internal class MemberInfo<T>
    {
      public static PropertyInfo GetProp<V>(Expression<Func<T, V>> propertyAccessor)
      {
        return propertyAccessor.GetPropertyInfo();
      }
      public static MethodInfo GetMeth<V>(Expression<Func<T, V>> methodCall)
      {
        return methodCall.GetMethodInfo();
      }
      public static MethodInfo GetMeth<V>(Expression<Action<T>> methodCall)
      {
        return methodCall.GetMethodInfo();
      }
    }
  }
  #region [ Class Hierarchy ]
  /*
   * object   Interf1   Interf2   Interf3
   *   |     /  |    \    |           /
   *   |    /   |     \   |          /
   *   |   /    |      \  |         /
   * Class1   Interf11  Interf12   /
   *   |     /         /  |       /
   *   |   /     /----/   |      /
   *   | / /----/         |     /
   * Class2             Interf123
   *   |                  /
   *   |            /----/
   *   |     /-----/
   * Class3
   * 
   * */

  public abstract class TET_AttributeBase : Attribute
  {
    public TET_AttributeBase(string value) { this.Value = value; }
    public string Value { get; set; }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
  public class TET_AttribSNAttribute : TET_AttributeBase
  {
    public TET_AttribSNAttribute(string value) : base(value) { }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  public class TET_AttribSIAttribute : TET_AttributeBase
  {
    public TET_AttribSIAttribute(string value) : base(value) { }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  public class TET_AttribMNAttribute : TET_AttributeBase
  {
    public TET_AttribMNAttribute(string value) : base(value) { }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
  public class TET_AttribMIAttribute : TET_AttributeBase
  {
    public TET_AttribMIAttribute(string value) : base(value) { }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
  public class TET_AttribNotUsedAttribute : TET_AttributeBase
  {
    public TET_AttribNotUsedAttribute(string value) : base(value) { }
  }

  [TET_AttribSN("TET_Interface1")]
  [TET_AttribSI("TET_Interface1")]
  [TET_AttribMN("TET_Interface1")]
  [TET_AttribMI("TET_Interface1")]
  [TET_AttribMN("TET_Interface1-2")]
  [TET_AttribMI("TET_Interface1-2")]
  internal interface TET_Interface1
  {
    string I1_P1 { get; set; }
    string I1_M1();
    string I1_O1(int value);
    string I1_O1(double value);
  }

  [TET_AttribSN("TET_Interface2")]
  [TET_AttribSI("TET_Interface2")]
  [TET_AttribMN("TET_Interface2")]
  [TET_AttribMI("TET_Interface2")]
  [TET_AttribMN("TET_Interface2-2")]
  [TET_AttribMI("TET_Interface2-2")]
  internal interface TET_Interface2
  {
    string I2_P2 { get; set; }
    string I2_M2();
    string I2_O2(int value);
    string I2_O2(double value);
  }

  [TET_AttribSN("TET_Interface3")]
  [TET_AttribSI("TET_Interface3")]
  [TET_AttribMN("TET_Interface3")]
  [TET_AttribMI("TET_Interface3")]
  [TET_AttribMN("TET_Interface3-2")]
  [TET_AttribMI("TET_Interface3-2")]
  internal interface TET_Interface3
  {
    string I3_P3 { get; set; }
    string I3_M3();
    string I3_O3(int value);
    string I3_O3(double value);
  }

  [TET_AttribSN("TET_Interface11")]
  [TET_AttribSI("TET_Interface11")]
  [TET_AttribMN("TET_Interface11")]
  [TET_AttribMI("TET_Interface11")]
  [TET_AttribMN("TET_Interface11-2")]
  [TET_AttribMI("TET_Interface11-2")]
  internal interface TET_Interface11 : TET_Interface1
  {
    string I11_P11 { get; set; }
    string I11_M11();
    string I11_O11(int value);
    string I11_O11(double value);
  }

  [TET_AttribSN("TET_Interface12")]
  [TET_AttribSI("TET_Interface12")]
  [TET_AttribMN("TET_Interface12")]
  [TET_AttribMI("TET_Interface12")]
  [TET_AttribMN("TET_Interface12-2")]
  [TET_AttribMI("TET_Interface12-2")]
  internal interface TET_Interface12 : TET_Interface1, TET_Interface2
  {
    new int I1_P1 { get; set; }
    new int I1_M1();
    new int I1_O1(char value);
    new int I1_O1(string value);

    string I12_P12 { get; set; }
    string I12_M12();
    string I12_O12(int value);
    string I12_O12(double value);
  }

  [TET_AttribSN("TET_Interface123")]
  [TET_AttribSI("TET_Interface123")]
  [TET_AttribMN("TET_Interface123")]
  [TET_AttribMI("TET_Interface123")]
  [TET_AttribMN("TET_Interface123-2")]
  [TET_AttribMI("TET_Interface123-2")]
  internal interface TET_Interface123 : TET_Interface12, TET_Interface3
  {
    new double I1_P1 { get; set; }
    new string I1_M1();
    new string I1_O1(float value);
    new string I1_O1(DateTime value);

    string I123_P123 { get; set; }
    string I123_M123();
    string I123_O123(int value);
    string I123_O123(double value);
  }

  [TET_AttribSN("TET_Class1")]
  [TET_AttribSI("TET_Class1")]
  [TET_AttribMN("TET_Class1")]
  [TET_AttribMI("TET_Class1")]
  [TET_AttribMN("TET_Class1-2")]
  [TET_AttribMI("TET_Class1-2")]
  internal class TET_Class1 : object, TET_Interface1
  {
    public string I1_P1 { get; set; }

    public string I1_M1() { return ""; }

    public string I1_O1(int value) { return "" + value; }

    public string I1_O1(double value) { return "" + value; }
  }

  [TET_AttribSN("TET_Class2")]
  [TET_AttribSI("TET_Class2")]
  [TET_AttribMN("TET_Class2")]
  [TET_AttribMI("TET_Class2")]
  [TET_AttribMN("TET_Class2-2")]
  [TET_AttribMI("TET_Class2-2")]
  internal class TET_Class2 : TET_Class1, TET_Interface11, TET_Interface12
  {
    public new int I1_P1 { get; set; }

    public new int I1_M1() { return 0; }

    public int I1_O1(char value) { return value; }

    public int I1_O1(string value) { return value.Length; }

    public string I12_P12 { get; set; }

    public string I12_M12() { return ""; }

    public string I12_O12(int value) { return "" + value; }

    public string I12_O12(double value) { return "" + value; }

    public string I2_P2 { get; set; }

    public string I2_M2() { return ""; }

    public string I2_O2(int value) { return "" + value; }

    public string I2_O2(double value) { return "" + value; }

    public string I11_P11 { get; set; }

    public string I11_M11() { return ""; }

    public string I11_O11(int value) { return "" + value; }

    public string I11_O11(double value) { return "" + value; }
  }

  [TET_AttribSN("TET_Class3")]
  [TET_AttribSI("TET_Class3")]
  [TET_AttribMN("TET_Class3")]
  [TET_AttribMI("TET_Class3")]
  [TET_AttribMN("TET_Class3-2")]
  [TET_AttribMI("TET_Class3-2")]
  internal class TET_Class3 : TET_Class2, TET_Interface123
  {
    public new double I1_P1 { get; set; }

    public string I1_O1(float value) { return "" + value; }

    public string I1_O1(DateTime value) { return "" + value; }

    public string I123_P123 { get; set; }

    public string I123_M123() { return ""; }

    public string I123_O123(int value) { return "" + value; }

    public string I123_O123(double value) { return "" + value; }

    public string I3_P3 { get; set; }

    public string I3_M3() { return ""; }

    public string I3_O3(int value) { return "" + value; }

    public string I3_O3(double value) { return "" + value; }
  }
  #endregion

}
