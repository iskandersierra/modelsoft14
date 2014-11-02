using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class CommonInformation : ICommonInformation
    {
        protected CommonInformation(IModelElementInformationProvider informationProvider)
        {
            if (informationProvider == null) throw new ArgumentNullException("informationProvider");

            InformationProvider = informationProvider;
        }

        public IModelElementInformationProvider InformationProvider { get; private set; }

        public abstract void Format(IndentedTextWriter writer);
    }
}