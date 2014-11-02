using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Reflection
{
    public interface ITypeSearchService
    {
        IEnumerable<Type> GetAllTypes(params Assembly[] assemblies);
        IEnumerable<Type> GetAllTypes(bool skipOnError, params Assembly[] assemblies);
        IEnumerable<Type> GetAllTypes(IEnumerable<Assembly> assemblies, bool skipOnError = true);
        IEnumerable<Type> GetAllTypes(Assembly assembly, bool skipOnError = true);

        IEnumerable<Type> GetAllClasses(IEnumerable<Type> types, Type baseType = null,
            bool? isConcrete = true, bool? isGenericDefinition = false, bool skipOnError = true);
        IEnumerable<Type> GetAllClasses<TBase>(IEnumerable<Type> types,
            bool? isConcrete = true, bool? isGenericDefinition = false, bool skipOnError = true);

        IEnumerable<Type> GetAllInterfaces(IEnumerable<Type> types, Type baseType = null,
            bool? isGenericDefinition = false, bool skipOnError = true);
        IEnumerable<Type> GetAllInterfaces<TBase>(IEnumerable<Type> types,
            bool? isGenericDefinition = false, bool skipOnError = true);
    }

    public class DefaultTypeSearchService : ITypeSearchService
    {
        public IEnumerable<Type> GetAllTypes(params Assembly[] assemblies)
        {
            return AllClasses.GetAllTypes(assemblies);
        }
        public IEnumerable<Type> GetAllTypes(bool skipOnError, params Assembly[] assemblies)
        {
            return AllClasses.GetAllTypes(skipOnError, assemblies);
        }
        public IEnumerable<Type> GetAllTypes(IEnumerable<Assembly> assemblies, bool skipOnError = true)
        {
            return assemblies.GetAllTypes(skipOnError);
        }
        public IEnumerable<Type> GetAllTypes(Assembly assembly, bool skipOnError = true)
        {
            return assembly.GetAllTypes(skipOnError);
        }

        public IEnumerable<Type> GetAllClasses(IEnumerable<Type> types, Type baseType = null,
            bool? isConcrete = true, bool? isGenericDefinition = false, bool skipOnError = true)
        {
            return types.GetAllClasses(baseType, isConcrete, isGenericDefinition, skipOnError);
        }
        public IEnumerable<Type> GetAllClasses<TBase>(IEnumerable<Type> types,
            bool? isConcrete = true, bool? isGenericDefinition = false, bool skipOnError = true)
        {
            return types.GetAllClasses<TBase>(isConcrete, isGenericDefinition, skipOnError);
        }

        public IEnumerable<Type> GetAllInterfaces(IEnumerable<Type> types, Type baseType = null, 
            bool? isGenericDefinition = false, bool skipOnError = true)
        {
            return types.GetAllInterfaces(baseType, isGenericDefinition, skipOnError);
        }
        public IEnumerable<Type> GetAllInterfaces<TBase>(IEnumerable<Type> types, 
            bool? isGenericDefinition = false, bool skipOnError = true)
        {
            return types.GetAllInterfaces<TBase>(isGenericDefinition, skipOnError);
        }
    }
}
