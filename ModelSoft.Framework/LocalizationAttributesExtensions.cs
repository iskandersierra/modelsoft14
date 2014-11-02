using System;
using System.ComponentModel;
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
        #region [ (Has/Get)Localized(Value/String/Stream/Object) ]

        public static bool HasLocalizedValue<TAttr>(this ICustomAttributeProvider attributeProvider)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return false;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return false;

            return true;
        }

        public static string GetLocalizedString<TAttr>(this ICustomAttributeProvider attributeProvider,
            CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetString(attr.ResourceId, culture);
        }

        public static Stream GetLocalizedStream<TAttr>(this ICustomAttributeProvider attributeProvider,
            CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetStream(attr.ResourceId, culture);
        }

        public static object GetLocalizedObject<TAttr>(this ICustomAttributeProvider attributeProvider,
            CultureInfo culture = null)
            where TAttr : LocalizedValueAttribute
        {
            var attr = attributeProvider.GetAttribute<TAttr>();
            if (attr == null) return null;

            var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
            if (rm == null) return null;

            return rm.GetObject(attr.ResourceId, culture);
        }

        #endregion

        #region [ Get(DisplayName/Description/Category)Function ]

        /// <summary>
        /// Returns a function which given a culture returns the display name of the given member <param name="attributeProvider">attributeProvider</param>
        /// The function might return a localized display name, the value of a DisplayNameAttribute or null 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <returns></returns>
        public static Func<CultureInfo, string> GetDisplayNameFunction(this ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider == null) throw new ArgumentNullException("attributeProvider");

            var result =
                GetLocalizedStringFunction<LocalizedDisplayNameAttribute, DisplayNameAttribute>(attributeProvider,
                    attr => attr.DisplayName);

            return result;
        }

        /// <summary>
        /// Returns a function which given a culture returns the description of the given member <param name="attributeProvider">attributeProvider</param>
        /// The function might return a localized description, the value of a DescriptionAttribute or null 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <returns></returns>
        public static Func<CultureInfo, string> GetDescriptionFunction(this ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider == null) throw new ArgumentNullException("attributeProvider");

            var result =
                GetLocalizedStringFunction<LocalizedDescriptionAttribute, DescriptionAttribute>(attributeProvider,
                    attr => attr.Description);

            return result;
        }

        /// <summary>
        /// Returns a function which given a culture returns the category of the given member <param name="attributeProvider">attributeProvider</param>
        /// The function might return a localized category, the value of a CategoryAttribute or null 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <returns></returns>
        public static Func<CultureInfo, string> GetCategoryFunction(this ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider == null) throw new ArgumentNullException("attributeProvider");

            var result = GetLocalizedStringFunction<LocalizedCategoryAttribute, CategoryAttribute>(attributeProvider,
                attr => attr.Category);

            return result;
        }

        private static Func<CultureInfo, string> GetLocalizedStringFunction<TLocalizedAttribute, TNonLocalizedAttribute>
            (
            ICustomAttributeProvider attributeProvider,
            Func<TNonLocalizedAttribute, string> getStringValueFunc)
            where TLocalizedAttribute : LocalizedValueAttribute
            where TNonLocalizedAttribute : Attribute
        {
            // First check wether there is a localized attribute
            if (attributeProvider.HasLocalizedValue<TLocalizedAttribute>())
            {
                var attr = attributeProvider.GetAttribute<TLocalizedAttribute>();
                if (attr != null)
                {
                    var rm = attr.GetResourceManager() ?? attributeProvider.FindResourceManager();
                    if (rm != null)
                    {
                        return culture => rm.GetString(attr.ResourceId, culture);
                    }
                }
            }

            // If it got here then find some DisplayNameAttribute
            var attributes = attributeProvider.GetCustomAttributes(typeof (DisplayNameAttribute), true);
            if (attributes.Length == 1 && attributes[0] is DisplayNameAttribute)
            {
                var displayName = ((DisplayNameAttribute) attributes[0]).DisplayName;
                return _ => displayName;
            }

            return null;
        }

        #endregion

        #region [ FindResourceManager ]

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

        #endregion
    }

}