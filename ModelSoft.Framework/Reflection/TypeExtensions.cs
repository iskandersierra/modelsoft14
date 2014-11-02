using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Collections;
using System.Xaml;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.Reflection
{
    public static class TypeExtensions
    {
        #region [ ToCommonString, IsCommonType, GetPlainName ]
        private static IDictionary<string, Type> _builtInNameToTypeMapping;
        public static IDictionary<string, Type> BuiltInNameToTypeMapping
        {
            get
            {
                InitializeMappings();
                return _builtInNameToTypeMapping;
            }
        }

        private static IDictionary<Type, string> _builtInTypeToNameMapping;
        public static IDictionary<Type, string> BuiltInTypeToNameMapping
        {
            get
            {
                InitializeMappings();
                return _builtInTypeToNameMapping;
            }
        }

        private static IList<Tup<Assembly, IList<string>>> _commonAssembliesAndNamespaces;
        public static IList<Tup<Assembly, IList<string>>> CommonAssembliesAndNamespaces
        {
            get
            {
                InitializeMappings();
                return _commonAssembliesAndNamespaces;
            }
        }

        public static bool IsCommonType([NotNull] this Type type)
        {
            while (true)
            {
                if (type == null) throw new ArgumentNullException("type");

                if (type.DeclaringType != null)
                    return false;

                if (type.IsArray || type.IsByRef || type.IsPointer)
                {
                    type = type.GetElementType();
                    continue;
                }

                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null)
                {
                    type = underlyingType;
                    continue;
                }

                if (BuiltInTypeToNameMapping.ContainsKey(type))
                    return true;

                var data = CommonAssembliesAndNamespaces.FirstOrDefault(t => type.Assembly == t.Item1);
                if (data != null)
                    return data.Item2.Contains(type.Namespace);
                return false;
            }
        }

        public static string GetPlainName(this Type type)
        {
            type.RequireNotNull("type");

            return GetPlainNameInternal(type, "");
        }

        private static string GetPlainNameInternal(this Type type, string infix)
        {
            if (type.DeclaringType == null)
            {
                if (type.IsGenericParameter)
                    return type.Name;

                if (type.IsArray)
                {
                    var newInfix = infix + "[" + new string(',', type.GetArrayRank() - 1) + "]";
                    var result = type.GetElementType().GetPlainNameInternal(newInfix);
                    return result;
                }
                if (type.IsByRef)
                {
                    var result = type.GetElementType().GetPlainNameInternal("&" + infix);
                    return result;
                }
                if (type.IsPointer)
                {
                    var result = type.GetElementType().GetPlainNameInternal("*" + infix);
                    return result;
                }

                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null)
                {
                    var result = underlyingType.GetPlainNameInternal("") + "?" + infix;
                    return result;
                }

                string name;
                if (BuiltInTypeToNameMapping.TryGetValue(type, out name))
                {
                    var result = name + infix;
                    return result;
                }
                var data = CommonAssembliesAndNamespaces.FirstOrDefault(t => type.Assembly == t.Item1);
                if (data != null && data.Item2.Contains(type.Namespace))
                {
                    var result = type.Name.SubstringBeforeLastOrSame("`") + infix;
                    return result;
                }
            }
            return type.FullName.SubstringBeforeLastOrSame("`") + infix;
        }

        public static string ToCommonString(this Type type, bool allowNull)
        {
            var result = type.ToCommonString();
            if (allowNull && type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                result = result + "?";
            return result;
        }

        /// <summary>
        /// Returns a string representation (C#-like) of a type, be it regular, generic, array, by ref, pointer or combinations of it
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToCommonString(this Type type)
        {
            type.RequireNotNull("type");

            return ToCommonStringInternal(type, "");

            //if (type.IsGenericParameter)
            //    return type.Name;
            //if (type.IsArray) // May not work for jagged arrays
            //    return type.GetElementType().ToCommonString() + "[" + new string(',', type.GetArrayRank() - 1) + "]";
            //if (type.IsByRef)
            //    return "&" + type.GetElementType().ToCommonString();
            //if (type.IsPointer)
            //    return "*" + type.GetElementType().ToCommonString();
            //var sb = new StringBuilder();
            //sb.Append(type.GetPlainName());
            //if (type.IsGenericType)
            //    sb.Append(type.GetGenericArguments().Select(t => t.ToCommonString()).ToStringList(", ", opening: "<",
            //                                                                                      closing: ">", empty: "<>"));
            //return sb.ToString();
        }

        private static string ToCommonStringInternal(this Type type, string infix)
        {
            type.RequireNotNull("type");

            if (type.IsGenericParameter)
                return type.Name;

            if (type.IsArray)
            {
                var newInfix = infix + "[" + new string(',', type.GetArrayRank() - 1) + "]";
                var result = type.GetElementType().ToCommonStringInternal(newInfix);
                return result;
            }
            if (type.IsByRef)
            {
                var result = type.GetElementType().ToCommonStringInternal("&" + infix);
                return result;
            }
            if (type.IsPointer)
            {
                var result = type.GetElementType().ToCommonStringInternal("*" + infix);
                return result;
            }

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                var result = underlyingType.ToCommonStringInternal("") + "?" + infix;
                return result;
            }

            string name;
            if (BuiltInTypeToNameMapping.TryGetValue(type, out name))
            {
                var result = name + infix;
                return result;
            }
            var data = CommonAssembliesAndNamespaces.FirstOrDefault(t => type.Assembly == t.Item1);
            if (data != null && data.Item2.Contains(type.Namespace))
            {
                name = type.Name.SubstringBeforeLastOrSame("`");
            }
            else
            {
                name = type.FullName.SubstringBeforeLastOrSame("`");
            }

            var sb = new StringBuilder();
            sb.Append(name);
            if (type.IsGenericType)
                sb.Append(type.GetGenericArguments()
                    .Select(t => t.ToCommonString())
                    .ToStringList(", ", opening: "<", closing: ">", empty: "<>"));
            sb.Append(infix);
            return sb.ToString();
        }

        private static readonly object InitializeMappingsLock = new object();

        private static void InitializeMappings()
        {
            if (_builtInNameToTypeMapping == null)
            {
                lock (InitializeMappingsLock)
                {
                    if (_builtInNameToTypeMapping == null)
                    {
                        var builtInTypeNames =
                            new[]
                            {
                                Tup.Create("object", typeof(object)),
                                Tup.Create("string", typeof(string)),
                                Tup.Create("char", typeof(char)),
                                Tup.Create("bool", typeof(bool)),
                                Tup.Create("float", typeof(float)),
                                Tup.Create("double", typeof(double)),
                                Tup.Create("decimal", typeof(decimal)),
                                Tup.Create("byte", typeof(byte)),
                                Tup.Create("sbyte", typeof(sbyte)),
                                Tup.Create("short", typeof(short)),
                                Tup.Create("ushort", typeof(ushort)),
                                Tup.Create("int", typeof(int)),
                                Tup.Create("uint", typeof(uint)),
                                Tup.Create("long", typeof(long)),
                                Tup.Create("ulong", typeof(ulong)),
                            };
                        var builtIn1 = new Dictionary<string, Type>();
                        var builtIn2 = new Dictionary<Type, string>();
                        foreach (var t in builtInTypeNames)
                        {
                            builtIn1.Add(t.Item1, t.Item2);
                            builtIn2.Add(t.Item2, t.Item1);
                        }

                        var common =
                            new[]
                            {
                                Tup.Create(typeof(string).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System", "System.Collections", "System.Collections.Generic", "System.IO", })),
                                Tup.Create(typeof(Uri).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System", })),
                                Tup.Create(typeof(Enumerable).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System", "System.Linq", })),
                                Tup.Create(typeof(XamlWriter).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System.Windows.Markup", "System.Xaml", })),
                                Tup.Create(typeof(XmlDocument).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System.Xml", })),
                                Tup.Create(typeof(XDocument).Assembly, (IList<string>)new ReadOnlyList<string>(new[] { "System.Xml.Linq", })),
                            };

                        _commonAssembliesAndNamespaces = new ReadOnlyList<Tup<Assembly, IList<string>>>(common);
                        _builtInTypeToNameMapping = new Collections.ReadOnlyDictionary<Type, string>(builtIn2);
                        var localVar = new Collections.ReadOnlyDictionary<string, Type>(builtIn1);
                        Thread.MemoryBarrier();
                        _builtInNameToTypeMapping = localVar;
                    }
                }
            }
        }
        #endregion

        #region [ GetPropertyInfo / GetPropertyName ]

        public static PropertyInfo GetPropertyInfo<TValue>(this Expression<Func<TValue>> propertyAccessor)
        {
            propertyAccessor.RequireNotNull("propertyAccessor");

            var member = propertyAccessor.Body as MemberExpression;
            (member != null).RequireCondition("propertyAccessor", @"Expression body must be a MemberExpression");

            var propertyInfo = member.Member as PropertyInfo;
            (propertyInfo != null).RequireCondition("propertyAccessor", @"Member accessor must be a Property");

            return propertyInfo;
        }

        public static string GetPropertyName<TValue>(this Expression<Func<TValue>> propertyAccessor)
        {
            return propertyAccessor.GetPropertyInfo().Name;
        }

        public static PropertyInfo GetPropertyInfo<TObject, TValue>(this Expression<Func<TObject, TValue>> propertyAccessor)
        {
            propertyAccessor.RequireNotNull("propertyAccessor");

            var member = propertyAccessor.Body as MemberExpression;
            (member != null).RequireCondition("propertyAccessor", @"Expression body must be a MemberExpression");

            var propertyInfo = member.Member as PropertyInfo;
            (propertyInfo != null).RequireCondition("propertyAccessor", @"Member accessor must be a Property");

            return propertyInfo;
        }

        public static string GetPropertyName<TObject, TValue>(this Expression<Func<TObject, TValue>> propertyAccessor)
        {
            return propertyAccessor.GetPropertyInfo().Name;
        }

        /// <summary>
        /// Expects an expression of type: p => p.Property
        /// Similar to GetPropertyInfo but checks wether the member is the parameter of the expression and not other expression, like in p => p.Foo.Bar or p => int.MaxValue.ToString().Length
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyAccessor"></param>
        /// <returns></returns>
        public static PropertyInfo GetParameterPropertyInfo<TObject, TValue>(this Expression<Func<TObject, TValue>> propertyAccessor)
        {
            propertyAccessor.RequireNotNull("propertyAccessor");

            var member = propertyAccessor.Body as MemberExpression;
            (member != null).RequireCondition("propertyAccessor", @"Expression body must be a MemberExpression");

            var propertyInfo = member.Member as PropertyInfo;
            (propertyInfo != null).RequireCondition("propertyAccessor", @"Member accessor must be a Property");

            (member.Expression == propertyAccessor.Parameters[0]).RequireCondition("propertyAccessor", @"Member accessor must be of the form p => p." + propertyInfo.Name);

            return propertyInfo;
        }

        #endregion

        #region [ GetMethodInfo / GetMethodName ]
        public static MethodInfo GetMethodInfo<TObject, TValue>(this Expression<Func<TObject, TValue>> methodCall)
        {
            methodCall.RequireNotNull("methodCall");

            var member = methodCall.Body as MethodCallExpression;
            (member != null).RequireCondition("methodCall", @"Expression body must be a MemberExpression");

            var methodInfo = member.Method;

            return methodInfo;
        }

        public static MethodInfo GetMethodInfo<TObject>(this Expression<Action<TObject>> methodCall)
        {
            methodCall.RequireNotNull("methodCall");

            var member = methodCall.Body as MethodCallExpression;
            (member != null).RequireCondition("methodCall", @"Expression body must be a MemberExpression");

            var methodInfo = member.Method;

            return methodInfo;
        }

        public static string GetMethodName<TObject, TValue>(this Expression<Func<TObject, TValue>> methodCall)
        {
            return methodCall.GetMethodInfo().Name;
        }

        public static string GetMethodName<TObject>(this Expression<Action<TObject>> methodCall)
        {
            return methodCall.GetMethodInfo().Name;
        }
        #endregion

        #region [ GetInheritedProviders ]

        private static readonly ICustomAttributeProvider[] EmptyProvidersArray = new ICustomAttributeProvider[0];
        public static ICustomAttributeProvider[] GetInheritedProviders(this ICustomAttributeProvider provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            var member = provider as MemberInfo;
            var parameter = provider as ParameterInfo;
            if (member == null && parameter == null) // It is an assembly, module or parameter info
                return EmptyProvidersArray;

            var type = provider as Type;
            if (type != null)
            {
                // It is a type so it inherits all attributes from its base class and implemented interfaces
// ReSharper disable once CoVariantArrayConversion
                return GetInheritedTypes(type);
            }
            else if (parameter != null)
            {
                // It is a parameter so find inherited member defining the parameter and return them 
// ReSharper disable once CoVariantArrayConversion
                return GetInheritedParameters(parameter);
            }
            else
            {
                // It is a member (method, property, event, field) so find inherited members from its declaring type
// ReSharper disable once CoVariantArrayConversion
                return GetInheritedMembers(member);
            }
        }

        private static readonly ParameterInfo[] EmptyParameterInfoArray = new ParameterInfo[0];
        private static ParameterInfo[] GetInheritedParameters(ParameterInfo parameter)
        {
            var member = parameter.Member;
            var inheritedMembers = GetInheritedMembers(member);
            if (inheritedMembers == null || inheritedMembers.Length == 0) return EmptyParameterInfoArray;

            var method = member as MethodBase;
            if (method != null)
            {
                var index = method.GetParameters().IndexOf(parameter);
                if (index >= 0)
                {
                    var result = inheritedMembers.OfType<MethodBase>().Select(e => e.GetParameters()[index]).ToArray();
                    return result;
                }
                var method2 = method as MethodInfo;
                if (method2 != null && method2.ReturnParameter == parameter)
                {
                    var result = inheritedMembers.OfType<MethodInfo>().Select(e => e.ReturnParameter).ToArray();
                    return result;
                }
            }

            var property = member as PropertyInfo;
            if (property != null)
            {
                var index = property.GetIndexParameters().IndexOf(parameter);
                if (index >= 0)
                {
                    var result = inheritedMembers.OfType<PropertyInfo>().Select(e => e.GetIndexParameters()[index]).ToArray();
                    return result;
                }
            }

            return EmptyParameterInfoArray;
        }

        private static readonly ConcurrentDictionary<Type, Type[]> CachedInheritedTypes = new ConcurrentDictionary<Type, Type[]>();
        public static Type[] GetInheritedTypes(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return CachedInheritedTypes.GetOrAdd(type, t => GatherInheritedTypesInternal(t, true));
        }

        public static Type[] RetrieveInheritedTypes(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return GatherInheritedTypesInternal(type, false);
        }

        private static Type[] GatherInheritedTypesInternal(Type type, bool useCache)
        {
            var implAttr = type.GetCustomAttributes(typeof (ImplementsInterfaceAttribute), false)
                .OfType<ImplementsInterfaceAttribute>().FirstOrDefault();
            var sortedTypes = implAttr == null ? CommonTypes.EmptyArrayOfType : implAttr.ImplementedInterfaces.Reverse();
            var directTypes = type.GetInterfaces()
                .Select(t => new {Type = t, Index = sortedTypes.IndexOf(t)})
                .OrderByDescending(e => e.Index)
                .Select(e => e.Type);
            var baseClass = type.BaseType;
            if (baseClass != null)
                directTypes = (baseClass.Singleton().Concat(directTypes)).ToArray();

            var allTypes = useCache 
                ? directTypes.DepthFirstSearch(GetInheritedTypes)
                : directTypes.DepthFirstSearch(RetrieveInheritedTypes);

            allTypes = allTypes.Distinct();

            return allTypes.ToArray();
        }

        public static MemberInfo[] GetInheritedMembers(MemberInfo member)
        {
            if (member == null) throw new ArgumentNullException("member");
            return GatherInheritedMembersInternal(member, true);
        }

        private static MemberInfo[] GatherInheritedMembersInternal(MemberInfo member, bool useCache)
        {
            var field = member as FieldInfo;
            if (field != null) return EmptyMemberInfoArray;

            var constructor = member as ConstructorInfo;
            if (constructor != null) return EmptyMemberInfoArray;

            var method = member as MethodInfo;
            if (method != null)
            {
                var declaringType = member.DeclaringType;
                if (declaringType == null) return EmptyMemberInfoArray;
                if (declaringType.IsInterface) return EmptyMemberInfoArray;
                var baseMethod = method.GetBaseDefinition();
                var inheritedTypes = useCache 
                    ? GetInheritedTypes(declaringType) 
                    : RetrieveInheritedTypes(declaringType);
                var result = inheritedTypes
                    .Select(t => GetInheritedMethod(declaringType, t, method, baseMethod))
                    .NotNull()
                    .ToArray<MemberInfo>();
                return result;
            }

            var property = member as PropertyInfo;
            if (property != null)
            {
                // Find inherited methods and then find the corresponding property
                var propMethod = property.GetGetMethod();
                bool isGetMethod = propMethod != null;
                if (!isGetMethod) propMethod = property.GetSetMethod();
                if (propMethod == null) return EmptyMemberInfoArray;
                var inheritedMethods = GetInheritedMembers(propMethod);
                var result = inheritedMethods
                    .Cast<MethodInfo>()
                    .Select(m => GetAssociatedProperty(m, isGetMethod))
                    .NotNull()
                    .ToArray<MemberInfo>();
                return result;
            }

            var @event = member as EventInfo;
            if (@event != null)
            {
                var evMethod = @event.GetAddMethod();
                var inheritedMethods = GetInheritedMembers(evMethod);
                var result = inheritedMethods
                    .Cast<MethodInfo>()
                    .Select(GetAssociatedEvent)
                    .NotNull()
                    .ToArray<MemberInfo>();
                return result;
            }

            return null;
        }

        private static EventInfo GetAssociatedEvent(MethodInfo method)
        {
            if (method.DeclaringType == null) return null;
            var @event = method.DeclaringType.GetEvents()
                .FirstOrDefault(p => p.GetAddMethod() == method);
            return @event;
        }

        private static PropertyInfo GetAssociatedProperty(MethodInfo method, bool isGetMethod)
        {
            if (method.DeclaringType == null) return null;
            var property = method.DeclaringType.GetProperties()
                .FirstOrDefault(p => (isGetMethod ? p.GetGetMethod() : p.GetSetMethod()) == method);
            return property;
        }

        private static MethodInfo GetInheritedMethod(Type declaringType, Type type, MethodInfo method, MethodInfo baseMethod)
        {
            var name = method.Name;
            if (type.IsClass)
            {
                if (baseMethod == method) return null;
                //if (method.IsHideBySig) return null;
                if ((method.Attributes & MethodAttributes.NewSlot) == MethodAttributes.NewSlot) return null;
                var methods = type.GetMethods()
                    .Where(m => m.Name == name)
                    .Where(m => m.GetBaseDefinition() == baseMethod)
                    .ToArray();
                if (methods.Length == 1) return methods[0];
                if (methods.Length == 0) return null;
                throw new InvalidOperationException(string.Format("Found too many methods overriding [{0}] on type {1}: {2}", method, type, methods.ToStringList("; ")));
            }
            if (type.IsInterface)
            {
                var map = declaringType.GetInterfaceMap(type);
                for (int i = 0; i < map.TargetMethods.Length; i++)
                {
                    if (map.TargetMethods[i] == method)
                        return map.InterfaceMethods[i];
                }
            }
            return null;
        }

        #endregion

        #region [ GetAllCustomAttributes ]

        /// TODO: Do it for members too (Properties, Methods and Events). Take care of inheritance.

        /// <summary>Searches and returns attributes. The inheritance chain is not used to find the attributes.</summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="type">The type which is searched for the attributes.</param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetAllCustomAttributes<T>(this Type type) where T : Attribute
        {
            return GetAllCustomAttributesInternal(type, typeof(T), false).Select(arg => (T)arg).ToArray();
        }
        public static object[] GetAllCustomAttributes(this Type type, Type attributeType)
        {
            return GetAllCustomAttributesInternal(type, attributeType, false).ToArray();
        }

        /// <summary>Searches and returns attributes.</summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="type">The type which is searched for the attributes.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. Interfaces will be searched, too.</param>
        /// <returns>Returns all attributes.</returns>
        public static T[] GetAllCustomAttributes<T>(this Type type, bool inherit) where T : Attribute
        {
            return GetAllCustomAttributesInternal(type, typeof(T), inherit).Select(arg => (T)arg).ToArray();
        }
        public static object[] GetAllCustomAttributes(this Type type, Type attributeType, bool inherit)
        {
            return GetAllCustomAttributesInternal(type, attributeType, inherit).ToArray();
        }

        /// <summary>Private helper for searching attributes.</summary>
        /// <param name="type">The type which is searched for the attribute.</param>
        /// <param name="attributeType">The type of attribute to search for.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attribute. Interfaces will be searched, too.</param>
        /// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
        private static object[] GetAllCustomAttributesInternal(Type type, Type attributeType, bool inherit)
        {
            if (!inherit)
            {
                return type.GetCustomAttributes(attributeType, false);
            }

            var attributeCollection = new Collection<object>();
            var baseType = type;

            do
            {
                baseType.GetCustomAttributes(attributeType, true).Apply(attributeCollection.Add);
                baseType = baseType.BaseType;
            }
            while (baseType != null);

            foreach (var interfaceType in type.GetInterfaces())
            {
                GetAllCustomAttributesInternal(interfaceType, attributeType, true).Apply(attributeCollection.Add);
            }

            var attributeArray = new object[attributeCollection.Count];
            attributeCollection.CopyTo(attributeArray, 0);
            return attributeArray;
        }

        /// <summary>Applies a function to every element of the list.</summary>
        private static void Apply<T>(this IEnumerable<T> enumerable, Action<T> function)
        {
            foreach (var item in enumerable)
            {
                function.Invoke(item);
            }
        }
        #endregion

        #region [ HasAttribute, GetAttribute, GetAttributes ]
        /*
         * GIT ISSUE #1: https://github.com/iskandersierra/modelsoft14/issues/1
         * 
         * Implement HasAttribute, HasAttributes, GetAttribute, GetAttributes on project ModelSoft.Framework
         * 
         * Those methods must extend interface type ICustomAttributeProvider from type 
         * ModelSoft.Framework.Reflection.TypeExtensions.
         * Method GetAttributes and its overloads allow obtaining all CLR attributes associated to any CLR 
         * reflection element. Method GetAttribute allow to obtain just one attribute, throwing an exception 
         * or returning null if exactly one attribute of the given type is not found. Methods HasAttributes 
         * and HasAttribute return whether the given member contains any or exactly one attribute of the 
         * given type.
         * All methods except one overload of GetAttributes will recieve an attribute type in the form a 
         * System.Type or a generic type. All methods will recieve parameter isInherited and 
         * includeInterfacesInheritance. Parameter isInherited indicates that inherited attributes must be 
         * retrieved as well as the directly applied attributes. Parameter includeInterfacesInheritance take 
         * into account interfaces so methods on classes inherit attributes from their interfaces. Use 
         * attribute ImplementsAttribute to indicate ordering and priority of attribute inheritance in 
         * cases where a class implements many interfaces and a member implementes many members from 
         * different interfaces.
         * */

        public static bool CacheAttributesPerMembers = true;

        public static bool HasAttribute<TAttr>(this ICustomAttributeProvider member, bool isInherited = true)
          where TAttr : Attribute
        {
            if (member == null)
                return false;

            return HasAttributeInternal(member, typeof(TAttr), isInherited);
        }
        public static bool HasAttribute(this ICustomAttributeProvider member, Type attributeType, bool isInherited = true)
        {
            if (member == null)
                return false;
            attributeType.RequireNotNull("attributeType");
            typeof(Attribute).IsAssignableFrom(attributeType).RequireCondition("attributeType");

            return HasAttributeInternal(member, attributeType, isInherited);
        }

        public static bool HasAttributes<TAttr>(this ICustomAttributeProvider member, bool isInherited = true)
          where TAttr : Attribute
        {
            if (member == null)
                return false;

            return HasAttributesInternal(member, typeof(TAttr), isInherited);
        }
        public static bool HasAttributes(this ICustomAttributeProvider member, Type attributeType, bool isInherited = true)
        {
            if (member == null)
                return false;
            attributeType.RequireNotNull("attributeType");
            typeof(Attribute).IsAssignableFrom(attributeType).RequireCondition("attributeType");

            return HasAttributesInternal(member, attributeType, isInherited);
        }

        public static TAttr GetAttribute<TAttr>(this ICustomAttributeProvider member, bool isInherited = true, bool throwOnNoSingleAttributeFound = false)
          where TAttr : Attribute
        {
            if (member == null)
                return null;

            return (TAttr)GetAttributeInternal(member, typeof(TAttr), 
                isInherited, throwOnNoSingleAttributeFound);
        }
        public static Attribute GetAttribute(this ICustomAttributeProvider member, Type attributeType, bool isInherited = true, bool throwOnNoSingleAttributeFound = false)
        {
            if (member == null)
                return null;
            attributeType.RequireNotNull("attributeType");
            typeof(Attribute).IsAssignableFrom(attributeType).RequireCondition("attributeType");

            return GetAttributeInternal(member, attributeType, 
                isInherited, throwOnNoSingleAttributeFound);
        }

        public static TAttr[] GetAttributes<TAttr>(this ICustomAttributeProvider member, bool isInherited = true)
          where TAttr : Attribute
        {
            if (member == null)
                return new TAttr[0];

            var list = member.GetAttributesInternal(typeof(TAttr), isInherited, int.MaxValue);
            return list.Cast<TAttr>().ToArray();
        }
        public static Attribute[] GetAttributes(this ICustomAttributeProvider member, Type attributeType, bool isInherited = true)
        {
            if (member == null)
                return CommonTypes.EmptyArrayOfAttribute;
            attributeType.RequireNotNull("attributeType");
            typeof(Attribute).IsAssignableFrom(attributeType).RequireCondition("attributeType");

            var list = member.GetAttributesInternal(attributeType, isInherited, int.MaxValue);
            return list;
        }
        public static Attribute[] GetAttributes(this ICustomAttributeProvider member, bool isInherited = true)
        {
            if (member == null)
                return CommonTypes.EmptyArrayOfAttribute;

            var list = member.GetAttributesInternal(null, isInherited, int.MaxValue);
            return list;
        }

        private static bool HasAttributeInternal(ICustomAttributeProvider member, Type attributeType, bool isInherited)
        {
            var list = member.GetAttributesInternal(attributeType, isInherited, 2);
            return list.Length == 1;
        }

        private static bool HasAttributesInternal(ICustomAttributeProvider member, Type attributeType, bool isInherited)
        {
            var list = member.GetAttributesInternal(attributeType, isInherited, 2);
            return list.Length >= 1;
        }

        private static Attribute GetAttributeInternal(ICustomAttributeProvider member, Type attributeType, bool isInherited, 
            bool throwOnNoSingleAttributeFound)
        {
            var list = member.GetAttributesInternal(attributeType, isInherited, 2);
            if (list.Length == 1)
                return list[0];
            if (throwOnNoSingleAttributeFound)
                throw new InvalidOperationException(string.Format("More than one attribute of type {0} applied to member {1}",
                    attributeType.Name, member));
            return null;
        }

        private static Attribute[] GetAttributesInternal(this ICustomAttributeProvider member, Type attributeType, bool isInherited, int maxAttributes)
        {
            var list = CacheAttributesPerMembers
                ? GetAllAttributesInternal(member)
                : GatherAllAttributesInternal(member);
            var result = list
                .Where(e => isInherited || !e.IsInherited)
                .Where(e => attributeType == null || attributeType.IsInstanceOfType(e.Attribute))
                .Select(e => e.Attribute)
                .ToArray();
            return result;
        }

        private static readonly ConcurrentDictionary<ICustomAttributeProvider, AttributeInfo[]> CachedMemberAttributes = new ConcurrentDictionary<ICustomAttributeProvider, AttributeInfo[]>();
        private static readonly ConcurrentDictionary<Type, AttributeUsageAttribute> CachedAttributeUsages = new ConcurrentDictionary<Type, AttributeUsageAttribute>();
        private static readonly AttributeUsageAttribute DefaultAttributeUsageAttribute = new AttributeUsageAttribute(AttributeTargets.All);
        private static AttributeInfo[] GetAllAttributesInternal(ICustomAttributeProvider member)
        {
            return CachedMemberAttributes.GetOrAdd(member, GatherAllAttributesInternal);
        }
        private static AttributeInfo[] GatherAllAttributesInternal(ICustomAttributeProvider member)
        {
            var inherited = GetAllInheritedAttributesInternal(member);
            var list = member
                .GetCustomAttributes(false);
            var current = list.OfType<Attribute>()
                .Select(a => new { Attr = a, Usage = CachedAttributeUsages.GetOrAdd(a.GetType(), t => t.GetCustomAttributes(typeof(AttributeUsageAttribute), true).OfType<AttributeUsageAttribute>().FirstOr(DefaultAttributeUsageAttribute)) })
                .Select(a => new AttributeInfo
                {
                    SourceProvider = member,
                    Attribute = a.Attr,
                    IsInheritable = a.Usage.Inherited,  
                    IsInherited = false,
                    RelativeOrder = 0,
                })
                .OrderBy(e => e.RelativeOrder)
                .ToArray();
            var result = current.Concat(inherited)
                .DistinctAttributes()
                .ToArray();
            return result;
        }

        private static readonly AttributeInfo[] EmptyAttributeInfoArray = new AttributeInfo[0];
        private static readonly MemberInfo[] EmptyMemberInfoArray = new MemberInfo[0];

        private static IEnumerable<AttributeInfo> GetAllInheritedAttributesInternal(ICustomAttributeProvider provider)
        {
            var inheritedProviders = GetInheritedProviders(provider);
            if (inheritedProviders.Length == 0)
                return EmptyAttributeInfoArray;
            var list = inheritedProviders
                .SelectMany(GetAllAttributesInternal)
                .Where(e => e.IsInheritable)
                .Select((e, i) => new AttributeInfo
                {
                    SourceProvider = e.SourceProvider,
                    Attribute = e.Attribute,
                    IsInherited = true,
                    IsInheritable = e.IsInheritable, // true
                    RelativeOrder = i,
                });
            return list;
        }

        private static IEnumerable<AttributeInfo> DistinctAttributes(this IEnumerable<AttributeInfo> attributes)
        {
            var attrTypeCount = new Dictionary<Type, int>();
            foreach (var info in attributes)
            {
                if (info == null || info.Attribute == null) continue;
                var type = info.Attribute.GetType();
                var usage = type
                    .GetCustomAttributes(typeof (AttributeUsageAttribute), true)
                    .OfType<AttributeUsageAttribute>()
                    .FirstOrDefault() 
                    ?? new AttributeUsageAttribute(AttributeTargets.All);
                int value;
                if (attrTypeCount.TryGetValue(type, out value))
                {
                    if (usage.AllowMultiple)
                    {
                        yield return info;
                    }
                    attrTypeCount[type] = value + 1;
                }
                else
                {
                    yield return info;
                    attrTypeCount[type] = 1;
                }
            }
        }

        class AttributeInfo
        {
            public ICustomAttributeProvider SourceProvider { get; set; }
            public Attribute Attribute;
            public bool IsInheritable;
            public bool IsInherited;
            public int RelativeOrder;

            public override string ToString()
            {
                return string.Format("Attribute: {0}, IsInherited: {1}, IsInheritable: {2}", Attribute, IsInherited, IsInheritable);
            }
        }
        #endregion

        #region [ HasAttributeHierarchical, GetAttributeHierarchical, GetAttributesHierarchical ]
        public static IEnumerable<TAttr> GetAttributesHierarchical<TAttr>(this Type type, bool includeInterfaces = true)
          where TAttr : Attribute
        {
            return type.GetAttributesHierarchicalInfo<TAttr>(includeInterfaces)
              .Select(t => t.Value);
        }
        public static IEnumerable<KeyValuePair<Type, TAttr>> GetAttributesHierarchicalInfo<TAttr>(this Type type, bool includeInterfaces = true)
          where TAttr : Attribute
        {
            return type.GetAttributesHierarchicalInfo(typeof(TAttr), includeInterfaces)
              .Select(t => KeyValuePair.Create(t.Key, (TAttr)t.Value));
        }
        public static IEnumerable<Attribute> GetAttributesHierarchical(this Type type, Type attributeType = null, bool includeInterfaces = true)
        {
            return type.GetAttributesHierarchicalInfo(attributeType, includeInterfaces)
              .Select(e => e.Value);
        }
        public static IEnumerable<KeyValuePair<Type, Attribute>> GetAttributesHierarchicalInfo(this Type type, Type attributeType = null, bool includeInterfaces = true)
        {
            type.RequireNotNull("type");
            var hierarchy = type.GetHierarchy(includeInterfaces);
            var attributes = attributeType == null
              ? hierarchy.SelectMany(t => t.GetCustomAttributes(false).Cast<Attribute>().Select(a => new { Attribute = a, Type = t }))
              : hierarchy.SelectMany(t => t.GetCustomAttributes(attributeType, false).Cast<Attribute>().Select(a => new { Attribute = a, Type = t }));
            var groupedAttributes = attributes
              .GroupBy(a => a.Attribute.GetType())
              .Select(g => new
                {
                    Type = g.Key,
                    Usage = g.Key.GetCustomAttribute(typeof(AttributeUsageAttribute), true) as AttributeUsageAttribute,
                    Attributes = g.ToList(),
                })
              .ToList();
            groupedAttributes = groupedAttributes
              .Select(e => e) // for each attribute type leave only the attribues that should be returned
              .ToList();
            var result = groupedAttributes
              .SelectMany(g => g.Attributes.Select(a => KeyValuePair.Create(a.Type, a.Attribute)))
              .ToList();
            return result;
        }
        #endregion

        #region [ attr.GetValueOr, attr.GetValueOrDefault ]
        public static TValue GetValue<TAttr, TValue>(this TAttr attribute, Func<TAttr, TValue> getValue)
          where TAttr : Attribute
        {
            attribute.RequireNotNull("attribute");
            getValue.RequireNotNull("getValue");

            return getValue(attribute);
        }
        public static TValue GetValueOr<TAttr, TValue>(this TAttr attribute, Func<TAttr, TValue> getValue, TValue defaultValue)
          where TAttr : Attribute
        {
            getValue.RequireNotNull("getValue");

            if (attribute == null) return defaultValue;
            return getValue(attribute);
        }
        public static TValue GetValueOrDefault<TAttr, TValue>(this TAttr attribute, Func<TAttr, TValue> getValue)
          where TAttr : Attribute
        {
            return attribute.GetValueOr(getValue, default(TValue));
        }
        #endregion

        #region [ Hierarchy ]
        public static IEnumerable<Type> GetHierarchy(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            var classes = type.Unfold(t => t.BaseType);
            var result = classes;
            if (includeInterfaces)
            {
                var interfaces = classes.SelectMany(c => c.GetInterfaces()).Distinct();
                result = result.Concat(interfaces);
            }
            return result;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesHierarchical(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            return type.GetHierarchy(includeInterfaces).SelectMany(t => t.GetTypeInfo().DeclaredProperties);
        }

        public static IEnumerable<EventInfo> GetEventsHierarchical(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            return type.GetHierarchy(includeInterfaces).SelectMany(t => t.GetTypeInfo().DeclaredEvents);
        }

        public static IEnumerable<FieldInfo> GetFieldsHierarchical(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            return type.GetHierarchy(includeInterfaces).SelectMany(t => t.GetTypeInfo().DeclaredFields);
        }

        public static IEnumerable<MemberInfo> GetMembersHierarchical(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            return type.GetHierarchy(includeInterfaces).SelectMany(t => t.GetTypeInfo().DeclaredMembers);
        }

        public static IEnumerable<MethodInfo> GetMethodsHierarchical(this Type type, bool includeInterfaces = false)
        {
            type.RequireNotNull("type");
            return type.GetHierarchy(includeInterfaces).SelectMany(t => t.GetTypeInfo().DeclaredMethods);
        }

        #endregion

        #region [ GetGenericArgs ]

        /// <summary>
        /// Finds a match for a given type and a given generic type definition.
        /// For example: typeof(List&lt;string>).GetGenericArgsFor(typeof(IEnumerable&lt;>)) returns [string]
        /// For example: typeof(Dictionary&lt;string, int>).GetGenericArgsFor(typeof(IDictionary&lt;,>)) returns [string, int]
        /// </summary>
        /// <param name="genericType"></param>
        /// <param name="genericDefinition"></param>
        /// <param name="nullOnError"></param>
        /// <returns></returns>
        public static Type[] GetGenericArgsFor(this Type genericType, Type genericDefinition, bool nullOnError = false)
        {
            if (genericType == null) throw new ArgumentNullException("genericType");
            if (genericDefinition == null) throw new ArgumentNullException("genericDefinition");
            if (!genericDefinition.IsGenericTypeDefinition) throw new ArgumentException(string.Format(Resources.NotAGenericTypeDefinition, genericDefinition));

            if (!genericType.IsGenericType)
                if (nullOnError) return null;
                else throw new ArgumentException(string.Format(Resources.NotAGenericType, genericType));

            List<Type> candidates;
            if (genericDefinition.IsInterface)
                candidates = Seq.Build(genericType).Concat(genericType.GetInterfaces()).ToList();
            else
                candidates = genericType.Unfold(e => e.BaseType).ToList();

            candidates = candidates
                .Where(e => e.IsGenericType)
                .Where(e => e.GetGenericTypeDefinition() == genericDefinition)
                .ToList();

            if (candidates.Count == 0)
                if (nullOnError) return null;
                else throw new ArgumentException(Resources.NoGenericBaseCandidateFound);
            if (candidates.Count > 1)
                if (nullOnError) return null;
                else throw new ArgumentException(Resources.AmbiguousGenericBaseCandidates);

            var result = candidates[0].GetGenericArguments();
            return result;
        }

        #endregion
    }
}
