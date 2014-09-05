using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Collections
{
  public interface ITrie :
    IDictionary
  {
    IEnumerable<KeyValuePair<object, object>> Track(object key, out bool found, bool activeOnly = true);
  }

  public interface ITrie<TKey, TSymbol, TValue> :
    ITrie,
    IDictionary<TKey, TValue>
  {
    IEnumerable<KeyValuePair<TKey, TValue>> Track(TKey key, out bool found, bool activeOnly = true);
  }
}
