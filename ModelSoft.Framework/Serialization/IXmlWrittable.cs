using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Serialization
{
  public interface IXmlWrittable
  {
    void WriteXml(XmlWriter writer);
  }
}
