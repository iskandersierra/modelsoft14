using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework.IoC
{
    public static class Inject
    {
        #region [ GetAttributedInjectMembers ]
        public static IEnumerable<InjectMemberInfo> GetAttributedInjectMembers(Type type)
        {
            type.RequireNotNull("type");
            Require.Condition(type.IsClass);

            var result = GetAttributedInjectMembersInternal(type);
#if TRACE
            result = result.ToArray();
#endif
            return result;
        }
        public static IEnumerable<InjectMemberInfo> GetAttributedInjectProperties(Type type)
        {
            type.RequireNotNull("type");
            Require.Condition(type.IsClass);

            var result = GetAttributedInjectPropertiesInternal(type);
#if TRACE
            result = result.ToArray();
#endif
            return result;
        }
        public static IEnumerable<InjectMemberInfo> GetAttributedInjectMethods(Type type)
        {
            type.RequireNotNull("type");
            Require.Condition(type.IsClass);

            var result = GetAttributedInjectMethodsInternal(type);
#if TRACE
            result = result.ToArray();
#endif
            return result;
        }
        public static IEnumerable<InjectMemberInfo> GetAttributedInjectConstructors(Type type)
        {
            type.RequireNotNull("type");
            Require.Condition(type.IsClass);

            var result = GetAttributedInjectConstructorsInternal(type);
#if TRACE
            result = result.ToArray();
#endif
            return result;
        }
        #endregion [ GetAttributedInjectMembers ]

        #region [ InjectOn ]
        public static InjectOn<T> AddInject<T>(this InjectOn<T> injector, InjectMemberInfo injectInfo)
        {
            injector.RequireNotNull("injector");
            injectInfo.RequireNotNull("injectInfo");
            injector.Infos.Add(injectInfo);
            return injector;
        }

        public static InjectOnProperty<T, P> InjectProperty<T, P>(this InjectOn<T> injector, Expression<Func<T, P>> property)
        {
            injector.RequireNotNull("injector");
            property.RequireNotNull("property");
            Require.Condition(property.Body is MemberExpression, "property", "property must be a property access expression");
            var memberAccess = (MemberExpression)property.Body;
            Require.Condition(memberAccess.Member is PropertyInfo, "property", "property must be a property access expression");
            var prop = (PropertyInfo)memberAccess.Member;
            Require.Condition(memberAccess.Expression == property.Parameters[0], "property", "property must be a property access expression on parameter " + property.Parameters[0].Name);
            Require.Condition(prop.CanWrite, "property", "property must be a setable property");
            Require.Condition(!prop.GetSetMethod().IsStatic, "property", "property must be an instance property");

            var propInjector = new InjectOnProperty<T, P>(injector)
              {
                  Info = new InjectPropertyInfo(prop.Name),
                  Position = injector.Infos.Count,
                  PropertyInfo = prop,
              };
            injector.Infos.Add(propInjector.Info);

            return propInjector;
        }

        public static void WithValue<T, P>(this InjectOnProperty<T, P> propertyInjector, object value)
        {
            propertyInjector.RequireNotNull("propertyInjector");

            var newParam = new InjectParameterValue(value);
            var newInfo = new InjectPropertyInfo(propertyInjector.Info.PropertyName, newParam);
            propertyInjector.Info = newInfo;
            propertyInjector.Injector.Infos[propertyInjector.Position] = newInfo;
        }

        public static void WithService<T, P>(this InjectOnProperty<T, P> propertyInjector, Type service, string key = null)
        {
            propertyInjector.RequireNotNull("propertyInjector");

            if (service != null)
            {
                var newParam = new InjectResolvedParameterValue(service, key);
                var newInfo = new InjectPropertyInfo(propertyInjector.Info.PropertyName, newParam);
                propertyInjector.Info = newInfo;
                propertyInjector.Injector.Infos[propertyInjector.Position] = newInfo;
            }
        }

        public static void WithOptionalService<T, P>(this InjectOnProperty<T, P> propertyInjector, Type service, string key = null)
        {
            propertyInjector.RequireNotNull("propertyInjector");

            if (service != null)
            {
                var newParam = new InjectOptionalParameterAttribute(service, key);
                var newInfo = new InjectPropertyInfo(propertyInjector.Info.PropertyName, newParam);
                propertyInjector.Info = newInfo;
                propertyInjector.Injector.Infos[propertyInjector.Position] = newInfo;
            }
        }

        //public static InjectOnConstructor<T> InjectConstructor<T>(this InjectOn<T> injector, Expression<Func<T>> constructor)
        //{
        //  injector.RequireNotNull("injector");
        //  constructor.RequireNotNull("constructor");
        //  Require.Condition(constructor.Body is MemberExpression, "property", "property must be a property access expression");
        //  var memberAccess = (MemberExpression)property.Body;
        //  Require.Condition(memberAccess.Member is PropertyInfo, "property", "property must be a property access expression");
        //  var prop = (PropertyInfo)memberAccess.Member;
        //  Require.Condition(memberAccess.Expression == property.Parameters[0], "property", "property must be a property access expression on parameter " + property.Parameters[0].Name);
        //  Require.Condition(prop.CanWrite, "property", "property must be a setable property");
        //  Require.Condition(!prop.GetSetMethod().IsStatic, "property", "property must be an instance property");

        //  var propInjector = new InjectOnProperty<T, P>(injector)
        //  {
        //    Info = new InjectPropertyInfo(prop.Name),
        //    Position = injector.Infos.Count,
        //    PropertyInfo = prop,
        //  };
        //  injector.Infos.Add(propInjector.Info);

        //  return propInjector;
        //}


        #endregion [ InjectOn ]

        #region [ Utilities ]
        private static IEnumerable<InjectMemberInfo> GetAttributedInjectMembersInternal(Type type)
        {
            var properties = GetAttributedInjectPropertiesInternal(type);
            var constructors = GetAttributedInjectConstructorsInternal(type);
            var methods = GetAttributedInjectMethodsInternal(type);
#if TRACE
            properties = properties.ToArray();
            constructors = constructors.ToArray();
            methods = methods.ToArray();
#endif
            return properties
              .Concat(constructors)
              .Concat(methods);// Concat
        }

        private static IEnumerable<InjectMemberInfo> GetAttributedInjectPropertiesInternal(Type type)
        {
            var properties = type
              .GetPropertiesHierarchical()
              .Where(p => p.HasAttribute<DependencyAttribute>())
              .Distinct(p => p.Name)
              .ToArray();
            foreach (var prop in properties)
            {
                var attr = prop.GetAttribute<DependencyAttribute>();
                var injectParam = GetInjectParam(prop);
                yield return new InjectPropertyInfo(prop.Name);
            }
        }

        private static IEnumerable<InjectMemberInfo> GetAttributedInjectConstructorsInternal(Type type)
        {
            var constructors = type.GetTypeInfo()
              .DeclaredConstructors
              .Where(p => p.HasAttribute<DependencyAttribute>())
              .ToArray();
            Require.Condition(constructors.Length <= 1, "type", "Type have more than one injection constructor: " + type.FullName);

            if (constructors.Length == 0)
            {
                constructors = type.GetTypeInfo().DeclaredConstructors.OrderByDescending(e => e.GetParameters().Length).ToArray();
                if (constructors.Length == 0)
                    throw new NotSupportedException("Type don't have any constructors: " + type.FullName);
                if (constructors.Length == 1)
                    yield break;
                if (constructors[0].GetParameters().Length == constructors[1].GetParameters().Length)
                    throw new NotSupportedException("Type have more than one constructor with the maximum number of parameters: " + type.FullName);
            }
            var parameterValues = ExtractParameterInjects(constructors[0]);
            yield return new InjectConstructorInfo(parameterValues);
        }

        private static IEnumerable<InjectMemberInfo> GetAttributedInjectMethodsInternal(Type type)
        {
            var methods = type
              .GetMethodsHierarchical()
              .Where(p => p.HasAttribute<DependencyAttribute>())
              .ToArray();
            foreach (var method in methods)
            {
                var parameterValues = ExtractParameterInjects(method);
                yield return new InjectMethodInfo(method.Name, parameterValues);
            }
            yield break;
        }

        private static object[] ExtractParameterInjects(MethodBase method)
        {
            var result = method
              .GetParameters()
              .Select(p => GetInjectParam(p))
              .ToArray<object>();
            return result;
        }

        private static InjectParameterValueBase GetInjectParam(ICustomAttributeProvider member)
        {
            member.RequireNotNull("member");

            var attrs = member.GetAttributes<InjectParameterValueAttribute>();
            Require.Condition(attrs.OfType<InjectArrayParameterValueAttribute>().Count() <= 1, "member", "Members cannot have more than one inject array attribute");

            var arr = attrs.OfType<InjectArrayParameterValueAttribute>().SingleOrDefault();
            var simples = attrs.OfType<InjectSimpleParameterValueAttribute>().OrderBy(e => e.Order).ToArray();
            var memberType = GetMemberOutType(member);

            if (arr != null)
            {
                var resArr = arr as InjectResolveArrayParameterAttribute;
                if (resArr != null)
                {
                    var elementType = resArr.ElementType;
                    var elementValues = simples.Select(a => GetSimpleInjectParam(elementType, a)).ToArray();

                    var result = resArr.ArrayType != null
                      ? new InjectResolveArrayParameterValue(resArr.ElementType, elementValues)
                      : new InjectResolveArrayParameterValue(resArr.ArrayType, resArr.ElementType, elementValues);
                    return result;
                }
                throw new NotSupportedException("Array parameter attribute not supported: " + arr.GetType());
            }
            else
            {
                Require.Condition(simples.Length <= 1, "member", "Members cannot have more than one simple inject attribute");
                return GetSimpleInjectParam(GetMemberOutType(member), simples.SingleOrDefault());
            }
        }

        private static InjectParameterValueBase GetSimpleInjectParam(Type serviceType, InjectSimpleParameterValueAttribute injectAttr)
        {
            if (injectAttr == null)
            {
                var service = serviceType;
                var result = new InjectResolvedParameterValue(service);
                return result;
            }

            var opt = injectAttr as InjectOptionalParameterAttribute;
            if (opt != null)
            {
                var service = opt.Service ?? serviceType;
                var result = new InjectOptionalParameterValue(service, opt.Key);
                return result;
            }

            var res = injectAttr as InjectOptionalParameterAttribute;
            if (res != null)
            {
                var service = res.Service ?? serviceType;
                var result = new InjectResolvedParameterValue(service, res.Key);
                return result;
            }

            throw new NotSupportedException("Simple parameter attribute not supported: " + injectAttr.GetType());
        }

        private static Type GetMemberOutType(ICustomAttributeProvider member)
        {
            member.RequireNotNull("member");

            if (member is PropertyInfo)
                return ((PropertyInfo)member).PropertyType;
            if (member is FieldInfo)
                return ((FieldInfo)member).FieldType;
            if (member is MethodInfo)
                return ((MethodInfo)member).ReturnType;
            if (member is ParameterInfo)
                return ((ParameterInfo)member).ParameterType;
            throw new NotSupportedException("Don't support member type: " + member.GetType().FullName);
        }

        #endregion [ Utilities ]
    }

    public class InjectOn
    {
        internal InjectOn() { }
        internal List<InjectMemberInfo> Infos = new List<InjectMemberInfo>();
    }

    public class InjectOn<T> : InjectOn
    {
        internal InjectOn() { }
    }

    public interface IInjectOnMember<T>
    {

    }

    public abstract class InjectOnMember<T> :
      IInjectOnMember<T>
    {
        internal InjectOnMember(InjectOn<T> injector) { this.Injector = injector; }
        internal InjectOn<T> Injector;
        internal int Position;
    }

    public class InjectOnProperty<T, P> :
      InjectOnMember<T>
    {
        internal InjectOnProperty(InjectOn<T> injector) : base(injector) { }
        internal InjectPropertyInfo Info;
        internal PropertyInfo PropertyInfo;
    }

    public class InjectOnConstructor<T> :
      InjectOnMember<T>
    {
        internal InjectOnConstructor(InjectOn<T> injector) : base(injector) { }
        internal InjectConstructorInfo Info;
        internal ConstructorInfo ConstructorInfo;
    }
}
