using System;

namespace ModelSoft.Framework.DomainObjects
{
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