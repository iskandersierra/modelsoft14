namespace ModelSoft.Framework
{
    public abstract class FreezableBase :
      IFreezable
    {
        private bool _isFrozen;
        public bool IsFrozen
        {
            get { return _isFrozen; }
            set
            {
                if (_isFrozen == value) return;
                if (_isFrozen && !value)
                    throw new InvalidFreezingOperationException("A frozen object cannot be defrosted");
                Freeze();
            }
        }

        public void Freeze()
        {
            if (!_isFrozen)
            {
                OnFreeze();
                this._isFrozen = true;
            }
        }

        protected virtual void OnFreeze()
        {
        }

        protected void CheckModifyFrozen()
        {
            if (_isFrozen)
                throw new InvalidFreezingOperationException("Cannot modify a frozen object");
        }
    }
}
