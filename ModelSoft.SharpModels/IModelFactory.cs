using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.SharpModels
{
    public interface IModelFactory
    {
        IModelElement Create(string url, bool throwIfNull = true);
        IModelElement Create(Type modelType, bool throwIfNull = true);
        TModelElement Create<TModelElement>(bool throwIfNull = true);

        bool TryCreate(string url, out IModelElement instance);
        bool TryCreate(Type modelType, out IModelElement instance);
        bool TryCreate<TModelElement>(out TModelElement instance);
    }


}
