using System;
using System.Collections.Generic;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.DomainObjects;

namespace ModelSoft.Core
{
    public interface IUriSpace : IWithUri
    {
        [Content]
        IIndexedList<string, IWithUri> NamedElements { get; }
    }
}