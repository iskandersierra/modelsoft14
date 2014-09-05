using System.Collections.Generic;

namespace ModelSoft.Framework
{
  public static class KeyValuePair
  {
    public static KeyValuePair<K, T> Create<K, T>(K key, T value)
    {
      return new KeyValuePair<K, T>(key, value);
    }
  }
}
