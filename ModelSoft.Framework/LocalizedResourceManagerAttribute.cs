using System;
using System.Collections.Concurrent;
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
}