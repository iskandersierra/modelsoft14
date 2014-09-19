using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    [LocalizedDisplayName("WithIdentifier_DisplayName")]
    [LocalizedDescription("WithIdentifier_Description")]
    [LocalizedCategory("CoreCategory")]
    [ImplementsInterface(typeof(IModelElement))]
    public interface IWithIdentifier : IModelElement
    {
        [LocalizedDisplayName("WithIdentifier_Identifier_DisplayName")]
        [LocalizedCategory("IdCategory")]
        Uri Identifier { get; set; }

        [LocalizedDisplayName("WithIdentifier_GlobalIdentifier_DisplayName")]
        [LocalizedCategory("IdCategory")]
        Uri GlobalIdentifier { get; }

        [Container, OppositeProperty("Elements")]
        [LocalizedDisplayName("WithIdentifier_IdentifierSpace_DisplayName")]
        [LocalizedCategory("IdCategory")]
        IIdentifierSpace IdentifierSpace { get; }
    }

    public static class WithUriImplementations
    {
        public static Uri Compute_AbsoluteUri(IWithIdentifier self)
        {
            Contract.Requires(self != null, "self cannot be null");
            Contract.Ensures(!(self.Identifier == null) || Contract.Result<Uri>() == null, "Identifier == null => Result == null");
            Contract.Ensures(!(self.Identifier.IsAbsoluteUri || self.IdentifierSpace == null) || Contract.Result<Uri>() == self.Identifier, "Identifier.IsAbsolute or IdentifierSpace == null => Result == Identifier");

            if (self == null) throw new ArgumentNullException("self");

            if (self.Identifier == null) return null;
            if (self.Identifier.IsAbsoluteUri || self.IdentifierSpace == null) return self.Identifier;
            var parentUri = self.IdentifierSpace.GlobalIdentifier;
            if (parentUri == null) return self.Identifier;
            var result = new Uri(parentUri, self.Identifier);
            return result;
        }
    }
}
