using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModelSoft.Framework.DomainObjects.Properties;

namespace ModelSoft.Framework.DomainObjects
{
    public abstract class CollectionBase<TValue> : 
        ICollection<TValue>, 
        ICollection
    {
        protected CollectionBase()
        {
            IsReadOnly = false;
        }

        public abstract IEnumerator<TValue> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TValue item)
        {
            CheckModifyCollection();
            CheckItem(item);
            if (CanEnter(item))
            {
                AddInternal(item);
                OnEnter(item);
            }
        }

        public void Clear()
        {
            CheckModifyCollection();
            if (CanClear())
            {
                var itemsToClear = this.ToArray();
                ClearInternal();
                itemsToClear.ForEach(OnLeave);
            }
        }

        public abstract bool Contains(TValue item);

        public virtual void CopyTo(TValue[] array, int arrayIndex)
        {
            this.AsEnumerable().CopyTo(array, arrayIndex);
        }

        public bool Remove(TValue item)
        {
            CheckModifyCollection();
            CheckItem(item);
            if (CanLeave(item))
            {
                if (RemoveInternal(item))
                {
                    OnLeave(item);
                    return true;
                }
            }
            return false;
        }

        public virtual void CopyTo(Array array, int index)
        {
            this.AsEnumerable().CopyToEx(array, index);
        }

        public abstract int Count { get; }
        public bool IsReadOnly { get; protected set; }
        public abstract object SyncRoot { get; }
        public abstract bool IsSynchronized { get; }

        #region [ Internals to override ]
        protected virtual void CheckModifyCollection()
        {
            if (IsReadOnly)
                throw new InvalidOperationException(Resources.CollectionIsReadonly);
        }
        protected abstract void CheckItem(TValue item);
        protected abstract void AddInternal(TValue item);

        protected virtual bool CanEnter(TValue item)
        {
            return true;
        }

        protected void OnEnter(TValue item)
        {
        }

        protected abstract bool RemoveInternal(TValue item);

        protected virtual bool CanLeave(TValue item)
        {
            return true;
        }

        protected void OnLeave(TValue item)
        {
        }

        protected virtual bool CanClear()
        {
            return true;
        }

        protected abstract void ClearInternal();
        #endregion [ Internals to override ]

    }
}