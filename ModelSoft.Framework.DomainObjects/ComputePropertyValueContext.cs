using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.DomainObjects
{
    public class ComputePropertyValueContext
    {
        public ComputePropertyValueContext([NotNull] IModelElement instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            Instance = instance;
        }

        public IModelElement Instance { get; private set; }
    }
}