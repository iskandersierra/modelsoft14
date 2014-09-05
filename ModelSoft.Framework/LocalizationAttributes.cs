using System;
using System.Collections.Concurrent;
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

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    [Serializable]
    public sealed class LocalizedResourceManagerAttribute : Attribute
    {
        private readonly Type _resourceSource;
        private readonly string _baseName;
        private readonly Type _typeOnAssembly;

        private static readonly ConcurrentDictionary<LocalizedResourceManagerAttribute, ResourceManager> Managers = new ConcurrentDictionary<LocalizedResourceManagerAttribute, ResourceManager>();

        public LocalizedResourceManagerAttribute([NotNull] Type resourceSource)
        {
            if (resourceSource == null) throw new ArgumentNullException("resourceSource");
            _resourceSource = resourceSource;
        }

        public LocalizedResourceManagerAttribute([NotNull] string baseName, [NotNull] Type typeOnAssembly)
        {
            if (baseName == null) throw new ArgumentNullException("baseName");
            if (typeOnAssembly == null) throw new ArgumentNullException("typeOnAssembly");
            _baseName = baseName;
            _typeOnAssembly = typeOnAssembly;
        }

        public LocalizedResourceManagerAttribute([NotNull] string baseName, [NotNull] Type typeOnAssembly,
            [NotNull] Type usingResourceSet)
        {
            if (baseName == null) throw new ArgumentNullException("baseName");
            if (typeOnAssembly == null) throw new ArgumentNullException("typeOnAssembly");
            if (usingResourceSet == null) throw new ArgumentNullException("usingResourceSet");
            _baseName = baseName;
            _typeOnAssembly = typeOnAssembly;
            _resourceSource = usingResourceSet;
        }

        public Type ResourceSource
        {
            get { return _resourceSource; }
        }

        public string BaseName
        {
            get { return _baseName; }
        }

        public Assembly Assembly
        {
            get { return (_typeOnAssembly ?? _resourceSource).Assembly; }
        }

        public ResourceManager GetResourceManager()
        {
            return Managers.GetOrAdd(this, key => key.CreateResourceManager());
        }

        private ResourceManager CreateResourceManager()
        {
            if (BaseName != null)
            {
                if (ResourceSource != null)
                    return new ResourceManager(BaseName, Assembly, ResourceSource);
                return new ResourceManager(BaseName, Assembly);
            }
            if (ResourceSource != null)
                return new ResourceManager(ResourceSource);
            return null;
        }

        public bool Equals(LocalizedResourceManagerAttribute other)
        {
            return other != null && 
                ResourceSource == other.ResourceSource && 
                string.Equals(BaseName, other.BaseName) && 
                Assembly == other.Assembly;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LocalizedResourceManagerAttribute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (ResourceSource != null ? ResourceSource.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (BaseName != null ? BaseName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Assembly != null ? Assembly.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            if (BaseName != null)
            {
                if (ResourceSource != null)
                    return string.Format(@"new ResourceManager(""{0}"", ""{1}"", ""{2}"")", BaseName, Assembly, ResourceSource);
                return string.Format(@"new ResourceManager(""{0}"", ""{1}"")", BaseName, Assembly);
            }
            if (ResourceSource != null)
                return string.Format(@"new ResourceManager(""{0}"")", BaseName);
            return string.Format(@"new ResourceManager()");
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public abstract class LocalizedValueAttribute : Attribute
    {
        private readonly string _resourceId;
        private readonly LocalizedResourceManagerAttribute _rmAttr;

        protected LocalizedValueAttribute([NotNull] string resourceId)
        {
            if (resourceId.IsWS()) throw new ArgumentNullException("resourceId");
            _resourceId = resourceId;
            _rmAttr = null;
        }

        protected LocalizedValueAttribute([NotNull] string resourceId, [NotNull] Type resourceSource)
        {
            if (resourceId.IsWS()) throw new ArgumentNullException("resourceId");
            _resourceId = resourceId;
            _rmAttr = new LocalizedResourceManagerAttribute(resourceSource);
        }

        protected LocalizedValueAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly)
        {
            if (resourceId.IsWS()) throw new ArgumentNullException("resourceId");
            _resourceId = resourceId;
            _rmAttr = new LocalizedResourceManagerAttribute(baseName, typeOnAssembly);
        }

        protected LocalizedValueAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly,
            [NotNull] Type usingResourceSet)
        {
            if (resourceId.IsWS()) throw new ArgumentNullException("resourceId");
            _resourceId = resourceId;
            _rmAttr = new LocalizedResourceManagerAttribute(baseName, typeOnAssembly, usingResourceSet);
        }

        public string ResourceId
        {
            get { return _resourceId; }
        }

        public bool CanCreateResourceManager
        {
            get { return _rmAttr != null; }
        }

        public ResourceManager GetResourceManager()
        {
            if (!CanCreateResourceManager) return null;
            return _rmAttr.GetResourceManager();
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public abstract class LocalizedStringAttribute : LocalizedValueAttribute
    {
        protected LocalizedStringAttribute([NotNull] string resourceId) : base(resourceId)
        {
        }

        protected LocalizedStringAttribute([NotNull] string resourceId, [NotNull] Type resourceSource) : base(resourceId, resourceSource)
        {
        }

        protected LocalizedStringAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly) : base(resourceId, baseName, typeOnAssembly)
        {
        }

        protected LocalizedStringAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet) : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class LocalizedDisplayNameAttribute : LocalizedValueAttribute
    {
        public LocalizedDisplayNameAttribute([NotNull] string resourceId) : base(resourceId)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] Type resourceSource) : base(resourceId, resourceSource)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly) : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet) : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class LocalizedDescriptionAttribute : LocalizedValueAttribute
    {
        public LocalizedDescriptionAttribute([NotNull] string resourceId) : base(resourceId)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] Type resourceSource) : base(resourceId, resourceSource)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly) : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet) : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class LocalizedCategoryAttribute : LocalizedValueAttribute
    {
        public LocalizedCategoryAttribute([NotNull] string resourceId) : base(resourceId)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] Type resourceSource) : base(resourceId, resourceSource)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly) : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet) : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }
}
