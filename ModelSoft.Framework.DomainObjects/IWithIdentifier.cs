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
        string Identifier { get; set; }

        [LocalizedDisplayName("WithIdentifier_GlobalIdentifier_DisplayName")]
        [LocalizedCategory("IdCategory")]
        string GlobalIdentifier { get; }

        [Container, OppositeProperty("Elements")]
        [LocalizedDisplayName("WithIdentifier_IdentifierSpace_DisplayName")]
        [LocalizedCategory("IdCategory")]
        IIdentifierSpace IdentifierSpace { get; }
    }

    public static class WithIdentifierImplementations
    {
        public static string Compute_AbsoluteUri(IWithIdentifier self)
        {
            //Contract.Requires(self != null, "self cannot be null");
            //Contract.Ensures(!(self.Identifier == null) || Contract.Result<Uri>() == null, "Identifier == null => Result == null");
            //Contract.Ensures(!(self.Identifier.IsAbsoluteUri || self.IdentifierSpace == null) || Contract.Result<Uri>() == self.Identifier, "Identifier.IsAbsolute or IdentifierSpace == null => Result == Identifier");

            if (self == null) throw new ArgumentNullException("self");

            if (self.Identifier == null) return null;
            if (!Uri.IsWellFormedUriString(self.Identifier, UriKind.RelativeOrAbsolute)) return null;
            var id = new Uri(self.Identifier, UriKind.RelativeOrAbsolute);
            if (id.IsAbsoluteUri || self.IdentifierSpace == null || self.GlobalIdentifier == null) return self.Identifier;
            var parentUri = new Uri(self.IdentifierSpace.GlobalIdentifier);
            var result = new Uri(parentUri, id);
            return result.ToString();
        }
    }
}
