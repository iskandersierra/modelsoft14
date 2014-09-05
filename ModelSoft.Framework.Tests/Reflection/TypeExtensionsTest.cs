using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.Test
{
    /// <summary>
    ///Se trata de una clase de prueba para TypeExtensionsTest y se pretende que
    ///contenga todas las pruebas unitarias TypeExtensionsTest.
    ///</summary>
    [TestClass()]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void TypeExtensions_GetPlainName()
        {
            Assert.IsNotNull(TypeExtensions.BuiltInNameToTypeMapping, "TypeExtensions.BuiltInNameToTypeMapping");
            Assert.IsNotNull(TypeExtensions.BuiltInTypeToNameMapping, "TypeExtensions.BuiltInTypeToNameMapping");
            Assert.IsNotNull(TypeExtensions.CommonAssembliesAndNamespaces, "TypeExtensions.CommonAssembliesAndNamespaces");

            // Built-in type
            AssertTypePlainNameInfo(typeof(int), true, "int", "int", "int?");
            AssertTypePlainNameInfo(typeof(char), true, "char", "char", "char?");
            AssertTypePlainNameInfo(typeof(bool), true, "bool", "bool", "bool?");
            AssertTypePlainNameInfo(typeof(byte), true, "byte", "byte", "byte?");
            AssertTypePlainNameInfo(typeof(uint), true, "uint", "uint", "uint?");
            AssertTypePlainNameInfo(typeof(long), true, "long", "long", "long?");
            AssertTypePlainNameInfo(typeof(float), true, "float", "float", "float?");
            AssertTypePlainNameInfo(typeof(sbyte), true, "sbyte", "sbyte", "sbyte?");
            AssertTypePlainNameInfo(typeof(short), true, "short", "short", "short?");
            AssertTypePlainNameInfo(typeof(ulong), true, "ulong", "ulong", "ulong?");
            AssertTypePlainNameInfo(typeof(ushort), true, "ushort", "ushort", "ushort?");
            AssertTypePlainNameInfo(typeof(object), true, "object", "object");
            AssertTypePlainNameInfo(typeof(string), true, "string", "string");
            AssertTypePlainNameInfo(typeof(double), true, "double", "double", "double?");
            AssertTypePlainNameInfo(typeof(decimal), true, "decimal", "decimal", "decimal?");

            // Class
            AssertTypePlainNameInfo(typeof(Activator), true, "Activator", "Activator");
            // Delegate
            AssertTypePlainNameInfo(typeof(Action), true, "Action", "Action");
            // Interface
            AssertTypePlainNameInfo(typeof(ICloneable), true, "ICloneable", "ICloneable");
            // Enum
            AssertTypePlainNameInfo(typeof(AttributeTargets), true, "AttributeTargets", "AttributeTargets", "AttributeTargets?");
            // Struct
            AssertTypePlainNameInfo(typeof(ConsoleKeyInfo), true, "ConsoleKeyInfo", "ConsoleKeyInfo", "ConsoleKeyInfo?");
            // Generic delegate
            AssertTypePlainNameInfo(typeof(Action<string>), true, "Action", "Action<string>");
            AssertTypePlainNameInfo(typeof(Action<>), true, "Action", "Action<T>");
            // Composite generic delegates
            AssertTypePlainNameInfo(typeof(Action<Func<bool, string>, char>), true, "Action", "Action<Func<bool, string>, char>");
            // Generic interface
            AssertTypePlainNameInfo(typeof(IComparable<string>), true, "IComparable", "IComparable<string>");
            // Arrays
            AssertTypePlainNameInfo(typeof(int[]), true, "int[]", "int[]");
            AssertTypePlainNameInfo(typeof(IComparable<string>[]), true, "IComparable[]", "IComparable<string>[]");
            // Matrix
            AssertTypePlainNameInfo(typeof(int[,]), true, "int[,]", "int[,]");
            // Jagged array
            AssertTypePlainNameInfo(typeof(int[][]), true, "int[][]", "int[][]");
            // Composite arrays and matrices
            AssertTypePlainNameInfo(typeof(int[,,][]), true, "int[,,][]", "int[,,][]");
            AssertTypePlainNameInfo(typeof(int[][,,]), true, "int[][,,]", "int[][,,]");
            AssertTypePlainNameInfo(typeof(int[,][,,]), true, "int[,][,,]", "int[,][,,]");
            AssertTypePlainNameInfo(typeof(IComparable<int[,][, ,]>), true, "IComparable", "IComparable<int[,][,,]>");
            AssertTypePlainNameInfo(typeof(IComparable<int[,][, ,]>[][, , ,]), true, "IComparable[][,,,]", "IComparable<int[,][,,]>[][,,,]");
            // Nullable
            AssertTypePlainNameInfo(typeof(int?), true, "int?", "int?");
            // Array of Nullable
            AssertTypePlainNameInfo(typeof(int?[]), true, "int?[]", "int?[]");
            // Pointer
            AssertTypePlainNameInfo(typeof(int*), true, "int*", "int*");
            // Pointer of Pointer
            AssertTypePlainNameInfo(typeof(int**), true, "int**", "int**");
            // Array of Pointer
            AssertTypePlainNameInfo(typeof(int*[]), true, "int*[]", "int*[]");
            AssertTypePlainNameInfo(typeof(int*[,][][, ,]), true, "int*[,][][,,]", "int*[,][][,,]");
            // ByRef
            //AssertTypePlainNameInfo(typeof(int&), true, "int&", "int&");

            // Uncommon type
            AssertTypePlainNameInfo(typeof(TypeExtensionsTest), false, "ModelSoft.Framework.Test.TypeExtensionsTest", "ModelSoft.Framework.Test.TypeExtensionsTest");
            AssertTypePlainNameInfo(typeof(TypeExtensionsTest[]), false, "ModelSoft.Framework.Test.TypeExtensionsTest[]", "ModelSoft.Framework.Test.TypeExtensionsTest[]");

        }

        private void AssertTypePlainNameInfo(Type type, bool isCommon, string plainName, string commonString, string nullableCommonString = null)
        {
            if (isCommon)
                Assert.IsTrue(type.IsCommonType(), type + ".IsCommonType()");
            else
                Assert.IsFalse(type.IsCommonType(), type + ".IsCommonType()");

            Assert.AreEqual(plainName, type.GetPlainName(), type + ".GetPlainName()");

            Assert.AreEqual(commonString, type.ToCommonString(), type + ".ToCommonString()");

            if (nullableCommonString != null)
                Assert.AreEqual(nullableCommonString, type.ToCommonString(true), type + ".ToCommonString(true)");
            else
                Assert.AreEqual(commonString, type.ToCommonString(true), type + ".ToCommonString(true)");

        }

        #region [ HasAttribute, HasAttributes, GetAttribute, GetAttributes  ]

        [TestMethod]
        public void TypeExtensions_HasDirectAttributes()
        {
            var thisAssembly = typeof (TypeExtensionsTest).Assembly;
            var interfType = typeof(IInterfWithAttributes);
            var classType1 = typeof(ClassWithInheritedAttributes);

            Assert.IsTrue(thisAssembly.HasAttribute(typeof(AssemblyTitleAttribute)));
            Assert.IsTrue(thisAssembly.HasAttribute(typeof(AssemblyTitleAttribute), false));
            Assert.IsTrue(thisAssembly.HasAttribute(typeof(AssemblyTitleAttribute), true, true));
            Assert.IsFalse(thisAssembly.HasAttribute(typeof(SerializableAttribute)));
            Assert.IsFalse(thisAssembly.HasAttribute(typeof(SerializableAttribute), false));
            Assert.IsFalse(thisAssembly.HasAttribute(typeof(SerializableAttribute), true, true));

            Assert.IsTrue(interfType.HasAttribute(typeof(MultInhAttribute)));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>());
            Assert.IsTrue(interfType.HasAttribute(typeof(MultInhAttribute), false));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>(false));
            Assert.IsTrue(interfType.HasAttribute(typeof(MultInhAttribute), false, true));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>(false, true));
            Assert.IsTrue(interfType.HasAttribute(typeof(MultInhAttribute), true, true));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>(true, true));
            
            Assert.IsTrue(interfType.HasAttributes(typeof(MultInhAttribute)));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>());
            Assert.IsTrue(interfType.HasAttributes(typeof(MultInhAttribute), false));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>(false));
            Assert.IsTrue(interfType.HasAttributes(typeof(MultInhAttribute), false, true));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>(false, true));
            Assert.IsTrue(interfType.HasAttributes(typeof(MultInhAttribute), true, true));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>(true, true));
            
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof(MultInhAttribute)));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>());
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof(MultInhAttribute), false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>(false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof(MultInhAttribute), false, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>(false, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof(MultInhAttribute), true, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>(true, true));
            
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof(MultInhAttribute)));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>());
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof(MultInhAttribute), false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>(false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof(MultInhAttribute), false, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>(false, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof(MultInhAttribute), true, true));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>(true, true));

            var allattrs = interfType.GetAttributes(false);
            Assert.IsNotNull(allattrs, "GetAttributes");
            Assert.AreEqual(4, allattrs.Length);
            Assert.IsTrue(allattrs.All(a => a != null));
        }

        #region [ Test types to apply test attributes ]

        [NoMultNoInh("1")]
        [MultNoInh("2")]
        [NoMultInh("3")]
        [MultInh("4")]
        public interface IInterfWithAttributes
        {

        }

        public class ClassWithInheritedAttributes : IInterfWithAttributes
        {
            
        }

        #endregion

        #region [ Test attributes ]
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
        public abstract class TestAttrBase : Attribute
        {
            protected TestAttrBase(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
        public class MultInhAttribute : TestAttrBase
        {
            public MultInhAttribute(string name) : base(name)
            {
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
        public class MultNoInhAttribute : TestAttrBase
        {
            public MultNoInhAttribute(string name)
                : base(name)
            {
            }
        }

        public class NoMultInhAttribute : TestAttrBase
        {
            public NoMultInhAttribute(string name)
                : base(name)
            {
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
        public class NoMultNoInhAttribute : TestAttrBase
        {
            public NoMultNoInhAttribute(string name)
                : base(name)
            {
            }
        }
        #endregion

        #endregion [ HasAttribute, HasAttributes, GetAttribute, GetAttributes  ]


    }
}
