using System;
using System.Reflection;
using System.Resources;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    [Serializable]
    public sealed class LocalizedResourceManagerAttribute : Attribute
    {
        private readonly Type _resourceSource;
        private readonly string _baseName;
        private readonly Type _typeOnAssembly;

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
            if (BaseName != null)
            {
                if (ResourceSource != null)
                    return ResourceManagersCache.Current.GetResourceManager(BaseName, Assembly, ResourceSource);
                return ResourceManagersCache.Current.GetResourceManager(BaseName, Assembly);
            }
            if (ResourceSource != null)
                return ResourceManagersCache.Current.GetResourceManager(ResourceSource);
            return null;
        }

        public override string ToString()
        {
            if (BaseName != null)
            {
                if (ResourceSource != null)
                    return string.Format(@"ResourceManager(""{0}"", ""{1}"", ""{2}"")", BaseName, Assembly, ResourceSource);
                return string.Format(@"ResourceManager(""{0}"", ""{1}"")", BaseName, Assembly);
            }
            if (ResourceSource != null)
                return string.Format(@"ResourceManager(""{0}"")", BaseName);
            return string.Format(@"ResourceManager()");
        }
    }
}