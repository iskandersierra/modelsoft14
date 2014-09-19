using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OppositePropertyAttribute : Attribute
    {
        public OppositePropertyAttribute(string propertyName)
        {
            propertyName.ValidateCSharpIdentifier();
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
