using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public interface IPersistentStructure : IDisposable
  {
    bool AutoSave { get; set; }

    bool Modified { get; }

    void Load();

    void Save();
  }
}
