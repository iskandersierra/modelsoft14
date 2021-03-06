﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectManagement
{
  public interface IClassDescription : 
    IDataTypeDescription
  {
    IPropertyDescriptionCollection Properties { get; }

    IClassDescriptionCollection BaseClasses { get; }
  }
}
