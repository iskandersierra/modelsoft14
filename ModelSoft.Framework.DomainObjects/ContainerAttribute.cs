using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class PropertyRoleAttribute : Attribute
    {
    }

    public class ContentAttribute : PropertyRoleAttribute
    {
    }

    public class ContainerAttribute : PropertyRoleAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ComputedAttribute : Attribute
    {
        //private readonly Type _type;
        //private readonly string _methodName;

        //public ComputedAttribute(Type type, string methodName)
        //{
        //    _type = type;
        //    _methodName = methodName;
        //}

        //public Type Type
        //{
        //    get { return _type; }
        //}

        //public string MethodName
        //{
        //    get { return _methodName; }
        //}
    }
}
