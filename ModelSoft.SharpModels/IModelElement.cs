using System.Collections.Generic;

namespace ModelSoft.SharpModels
{

    [IsAbstract(true)]
    [ModelElementUrl("ModelElement")]
    public interface IModelElement
    {
        IModelElement ContainerElement { get; }
        IEnumerable<IModelElement> ContainedElements { get; }
        IModel ContainerModel { get; }
    }
}