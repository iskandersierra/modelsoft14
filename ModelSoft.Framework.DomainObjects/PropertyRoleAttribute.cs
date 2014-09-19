using System;

namespace ModelSoft.Framework.DomainObjects
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class PropertyRoleAttribute : Attribute
    {
    }
}