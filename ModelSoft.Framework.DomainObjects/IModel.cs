using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("Model_DisplayName")]
    [LocalizedCategory("CoreCategory")]
    [ImplementsInterface(typeof(IIdentifierSpace))]
    public interface IModel : IIdentifierSpace
    {
        [Content]
        [LocalizedDisplayName("Model_Elements_DisplayName")]
        [LocalizedCategory("CoreCategory")]
        new IList<IModelElement> Elements { get; }
    }
}
