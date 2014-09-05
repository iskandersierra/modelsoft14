using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("ModelElement_DisplayName")]
    public interface IModelElement
    {
        [Container, LocalizedDisplayName("ModelElement_Container_DisplayName")]
        IModelElement Container { get; }

        [Computed, LocalizedDisplayName("ModelElement_ContainerModel_DisplayName")]
        IModel ContainerModel { get; }
    }

    //[Implements(typeof(IModelElement))]
    public abstract class ModelElement : IModelElement
    {
        public virtual IModelElement Container { get; protected set; }

        public IModel ContainerModel
        {
            get
            {
                return this.Unfold<IModelElement>(e => e.Container).OfType<IModel>().Last();
            }
        }
    }
}
