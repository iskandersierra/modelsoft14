﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.DomainObjects
{
    public interface IModel : IModelElement, IWithUri
    {
        IList<IModelElement> ModelElements { get; }
    }
}
