namespace ModelSoft.SharpModels
{
    public interface IModelProperty
    {
        long Identifier { get; }
        string Name { get; }
        ModelPropertyType ModelPropertyType { get; }
        ModelPropertyMultiplicity ModelPropertyMultiplicity { get; }
        string FriendlyName { get; }
        bool IsReadOnly { get; }

        IModelProperty Opposite { get; }
    }
    public interface IModelProperty<T> : IModelProperty
    {
    }

}
