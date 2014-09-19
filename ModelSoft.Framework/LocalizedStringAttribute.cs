using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework
{
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
    public class LocalizedDisplayNameAttribute : LocalizedStringAttribute
    {
        public LocalizedDisplayNameAttribute([NotNull] string resourceId)
            : base(resourceId)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] Type resourceSource)
            : base(resourceId, resourceSource)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly)
            : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedDisplayNameAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet)
            : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class LocalizedDescriptionAttribute : LocalizedStringAttribute
    {
        public LocalizedDescriptionAttribute([NotNull] string resourceId)
            : base(resourceId)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] Type resourceSource)
            : base(resourceId, resourceSource)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly)
            : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedDescriptionAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet)
            : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    [Serializable]
    public class LocalizedCategoryAttribute : LocalizedStringAttribute
    {
        public LocalizedCategoryAttribute([NotNull] string resourceId)
            : base(resourceId)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] Type resourceSource)
            : base(resourceId, resourceSource)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly)
            : base(resourceId, baseName, typeOnAssembly)
        {
        }

        public LocalizedCategoryAttribute([NotNull] string resourceId, [NotNull] string baseName, [NotNull] Type typeOnAssembly, [NotNull] Type usingResourceSet)
            : base(resourceId, baseName, typeOnAssembly, usingResourceSet)
        {
        }
    }
}