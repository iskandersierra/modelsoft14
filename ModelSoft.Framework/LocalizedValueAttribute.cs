using System;
using System.Resources;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework
{
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
}