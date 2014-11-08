using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.SharpModels
{
    public interface IModelPropertyChanged
    {
        event EventHandler<ModelPropertyChangedEventArgs> ModelPropertyChanged;
    }

    public class ModelPropertyChangedEventArgs : EventArgs
    {
        public ModelPropertyChangedEventArgs(
            IModelElement element, 
            IModelProperty property, 
            PropertyChangedEventArgs propertyChanged)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (property == null) throw new ArgumentNullException("property");
            if (propertyChanged == null) throw new ArgumentNullException("propertyChanged");
            Element = element;
            Property = property;
            PropertyChanged = propertyChanged;
        }

        public ModelPropertyChangedEventArgs(
            IModelElement element, 
            IModelProperty property, 
            CollectionChangeEventArgs collectionChange)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (property == null) throw new ArgumentNullException("property");
            if (collectionChange == null) throw new ArgumentNullException("collectionChange");
            Element = element;
            Property = property;
            CollectionChange = collectionChange;
        }

        public ModelPropertyChangedEventArgs(
            IModelElement element, 
            IModelProperty property, 
            ListChangedEventArgs listChanged)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (property == null) throw new ArgumentNullException("property");
            if (listChanged == null) throw new ArgumentNullException("listChanged");
            Element = element;
            Property = property;
            ListChanged = listChanged;
        }

        public IModelElement Element { get; private set; }
        
        public IModelProperty Property { get; private set; }

        public PropertyChangedEventArgs PropertyChanged { get; private set; }

        public CollectionChangeEventArgs CollectionChange { get; private set; }

        public ListChangedEventArgs ListChanged { get; private set; }
    }
}
