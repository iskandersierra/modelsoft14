using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.DomainObjects
{
    public class FieldData : IFieldData
    {
        private string name;
        private object fieldValue;
        private readonly object defaultValue;
        private bool isSet;

        public FieldData([NotNull] IFieldHost host, string name, object defaultValue)
        {
            if (host == null) throw new ArgumentNullException("host");
            Host = host;
            this.defaultValue = defaultValue;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        protected object InnerValue
        {
            get
            {
                return fieldValue;
            }
            set
            {
                if (object.Equals(fieldValue, value)) return;
                fieldValue = value;
                isSet = true;
            }
        }

        public bool IsSet { get { return isSet; } }

        public void Reset()
        {
            if (isSet)
            {
                object oldValue = fieldValue;
                fieldValue = defaultValue;
                isSet = false;
                Host.OnFieldChanged(this, oldValue, fieldValue);
            }
        }

        public IFieldHost Host { get; private set; }

        object IFieldData.UntypedValue
        {
            get { return InnerValue; }
            set { InnerValue = value; }
        }
    }

    public class FieldData<T> : FieldData
    {
        public FieldData([NotNull] IFieldHost host, string name, object defaultValue) 
            : base(host, name, defaultValue)
        {
        }

        public T Value
        {
            get
            {
                return (T) InnerValue;
            }
            set
            {
                InnerValue = value;
            }
        }
    }
}