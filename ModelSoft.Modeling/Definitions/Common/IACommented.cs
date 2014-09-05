﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Modeling.Definitions.Common
{
  public interface IACommented :
    IModelElement
  {
    [RelationshipType(ERelationshipType.Content)]
    IModelElementCollection<IAComment> Comments { get; }
  }
}
