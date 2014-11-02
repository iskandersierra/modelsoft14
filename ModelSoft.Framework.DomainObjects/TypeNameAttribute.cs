using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class AssemblyBaseNameAttribute : Attribute
    {
        public AssemblyBaseNameAttribute(string baseName)
        {
            BaseName = baseName;
        }

        public string BaseName { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class TypeNameAttribute : Attribute
    {
        public TypeNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class AbstractTypeAttribute : Attribute
    {
    }
}
