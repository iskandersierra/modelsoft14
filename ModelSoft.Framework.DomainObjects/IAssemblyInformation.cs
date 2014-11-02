using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IAssemblyInformation : 
        INamedInformation
    {
        //Assembly ClrAssembly { get; }
        IReadOnlyCollection<ITypeInformation> Types { get; }
    }

    public class AssemblyInformation : 
        NamedInformation, 
        IAssemblyInformation
    {
        private IReadOnlyCollection<ITypeInformation> _types;

        public AssemblyInformation(
            IModelElementInformationProvider informationProvider, 
            string name, 
            ITypeInformation[] types,
            Func<CultureInfo, string> displayNameFunc = null, 
            Func<CultureInfo, string> descriptionFunc = null, 
            Func<CultureInfo, string> categoryFunc = null) : 
            base(informationProvider, name, displayNameFunc, descriptionFunc, categoryFunc)
        {
            if (types == null) throw new ArgumentNullException("types");

            // Check no null literal
            if (types.Any(e => e == null))
                throw new ArgumentNullException("types");
            // Check no duplicate literal names
            if (types.AreDistinct(e => e.Name))
                throw new ArgumentException(string.Format("Assembly {0} cannot contain duplicated type names: {1}", this.Name, types.GetDuplicates(e => e.Name).ToStringList(", ", e => e.Name)), "types");

            _types = new ReadOnlyCollection<ITypeInformation>(types);
        }

        public IReadOnlyCollection<ITypeInformation> Types
        {
            get { return _types; }
        }
    }
}
