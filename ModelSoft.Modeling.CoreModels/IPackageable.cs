using ModelSoft.Framework.DomainObjects;

namespace ModelSoft.Core
{
    public interface IPackageable : IWithUri
    {
        [Container]
        IPackage ContainerPackage { get; }
    }
}