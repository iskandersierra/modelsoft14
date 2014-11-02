namespace ModelSoft.Framework.DomainObjects
{
    [ImplementsInterface(typeof(IModelElement))]
    public abstract class ModelElement : IModelElement
    {
        private IModelElement _container;

        public IModelElement Container
        {
            get { return _container; }
            private set
            {
                if (_container == value) return;

            }
        }

        public virtual IModel ContainerModel
        {
            get
            {
                return ModelElementImplementations.Compute_ContainerModel(this);
            }
        }

    }
}