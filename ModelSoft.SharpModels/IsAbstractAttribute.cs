using System;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.SharpModels
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class IsAbstractAttribute : Attribute
    {
        private readonly bool _isAbstract;

        public bool IsAbstract
        {
            get { return _isAbstract; }
        }

        public IsAbstractAttribute() : this(true)
        {
        }

        public IsAbstractAttribute(bool isAbstract)
        {
            _isAbstract = isAbstract;
        }

        public static bool ElementIsAbstract(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");

            var attr = interfaceType.GetAttribute<IsAbstractAttribute>();
            return attr != null && attr.IsAbstract;
        }
    }
}