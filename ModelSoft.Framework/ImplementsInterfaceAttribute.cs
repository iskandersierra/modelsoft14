using System;
using System.Linq;

namespace ModelSoft.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class ImplementsInterfaceAttribute : Attribute
    {
        private Type[] implementedInterfaces;

        public ImplementsInterfaceAttribute(params Type[] implementedInterfaces)
        {
            if (implementedInterfaces == null) throw new ArgumentNullException("implementedInterfaces");
            if (implementedInterfaces.Any(e => e == null)) throw new ArgumentNullException("implementedInterfaces");

            this.implementedInterfaces = implementedInterfaces;
        }

        public Type[] ImplementedInterfaces
        {
            get { return implementedInterfaces; }
        }
    }
}
