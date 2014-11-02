using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Resources;

namespace ModelSoft.Framework
{
    public interface IResourceManagersCache
    {
        ResourceManager GetResourceManager(string baseName, Assembly assembly);
        ResourceManager GetResourceManager(string baseName, Assembly assembly, Type usingResourceSet);
        ResourceManager GetResourceManager(Type resourceSource);
        void Clean();
    }

    public class ResourceManagersCache : IResourceManagersCache
    {
        private ConcurrentDictionary<ResourceManagersCacheKey, ResourceManager> _cache;

        public ResourceManagersCache()
        {
            _cache = new ConcurrentDictionary<ResourceManagersCacheKey, ResourceManager>();
        }

        private static readonly ResourceManagersCache _default = new ResourceManagersCache();
        public static ResourceManagersCache Default
        {
            get { return _default; }
        }


        private static ResourceManagersCache _Current;
        public static ResourceManagersCache Current
        {
            get { return _Current ?? _default; }
            set
            {
                _Current = value;
            }
        }

        public ResourceManager GetResourceManager(string baseName, Assembly assembly)
        {
            return _cache.GetOrAdd(new ResourceManagersCacheKey(baseName, assembly), key => new ResourceManager(key.BaseName, key.Assembly));
        }

        public ResourceManager GetResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
        {
            return _cache.GetOrAdd(new ResourceManagersCacheKey(baseName, assembly, usingResourceSet), key => new ResourceManager(key.BaseName, key.Assembly, key.UsingResourceSet));
        }

        public ResourceManager GetResourceManager(Type resourceSource)
        {
            return _cache.GetOrAdd(new ResourceManagersCacheKey(resourceSource), key => new ResourceManager(key.UsingResourceSet));
        }

        public void Clean()
        {
            var pairs = _cache.ToArray();
            _cache.Clear();
            pairs.ForEach(rm => rm.Value.ReleaseAllResources());
        }

        struct ResourceManagersCacheKey
        {
            public ResourceManagersCacheKey(string baseName, Assembly assembly)
            {
                BaseName = baseName;
                Assembly = assembly;
                UsingResourceSet = null;
            }

            public ResourceManagersCacheKey(string baseName, Assembly assembly, Type usingResourceSet)
            {
                BaseName = baseName;
                Assembly = assembly;
                UsingResourceSet = usingResourceSet;
            }

            public ResourceManagersCacheKey(Type resourceSource)
            {
                BaseName = null;
                Assembly = null;
                UsingResourceSet = resourceSource;
            }

            public readonly string BaseName;
            public readonly Assembly Assembly;
            public readonly Type UsingResourceSet;

            public bool Equals(ResourceManagersCacheKey other)
            {
                return 
                    string.Equals(BaseName, other.BaseName) && 
                    Equals(Assembly, other.Assembly) && 
                    UsingResourceSet == other.UsingResourceSet;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is ResourceManagersCacheKey && Equals((ResourceManagersCacheKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode =                  (BaseName != null           ? BaseName.GetHashCode()         : 0);
                    hashCode     = (hashCode*397) ^ (Assembly != null           ? Assembly.GetHashCode()         : 0);
                    hashCode     = (hashCode*397) ^ (UsingResourceSet != null   ? UsingResourceSet.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }
    }
}
