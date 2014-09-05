namespace ModelSoft.Framework.DomainObjects
{
    public interface IFieldHost
    {
        void OnFieldChanged(IFieldData fieldData, object oldValue, object fieldValue);
    }
}