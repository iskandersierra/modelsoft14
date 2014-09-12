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

            #region Assembly and String
            var thisAssembly = typeof (TypeExtensionsTest).Assembly;

            Assert.IsTrue(thisAssembly.HasAttribute(typeof (AssemblyTitleAttribute)));
            Assert.IsTrue(thisAssembly.HasAttribute(typeof (AssemblyTitleAttribute), false));
            Assert.IsFalse(thisAssembly.HasAttribute(typeof (SerializableAttribute)));
            Assert.IsFalse(thisAssembly.HasAttribute(typeof (SerializableAttribute), false));

            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof (MultInhAttribute)));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>());
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute(typeof (MultInhAttribute), false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttribute<MultInhAttribute>(false));

            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof (MultInhAttribute)));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>());
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes(typeof (MultInhAttribute), false));
            Assert.IsFalse(CommonTypes.TypeOfString.HasAttributes<MultInhAttribute>(false));

            #endregion

            #region Interface
            var interfType = typeof(IInterfWithAttributes);

            Assert.IsTrue(interfType.HasAttribute(typeof (MultInhAttribute)));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>());
            Assert.IsTrue(interfType.HasAttribute(typeof (MultInhAttribute), false));
            Assert.IsTrue(interfType.HasAttribute<MultInhAttribute>(false));

            Assert.IsTrue(interfType.HasAttributes(typeof (MultInhAttribute)));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>());
            Assert.IsTrue(interfType.HasAttributes(typeof (MultInhAttribute), false));
            Assert.IsTrue(interfType.HasAttributes<MultInhAttribute>(false));

            var allattrs = interfType.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("1"),
                new MultNoInhAttribute("2"),
                new MultNoInhAttribute("2a"),
                new NoMultInhAttribute("3"),
                new MultInhAttribute("4"),
            });

            #endregion

            #region Class
            var classType1 = typeof(ClassWithInheritedAttributes);

            Assert.IsFalse(classType1.HasAttribute<NoMultNoInhAttribute>());
            Assert.IsFalse(classType1.HasAttribute<NoMultNoInhAttribute>(false));
            Assert.IsFalse(classType1.HasAttribute<MultNoInhAttribute>());
            Assert.IsFalse(classType1.HasAttribute<MultNoInhAttribute>(false));
            Assert.IsTrue(classType1.HasAttribute<NoMultInhAttribute>());
            Assert.IsFalse(classType1.HasAttribute<NoMultInhAttribute>(false));
            Assert.IsFalse(classType1.HasAttribute<MultInhAttribute>());
            Assert.IsTrue(classType1.HasAttribute<MultInhAttribute>(false));

            Assert.IsFalse(classType1.HasAttributes<NoMultNoInhAttribute>());
            Assert.IsFalse(classType1.HasAttributes<NoMultNoInhAttribute>(false));
            Assert.IsFalse(classType1.HasAttributes<MultNoInhAttribute>());
            Assert.IsFalse(classType1.HasAttributes<MultNoInhAttribute>(false));
            Assert.IsTrue(classType1.HasAttributes<NoMultInhAttribute>());
            Assert.IsFalse(classType1.HasAttributes<NoMultInhAttribute>(false));
            Assert.IsTrue(classType1.HasAttributes<MultInhAttribute>());
            Assert.IsTrue(classType1.HasAttributes<MultInhAttribute>(false));

            allattrs = classType1.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("5"),
            });

            allattrs = classType1.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("4"),
                new MultInhAttribute("5"),
                new NoMultInhAttribute("23"),
                new MultInhAttribute("24"),
            });

            #endregion

            #region Members, Parameters and Returns

            #region Field

            var classField = classType1.GetField("Field");
            allattrs = classField.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("^"),
            });
            allattrs = classField.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("^"),
            });

            #endregion

            #region Property

            var interfProperty = interfType.GetProperty("Property");
            allattrs = interfProperty.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("A"), new MultNoInhAttribute("B"), new MultNoInhAttribute("B1"), new NoMultInhAttribute("C"), new MultInhAttribute("D"),
            });
            allattrs = interfProperty.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("A"), new MultNoInhAttribute("B"), new MultNoInhAttribute("B1"), new NoMultInhAttribute("C"), new MultInhAttribute("D"),
            });

            var classProperty = classType1.GetProperty("Property");
            allattrs = classProperty.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("$"),
            });
            allattrs = classProperty.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("$"),
                new NoMultInhAttribute("2C"), new MultInhAttribute("2D"), 
                new MultInhAttribute("D"),
            });

            #endregion

            #region Event

            var interfEvent = interfType.GetEvent("Event");
            allattrs = interfEvent.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("R"), new MultNoInhAttribute("S"), new MultNoInhAttribute("S1"), new NoMultInhAttribute("T"), new MultInhAttribute("U"), 
            });
            allattrs = interfEvent.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("R"), new MultNoInhAttribute("S"), new MultNoInhAttribute("S1"), new NoMultInhAttribute("T"), new MultInhAttribute("U"), 
            });

            var classEvent = classType1.GetEvent("Event");
            allattrs = classEvent.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("%"),
            });
            allattrs = classEvent.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("%"),
                new NoMultInhAttribute("2T"), new MultInhAttribute("2U"),
                new MultInhAttribute("U"),
            });

            #endregion

            #region Method

            var interfMethod = interfType.GetMethod("Method");
            allattrs = interfMethod.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("a"), new MultNoInhAttribute("b"), new MultNoInhAttribute("b1"), new NoMultInhAttribute("c"), new MultInhAttribute("d"), 
            });
            allattrs = interfMethod.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("a"), new MultNoInhAttribute("b"), new MultNoInhAttribute("b1"), new NoMultInhAttribute("c"), new MultInhAttribute("d"), 
            });

            var classMethod = classType1.GetMethod("Method");
            allattrs = classMethod.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("!"),
            });
            allattrs = classMethod.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("!"),
                new NoMultInhAttribute("2c"), new MultInhAttribute("2d"), 
                new MultInhAttribute("d"), 
            });

            #endregion

            #region Method parameter

            var interfMethodParam = interfMethod.GetParameters()[0];
            allattrs = interfMethodParam.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("p"), new MultNoInhAttribute("q"), new MultNoInhAttribute("q1"), new NoMultInhAttribute("r"), new MultInhAttribute("s"), 
            });
            allattrs = interfMethodParam.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("p"), new MultNoInhAttribute("q"), new MultNoInhAttribute("q1"), new NoMultInhAttribute("r"), new MultInhAttribute("s"), 
            });

            var classMethodParam = classMethod.GetParameters()[0];
            allattrs = classMethodParam.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("#"),
            });
            allattrs = classMethodParam.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("#"),
                new NoMultInhAttribute("2r"), new MultInhAttribute("2s"), 
                new MultInhAttribute("s"), 
            });

            #endregion

            #region Method return parameter

            var interfMethodReturn = interfMethod.ReturnParameter;
            allattrs = interfMethodReturn.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("i"), new MultNoInhAttribute("j"), new MultNoInhAttribute("j1"), new NoMultInhAttribute("k"), new MultInhAttribute("l"), 
            });
            allattrs = interfMethodReturn.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new NoMultNoInhAttribute("i"), new MultNoInhAttribute("j"), new MultNoInhAttribute("j1"), new NoMultInhAttribute("k"), new MultInhAttribute("l"), 
            });

            var classMethodReturn = classMethod.ReturnParameter;
            allattrs = classMethodReturn.GetAttributes(false);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("@"),
            });
            allattrs = classMethodReturn.GetAttributes(true);
            CheckAllAttributes(allattrs, new Attribute[]
            {
                new MultInhAttribute("@"),
                new NoMultInhAttribute("2k"), new MultInhAttribute("2l"), 
                new MultInhAttribute("l"), 
            });

            #endregion

            #endregion
        }

        private void CheckAllAttributes(Attribute[] attributes, Attribute[] expected)
        {
            Assert.IsNotNull(attributes);
            Assert.IsTrue(attributes.All(a => a != null));
            attributes = attributes.OfType<TestAttrBase>().ToArray();
            Assert.AreEqual(expected.Length, attributes.Length);
            Assert.IsTrue(expected.OrderBy(e => e).SameSequenceAs(attributes.OrderBy(e => e)),
                string.Format("Sequance do not match. Expected = [{0}] and actual = [{1}]", expected.ToStringList(), attributes.ToStringList()));
        }

        #region [ Test types to apply test attributes ]

        [NoMultNoInh("1"), MultNoInh("2"), MultNoInh("2a"), NoMultInh("3"), MultInh("4")]
        public interface IInterfWithAttributes
        {
            [NoMultNoInh("A"), MultNoInh("B"), MultNoInh("B1"), NoMultInh("C"), MultInh("D")]
            string Property { get; set; }

            [NoMultNoInh("a"), MultNoInh("b"), MultNoInh("b1"), NoMultInh("c"), MultInh("d")]
            [return: NoMultNoInh("i"), MultNoInh("j"), MultNoInh("j1"), NoMultInh("k"), MultInh("l")]
            string Method(
                [NoMultNoInh("p"), MultNoInh("q"), MultNoInh("q1"), NoMultInh("r"), MultInh("s")] double distance,
                [NoMultNoInh("P"), MultNoInh("Q"), MultNoInh("Q1"), NoMultInh("R"), MultInh("S")] float speed);

            [NoMultNoInh("R"), MultNoInh("S"), MultNoInh("S1"), NoMultInh("T"), MultInh("U")]
            event EventHandler Event;

        }

        [NoMultNoInh("21"), MultNoInh("22"), MultNoInh("22a"), NoMultInh("23"), MultInh("24")]
        public interface IInterfWithAttributes2
        {
            [NoMultNoInh("2A"), MultNoInh("2B"), MultNoInh("2B1"), NoMultInh("2C"), MultInh("2D")]
            string Property { get; set; }

            [NoMultNoInh("2a"), MultNoInh("2b"), MultNoInh("2b1"), NoMultInh("2c"), MultInh("2d")]
            [return: NoMultNoInh("2i"), MultNoInh("2j"), MultNoInh("2j1"), NoMultInh("2k"), MultInh("2l")]
            string Method(
                [NoMultNoInh("2p"), MultNoInh("2q"), MultNoInh("2q1"), NoMultInh("2r"), MultInh("2s")] double distance,
                [NoMultNoInh("2P"), MultNoInh("2Q"), MultNoInh("2Q1"), NoMultInh("2R"), MultInh("2S")] float speed);

            [NoMultNoInh("2R"), MultNoInh("2S"), MultNoInh("2S1"), NoMultInh("2T"), MultInh("2U")]
            event EventHandler Event;

        }

        [MultInh("5")]
        [ImplementsInterface(typeof(IInterfWithAttributes2), typeof(IInterfWithAttributes))]
        public class ClassWithInheritedAttributes : IInterfWithAttributes, IInterfWithAttributes2
        {
            [MultInh("$")]
            public string Property { get; set; }
            [MultInh("!")]
            [return: MultInh("@")]
            public string Method([MultInh("#")] double distance, [MultInh("&")] float speed)
            {
                throw new NotImplementedException();
            }

            [MultInh("%")]
            public event EventHandler Event;

            [MultInh("^")]
            public string Field;
        }

        #endregion

        #region [ Test attributes ]
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
        public abstract class TestAttrBase : Attribute, IComparable<TestAttrBase>, IComparable
        {
            protected TestAttrBase(string name)
            {
                Name = name;
            }

            public string Name { get; set; }

            protected bool Equals(TestAttrBase other)
            {
                return base.Equals(other) && string.Equals(Name, other.Name);
            }

            public int CompareTo(TestAttrBase other)
            {
                int comp;
                comp = String.Compare(Name, other.Name, StringComparison.Ordinal);
                if (comp != 0) return comp;
                comp = String.Compare(GetType().FullName, other.GetType().FullName, StringComparison.Ordinal);
                return comp;
            }

            public int CompareTo(object obj)
            {
                return CompareTo((TestAttrBase)obj);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestAttrBase) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (base.GetHashCode()*397) ^ (Name != null ? Name.GetHashCode() : 0);
                }
            }

            public override string ToString()
            {
                return string.Format("{1}: {0}", Name, GetType().Name);
            }
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
