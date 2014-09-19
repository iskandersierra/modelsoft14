using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;

namespace ModelSoft.Framework.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(EnumerableTypeProxy<>))]
    public class CustomList<T> : NotifyPropertyChanged, INotifyCollectionChanged, IList<T>, IList
    {
        public const CustomListEvents DefaultEvents = CustomListEvents.All;
        public const bool DefaultRaiseEvents = true;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #region [ Variables ]
        private List<T> _list = new List<T>();
        private List<T> _previousList;
        private CustomListEvents _events = DefaultEvents;
        private bool _raiseEvents = DefaultRaiseEvents;
        #endregion

        #region [ Constructors ]
        public CustomList() : this(DefaultEvents, DefaultRaiseEvents) { }

        public CustomList(CustomListEvents events) : this(events, DefaultRaiseEvents) { }

        public CustomList(CustomListEvents events, bool raiseEvents)
        {
            _events = events;
            _raiseEvents = raiseEvents;
        }

        public CustomList(CustomListEvents events, bool raiseEvents, IEnumerable<T> values)
            : this(events, raiseEvents)
        {
            AddRange(values);
        }

        public CustomList(CustomListEvents events, bool raiseEvents, params T[] values)
            : this(events, raiseEvents)
        {
            AddRange(values);
        }

        public CustomList(CustomListEvents events, params T[] values)
            : this(events)
        {
            AddRange(values);
        }

        public CustomList(CustomListEvents events, IEnumerable<T> values)
            : this(events)
        {
            AddRange(values);
        }

        public CustomList(params T[] values)
            : this()
        {
            AddRange(values);
        }

        public CustomList(IEnumerable<T> values)
            : this()
        {
            AddRange(values);
        }
        #endregion

        #region [ Properties ]
        public bool RaiseEvents
        {
            get { return _raiseEvents; }
            protected set { _raiseEvents = value; }
        }

        public CustomListEvents Events
        {
            get { return _events; }
            protected set { _events = value; }
        }
        #endregion

        #region [ List Methods ]

        public void AddRange(IEnumerable<T> e)
        {
            if (e != null)
                e.ForEach(Add);
        }

        public void AddRange(params T[] objs)
        {
            AddRange(objs as IEnumerable<T>);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Insert(_list.Count, item);
        }

        int IList.Add(object value)
        {
            Add((T)value);
            return Count - 1;
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        public void Clear()
        {
            if (!BeforeClearInternal()) return;
            _previousList = _list;
            _list = new List<T>();
            AfterClearInternal();
            _previousList = null;
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            Check(item);
            int pos = IndexOf(item);
            int oldCount = Count;
            if (pos >= 0)
                RemoveAt(pos);
            return oldCount != Count;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((IList)_list).CopyTo(array, index);
        }

        public int Count
        {
            get { return _list.Count; }
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

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Check(item);
            if (!BeforeInsertInternal(item, index)) return;
            _list.Insert(index, item);
            AfterInsertInternal(item, index);
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            if (!BeforeRemoveInternal(item, index)) return;
            _list.RemoveAt(index);
            AfterRemoveInternal(item, index);
        }

        void IList<T>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                Check(value);
                var oldItem = _list[index];
                if (!BeforeSetInternal(value, index, oldItem)) return;
                _list[index] = value;
                AfterSetInternal(value, index, oldItem);
            }
        }

        #endregion

        #region [ Events ]

        protected virtual void Check(T item)
        {
        }

        public event EventHandler<CancelableItemPositionEventArgs<T, int>> BeforeEnter;
        private bool BeforeEnterInternal(T item, int pos)
        {
            if ((_events & CustomListEvents.BeforeEnter) != 0)
            {
                if (!OnBeforeEnter(item, pos)) return false;
                if (BeforeEnter != null)
                {
                    CancelableItemPositionEventArgs<T, int> args;
                    BeforeEnter(this, args = new CancelableItemPositionEventArgs<T, int>(item, pos));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<ItemPositionEventArgs<T, int>> AfterEnter;
        private void AfterEnterInternal(T item, int pos)
        {
            if ((_events & CustomListEvents.AfterEnter) != 0)
            {
                OnAfterEnter(item, pos);
                if (AfterEnter != null)
                    AfterEnter(this, new ItemPositionEventArgs<T, int>(item, pos));
            }
        }

        public event EventHandler<CancelableItemPositionEventArgs<T, int>> BeforeLeave;
        private bool BeforeLeaveInternal(T item, int pos)
        {
            if ((_events & CustomListEvents.BeforeLeave) != 0)
            {
                if (!OnBeforeLeave(item, pos)) return false;
                if (BeforeLeave != null)
                {
                    CancelableItemPositionEventArgs<T, int> args;
                    BeforeLeave(this, args = new CancelableItemPositionEventArgs<T, int>(item, pos));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<ItemPositionEventArgs<T, int>> AfterLeave;
        private void AfterLeaveInternal(T item, int pos)
        {
            if ((_events & CustomListEvents.AfterEnter) != 0)
            {
                OnAfterLeave(item, pos);
                if (AfterLeave != null)
                    AfterLeave(this, new ItemPositionEventArgs<T, int>(item, pos));
            }
        }

        public event EventHandler<CancelableItemPositionEventArgs<T, int>> BeforeInsert;
        private bool BeforeInsertInternal(T item, int pos)
        {
            if (!BeforeEnterInternal(item, pos)) return false;
            if ((_events & CustomListEvents.BeforeInsert) != 0)
            {
                if (!OnBeforeInsert(item, pos)) return false;
                if (BeforeInsert != null)
                {
                    CancelableItemPositionEventArgs<T, int> args;
                    BeforeInsert(this, args = new CancelableItemPositionEventArgs<T, int>(item, pos));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<ItemPositionEventArgs<T, int>> AfterInsert;
        private void AfterInsertInternal(T item, int pos)
        {
            AfterEnterInternal(item, pos);
            if ((_events & CustomListEvents.AfterInsert) != 0)
            {
                OnAfterInsert(item, pos);
                if (AfterInsert != null)
                    AfterInsert(this, new ItemPositionEventArgs<T, int>(item, pos));
            }
            OnPropertyChanged("Item");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, pos);
        }

        public event EventHandler<CancelableItemPositionEventArgs<T, int>> BeforeRemove;
        private bool BeforeRemoveInternal(T item, int pos)
        {
            if (!BeforeLeaveInternal(item, pos)) return false;
            if ((_events & CustomListEvents.BeforeRemove) != 0)
            {
                if (!OnBeforeRemove(item, pos)) return false;
                if (BeforeRemove != null)
                {
                    CancelableItemPositionEventArgs<T, int> args;
                    BeforeRemove(this, args = new CancelableItemPositionEventArgs<T, int>(item, pos));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<ItemPositionEventArgs<T, int>> AfterRemove;
        private void AfterRemoveInternal(T item, int pos)
        {
            AfterLeaveInternal(item, pos);
            if ((_events & CustomListEvents.AfterRemove) != 0)
            {
                OnAfterRemove(item, pos);
                if (AfterRemove != null)
                    AfterRemove(this, new ItemPositionEventArgs<T, int>(item, pos));
            }
            OnPropertyChanged("Item");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, pos);
        }

        public event EventHandler<CancelableSubstituteItemPositionEventArgs<T, int>> BeforeSet;
        private bool BeforeSetInternal(T item, int pos, T oldItem)
        {
            if (!BeforeLeaveInternal(oldItem, pos)) return false;
            if (!BeforeEnterInternal(item, pos)) return false;
            if ((_events & CustomListEvents.BeforeSet) != 0)
            {
                if (!OnBeforeSet(item, pos, oldItem)) return false;
                if (BeforeSet != null)
                {
                    CancelableSubstituteItemPositionEventArgs<T, int> args;
                    BeforeSet(this, args = new CancelableSubstituteItemPositionEventArgs<T, int>(item, pos, oldItem));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<SubstituteItemPositionEventArgs<T, int>> AfterSet;
        private void AfterSetInternal(T item, int pos, T oldItem)
        {
            AfterLeaveInternal(oldItem, pos);
            AfterEnterInternal(item, pos);
            if ((_events & CustomListEvents.AfterSet) != 0)
            {
                OnAfterSet(item, pos, oldItem);
                if (AfterSet != null)
                    AfterSet(this, new SubstituteItemPositionEventArgs<T, int>(item, pos, oldItem));
            }
            OnPropertyChanged("Item");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, item, oldItem, pos);
        }

        public event EventHandler<CancelableItemsEventArgs<T>> BeforeClear;
        private bool BeforeClearInternal()
        {
            if ((_events & CustomListEvents.BeforeLeave) != 0)
            {
                int count = 0;
                foreach (var item in _list)
                {
                    if (!BeforeLeaveInternal(item, count)) return false;
                    count++;
                }
            }
            if ((_events & CustomListEvents.BeforeClear) != 0)
            {
                if (!OnBeforeClear(_list)) return false;
                if (BeforeClear != null)
                {
                    CancelableItemsEventArgs<T> args;
                    BeforeClear(this, args = new CancelableItemsEventArgs<T>(_previousList));
                    return !args.Canceled;
                }
            }
            return true;
        }

        public event EventHandler<ItemsEventArgs<T>> AfterClear;
        private void AfterClearInternal()
        {
            if ((_events & CustomListEvents.AfterLeave) != 0)
            {
                int count = 0;
                foreach (var item in _previousList)
                {
                    AfterLeaveInternal(item, count);
                    count++;
                }
            }
            if ((_events & CustomListEvents.AfterClear) != 0)
            {
                OnAfterClear(_previousList);
                if (AfterClear != null)
                    AfterClear(this, new ItemsEventArgs<T>(_previousList));
            }
            OnPropertyChanged("Item");
            OnCollectionReset();
        }

        #endregion

        #region [ NotifyCollectionChanged ]

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, object oldItem, int pos)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, oldItem, pos));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion

        #region [ To Override ]

        protected List<T> InternalList
        {
            get { return _list; }
        }

        protected virtual bool OnBeforeEnter(T item, int pos)
        {
            return true;
        }

        protected virtual void OnAfterEnter(T item, int pos)
        {
        }

        protected virtual bool OnBeforeLeave(T item, int pos)
        {
            return true;
        }

        protected virtual void OnAfterLeave(T item, int pos)
        {
        }

        protected virtual bool OnBeforeInsert(T item, int pos)
        {
            return true;
        }

        protected virtual void OnAfterInsert(T item, int pos)
        {
        }

        protected virtual bool OnBeforeRemove(T item, int pos)
        {
            return true;
        }

        protected virtual void OnAfterRemove(T item, int pos)
        {
        }

        protected virtual bool OnBeforeSet(T item, int pos, T oldItem)
        {
            return true;
        }

        protected virtual void OnAfterSet(T item, int pos, T oldItem)
        {
        }

        protected virtual bool OnBeforeClear(IEnumerable<T> items)
        {
            return true;
        }

        protected virtual void OnAfterClear(IEnumerable<T> items)
        {
        }

        #endregion
    }

    internal sealed class EnumerableTypeProxy<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public EnumerableTypeProxy(IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            _enumerable = enumerable;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get { return _enumerable.ToArray(); }
        }
    }
}
