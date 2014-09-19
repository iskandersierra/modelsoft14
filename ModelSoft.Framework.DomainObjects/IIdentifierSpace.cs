using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("IdentifierSpace_DisplayName")]
    [LocalizedCategory("CoreCategory")]
    [ImplementsInterface(typeof(IWithIdentifier))]
    public interface IIdentifierSpace : IWithIdentifier
    {
        [Content]
        [LocalizedDisplayName("IdentifierSpace_Elements_DisplayName")]
        [LocalizedCategory("IdCategory")]
        IList<IWithIdentifier> Elements { get; }
    }
}
