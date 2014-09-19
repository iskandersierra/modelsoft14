using System;
using System.Linq;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("ModelElement_DisplayName")]
    [LocalizedDescription("ModelElement_Description")]
    [LocalizedCategory("CoreCategory")]
    //[ContractClass(typeof(ContractForModelElement))]
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

    public abstract class ModelElement : IModelElement
    {
        public virtual IModelElement Container { get; protected set; }

        public virtual IModel ContainerModel
        {
            get
            {
                return ModelElementImplementations.Compute_ContainerModel(this);
            }
        }
    }

    public static class ModelElementImplementations
    {

        public static IModel Compute_ContainerModel(IModelElement self)
        {
            if (self == null) throw new ArgumentNullException("self");
            //Contract.Requires(self != null, "Self is not null");
            //Contract.Ensures(!(self.Container != null) || (Contract.Result<IModel>() != self), "A model with container cannot be its own container model");
            //Contract.Ensures(!(self.Container == null && self is IModel) || (Contract.Result<IModel>() == self), "A model without container is its own container model");
            var result = self.Unfold(e => e.Container).OfType<IModel>().Last();
            return result;
        }
    }

    //[ContractClassFor(typeof(IModelElement))]
    //public class ContractForModelElement : IModelElement
    //{
    //    private ContractForModelElement() { }

    //    public IModelElement Container {
    //        get
    //        {
    //            return null;
    //        }
    //    }

    //    public IModel ContainerModel
    //    {
    //        get
    //        {
    //            Contract.Ensures(!(this.Container != null) || (Contract.Result<IModel>() != this), "A model with container cannot be its own container model");
    //            Contract.Ensures(!(this.Container == null && this is IModel) || (Contract.Result<IModel>() == this), "A model without container is its own container model");

    //            return null;
    //        }
    //    }
    //}
}
