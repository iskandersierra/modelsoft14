namespace ModelSoft.Framework.DomainObjects
{
    public interface ISimpleTypeInformation : 
        ITypeInformation
    {
        string SerializeToString(object instance);
        object DeserializeFromString(string serializedInstance);
    }
}