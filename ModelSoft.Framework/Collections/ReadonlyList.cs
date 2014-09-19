using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.ComponentModel;

namespace ModelSoft.Framework.Collections
{
    public class ReadOnlyList<T> :
      NotifyPropertyChanged,
      INotifyCollectionChanged,
      IList<T>,
      IList
    {
        private CustomList<T> list;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ReadOnlyList(IEnumerable<T> values)
            : this(new CustomList<T>(values))
        {
        }

        public ReadOnlyList(ICollection<T> values)
            : this(new CustomList<T>(values))
        {
        }

        public ReadOnlyList(IList<T> values)
            : this(new CustomList<T>(values))
        {
        }

        public ReadOnlyList(CustomList<T> values)
        {
            list = values;
            list.PropertyChanged += list_PropertyChanged;
            list.CollectionChanged += list_CollectionChanged;
        }

        #region [ Private Methods ]

        private void ThrowCannotModified()
        {
            throw new InvalidOperationException("This list can't be modified");
        }

        private void list_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        private void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        #endregion

        #region [ List Methods ]

        public void AddRange(IEnumerable<T> e)
        {
            ThrowCannotModified();
        }

        public void AddRange(params T[] objs)
        {
            ThrowCannotModified();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            ThrowCannotModified();
        }

        int IList.Add(object value)
        {
            ThrowCannotModified();
            return -1;
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        public void Clear()
        {
            ThrowCannotModified();
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            ThrowCannotModified();
        }

        void IList.Remove(object value)
        {
            ThrowCannotModified();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            ThrowCannotModified();
            return false;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((IList)list).CopyTo(array, index);
        }

        public int Count
        {
            get { return list.Count; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ThrowCannotModified();
        }

        public void RemoveAt(int index)
        {
            ThrowCannotModified();
        }

        void IList<T>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                ThrowCannotModified();
            }
        }

        #endregion
    }
}
