using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public class ModelElementRegistry : IModelElementRegistry
    {
        private static readonly ModelElementRegistry _default = new ModelElementRegistry();
        public static ModelElementRegistry Default { get { return _default; } }

        #region [ Implementation ]

        private static readonly ConcurrentDictionary<Type, ClassInformation> ClassesRegisteredByInterface = new ConcurrentDictionary<Type, ClassInformation>();
        private static readonly ConcurrentDictionary<Type, ClassInformation> ClassesRegisteredByClass = new ConcurrentDictionary<Type, ClassInformation>();
        private static readonly ConcurrentDictionary<string, ClassInformation> ClassesRegisteredByUri = new ConcurrentDictionary<string, ClassInformation>();

        public ClassInformation GetClassInformation(Type type)
        {
            throw new NotImplementedException();
        }
        private static ClassInformation CreateClassInformation(Type type)
        {
            //var info = new ClassInformation();
            throw new NotImplementedException();
        }
        #endregion [ Implementation ]

        #region [ Implementation Classes ]

        #endregion [ Implementation Classes ]

        public TypeInformation GetTypeInformation(Type type)
        {
            throw new NotImplementedException();
        }

        public TypeInformation GetTypeInformation(string globalIdentifier)
        {
            throw new NotImplementedException();
        }

        public void RegisterTypes(IEnumerable<Type> typesToRegister)
        {
            throw new NotImplementedException();
        }

        public IModelElementTypeLocator Source { get; set; }
    }
}