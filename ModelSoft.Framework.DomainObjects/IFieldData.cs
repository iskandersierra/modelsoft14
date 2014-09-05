namespace ModelSoft.Framework.DomainObjects
{
    public interface IFieldData
    {
        string Name { get; }
        object UntypedValue { get; set; }
        bool IsSet { get; }
        void Reset();

        IFieldHost Host { get; }
    }
}