using System;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.SharpModels
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class ImplementedByAttribute : Attribute
    {
        private readonly Type _modelElementType;

        public ImplementedByAttribute(Type modelElementType)
        {
            if (modelElementType == null) throw new ArgumentNullException("modelElementType");
            if (!typeof(IModelElement).IsAssignableFrom(modelElementType)) throw new ArgumentException("modelElementType");
            if (!modelElementType.IsClass) throw new ArgumentException(string.Format("Type {0} is not a class so it cannot implement model interfaces", modelElementType));
            if (modelElementType.IsAbstract) throw new ArgumentException(string.Format("Class {0} is abstract so it cannot implement model interfaces", modelElementType));
            if (modelElementType.IsGenericTypeDefinition) throw new ArgumentException(string.Format("Class {0} is a generic definition so it cannot implement model interfaces", modelElementType));

            _modelElementType = modelElementType;
        }

        public Type ModelElementType
        {
            get { return _modelElementType; }
        }

        public static Type ElementIsImplementedBy(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");

            var attr = interfaceType.GetAttribute<ImplementedByAttribute>();
            if (attr != null) return attr.ModelElementType;
            return null;
        }

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ImplementsAttribute : Attribute
    {
        private readonly Type _modelElementType;

        public ImplementsAttribute(Type modelElementType)
        {
            if (modelElementType == null) throw new ArgumentNullException("modelElementType");
            if (!typeof(IModelElement).IsAssignableFrom(modelElementType)) throw new ArgumentException("modelElementType");
            if (!modelElementType.IsInterface) throw new ArgumentException(string.Format("Type {0} is not an interface", modelElementType));
            if (modelElementType.IsGenericTypeDefinition) throw new ArgumentException(string.Format("Interface {0} is a generic definition", modelElementType));

            _modelElementType = modelElementType;
        }

        public Type ModelElementType
        {
            get { return _modelElementType; }
        }

        public static Type ElementImplements(Type classType)
        {
            if (classType == null) throw new ArgumentNullException("classType");

            var attr = classType.GetAttribute<ImplementsAttribute>();
            if (attr != null) return attr.ModelElementType;
            return null;
        }

    }
}