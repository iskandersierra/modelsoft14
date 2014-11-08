using System.Collections.Generic;
using System.ComponentModel;

namespace ModelSoft.SharpModels
{

    [IsAbstract(true)]
    [ModelElementUrl("ModelElement")]
    public interface IModelElement : 
        INotifyPropertyChanged, 
        INotifyPropertyChanging,
        IModelPropertyChanged
    {
        IModelElement ContainerElement { get; }
        IEnumerable<IModelElement> ContainedElements { get; }
        IModel ContainerModel { get; }

        ITypeInformation TypeInformation { get; }
    }
}