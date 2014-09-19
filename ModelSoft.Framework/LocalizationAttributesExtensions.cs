using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Framework
{
    public static class LocalizationAttributesExtensions
    {
        public static bool HasLocalizedValue<TAttr>(this ICustomAttributeProvider attributeProvider)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return false;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return false;

            return true;
        }

        public static string GetLocalizedString<TAttr>(this ICustomAttributeProvider attributeProvider, CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetString(attr.ResourceId, culture);
        }

        public static Stream GetLocalizedStream<TAttr>(this ICustomAttributeProvider attributeProvider, CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetStream(attr.ResourceId, culture);
        }

        public static object GetLocalizedObject<TAttr>(this ICustomAttributeProvider attributeProvider, CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetObject(attr.ResourceId, culture);
        }

        public static ResourceManager FindResourceManager(this ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider == null) return null;
            var attr = attributeProvider.GetAttribute<LocalizedResourceManagerAttribute>();
            if (attr != null) return attr.GetResourceManager();

            var module = attributeProvider as Module;
            if (module != null)
                return FindInheritedResourceManager(module);
            var member = attributeProvider as MemberInfo;
            if (member != null)
                return FindInheritedResourceManager(member);
            var parameter = attributeProvider as ParameterInfo;
            if (parameter != null)
                return FindInheritedResourceManager(parameter);
            return null;
        }

        private static ResourceManager FindInheritedResourceManager([NotNull] Module module)
        {
            var result = module.Assembly.FindResourceManager();
            return result;
        }

        private static ResourceManager FindInheritedResourceManager([NotNull] MemberInfo member)
        {
            var result = member.DeclaringType.FindResourceManager() ?? member.Module.FindResourceManager();
            return result;
        }

        private static ResourceManager FindInheritedResourceManager([NotNull] ParameterInfo parameter)
        {
            var result = parameter.Member.FindResourceManager();
            return result;
        }
    }
}