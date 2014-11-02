using System;
using System.Linq;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("ModelElement_DisplayName")]
    [LocalizedDescription("ModelElement_Description")]
    [LocalizedCategory("CoreCategory")]
    [TypeName("ModelElement")]
    [AbstractType]
    public interface IModelElement
    {
        [Container]
        [LocalizedDisplayName("ModelElement_Container_DisplayName")]
        [LocalizedCategory("CoreCategory")]
        IModelElement Container { get; }

        [Computed]
        [LocalizedDisplayName("ModelElement_ContainerModel_DisplayName")]
        [LocalizedCategory("CoreCategory")]
        IModel ContainerModel { get; }
    }

    public static class ModelElementImplementations
    {

        public static IModel Compute_ContainerModel(IModelElement self)
        {
            if (self == null) throw new ArgumentNullException("self");
            var result = self.Unfold(e => e.Container).OfType<IModel>().Last();
            return result;
        }
    }
}
