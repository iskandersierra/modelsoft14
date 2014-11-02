using System.Collections.Generic;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class IdentifiedCollection<TKey, TValue> :
        CollectionBase<TValue>,
        IIdentifiedCollection<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                TValue result;
                if (!TryGetValue(key, out result))
                    throw new KeyNotFoundException("Key = " + key);
                return result;
            }
        }

        public abstract TKey KeyOf(TValue value);

        public virtual bool ContainsKey(TKey key)
        {
            TValue value;
            return TryGetValue(key, out value);
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var item in this)
            {
                var itemKey = KeyOf(item);
                if (KeyAreEquals(itemKey, key))
                {
                    value = item;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        protected abstract bool KeyAreEquals(TKey key1, TKey key2);

        bool IIdentifiedCollection.ContainsKey(object key)
        {
            return key is TKey && ContainsKey((TKey)key);
        }

        bool IIdentifiedCollection.TryGetValue(object key, out object value)
        {
            TValue value1;
            var found = TryGetValue((TKey)key, out value1);
            value = value1;
            return found;
        }

        object IIdentifiedCollection.KeyOf(object value)
        {
            return KeyOf((TValue)value);
        }

        object IIdentifiedCollection.this[object key]
        {
            get
            {
                return this[(TKey)key];
            }
        }
    }
}
