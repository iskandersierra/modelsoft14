using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xaml;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ModelSoft.Modeling")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ModelSoft.Modeling")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ea8180dd-e25c-4ba4-8ce8-fd2514d8ed15")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


[assembly: XmlnsPrefix("http://imodelsoft.com/modeling", "mmx")]
[assembly: XmlnsPrefix("http://imodelsoft.com/modeling/definitions", "mm")]

[assembly: XmlnsDefinition("http://imodelsoft.com/modeling", "ModelSoft.Modeling")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Common")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Common.ImplPOCOS")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Core.MM0")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Core.MM0.ImplPOCOS")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Core.Expressions")]
[assembly: XmlnsDefinition("http://imodelsoft.com/modeling/definitions", "ModelSoft.Modeling.Definitions.Core.Expressions.ImplPOCOS")]

