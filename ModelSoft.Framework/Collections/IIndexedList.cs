using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Collections
{
// ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IIndexedList<TKey, TValue> :
        IList<TValue>,
        IDictionary<TKey, TValue>
    {
        TKey KeyOf(TValue value);

        int IndexOfKey(TKey key);

        void MakeReadOnly();

        IEnumerator<TValue> GetEnumerator();
    }

    public class IndexedList<TKey, TValue> : 
        IIndexedList<TKey, TValue>
    {
        #region [ Constructors ]

        public IndexedList(Func<TValue, TKey> keySelector, IComparer<TKey> keyComparer = null)
        {
            keySelector.RequireNotNull("keySelector");
            if (keyComparer == null) keyComparer = Comparer<TKey>.Default;

            _keyComparer = keyComparer;
            _keySelector = keySelector;
            _keys = new List<TKey>();
            _values = new List<TValue>();
        }
        #endregion

        #region [ Queries ]
        public IEnumerator<TValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _keys.Enumerate(_values)
                .Select(e => KeyValuePair.Create(e.Item1, e.Item2))
                .CopyTo(array, arrayIndex);
        }

        public TKey KeyOf(TValue value)
        {
            var key = _keySelector(value);
            return key;
        }

        public int Count
        {
            get { return _keys.Count; }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public bool Contains(TValue item)
        {
            var key = _keySelector(item);
            return ContainsKey(key);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public int IndexOf(TValue item)
        {
            var pos = 0;
            foreach (var value in _values)
            {
                if (object.Equals(value, item))
                    return pos;
                pos++;
            }
            return -1;
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public int IndexOfKey(TKey key)
        {
            var pos = 0;
            foreach (var k in _keys)
            {
                if (_keyComparer.Compare(k, key) == 0)
                    return pos;
                pos++;
            }
            return -1;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public ICollection<TKey> Keys {
            get { return _keysCollection; }
        }

        public ICollection<TValue> Values
        {
            get { return this; }
        }

        #endregion

        #region [ Indexers ]
        public TValue this[int index]
        {
            get { return _values[index]; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TValue this[TKey key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Operations ]
        public void MakeReadOnly()
        {
            _isReadOnly = true;
        }

        public void Clear()
        {
            CheckReadonly();

            var remainList = new List<KeyValuePair<TKey, TValue>>();
            var removeList = new List<KeyValuePair<TKey, TValue>>();
            foreach (var pair in ((IDictionary<TKey, TValue>)this))
            {
                if (!CanLeave(pair.Key, pair.Value))
                    remainList.Add(KeyValuePair.Create(pair.Key, pair.Value));
                else
                    removeList.Add(KeyValuePair.Create(pair.Key, pair.Value));
            }

            _keys.Clear();
            _values.Clear();  

            foreach (var pair in removeList)
            {
                OnLeave(pair.Key, pair.Value);
            }

            foreach (var pair in remainList)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }

        public void Add(TValue item)
        {
            Insert(_keys.Count, item);
        }

        public bool Remove(TValue item)
        {
            CheckReadonly();
            var key = _keySelector(item);
            return Remove(key);
        }

        public void Insert(int index, TValue item)
        {
            CheckReadonly();
            var key = _keySelector(item);
            if (CanEnter(key, item))
            {
                _keys.Insert(index, key);
                _values.Insert(index, item);
                OnEnter(key, item);
            }
        }

        public void RemoveAt(int index)
        {
            
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            int pos = IndexOfKey(key);
            if (pos >= 0)
            {
                var count = Count;
                RemoveAt(pos);
                return count != Count;
            }
            return false;
        }

        #endregion

        #region [ Fields ]
        private readonly IComparer<TKey> _keyComparer;
        private readonly Func<TValue, TKey> _keySelector;
        private readonly List<TKey> _keys;
        private readonly List<TValue> _values;
        private bool _isReadOnly;
        private ICollection<TKey> _keysCollection;

        #endregion

        #region [ Internal ]
        protected void CheckReadonly()
        {
            if (_isReadOnly)
                throw new InvalidOperationException("Collection is readonly");
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _keys.Enumerate(_values)
                .Select(e => KeyValuePair.Create(e.Item1, e.Item2))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            if (_keyComparer.Compare(_keySelector(item.Value), item.Key) == 0)
                Add(item.Value);
            else
                throw new InvalidOperationException("Key do not correspond with value");
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return this.TryGetValue(item.Key, out value) &&
                   object.Equals(value, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (this.TryGetValue(item.Key, out value) && object.Equals(value, item.Value))
                return Remove(item.Key);
            return false;
        }
        #endregion

        #region [ Overrideable Events ]

        protected virtual void OnEnter(TKey key, TValue value)
        {

        }

        protected virtual bool CanEnter(TKey key, TValue value)
        {
            return true;
        }

        protected virtual void OnLeave(TKey key, TValue value)
        {

        }

        protected virtual bool CanLeave(TKey key, TValue value)
        {
            return true;
        }

        #endregion
    }
}
