using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.Serialization
{
    public class MSCoreLibStringSerializer :
      IStringSerializer
    {
        private static MSCoreLibStringSerializer _Default;
        public static MSCoreLibStringSerializer Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new MSCoreLibStringSerializer();
                }
                return _Default;
            }
        }

        public string SerializeToString(object value)
        {
            if (value == null) return null;

            if (value is Uri) return UriToString((Uri)value);

            throw new ArgumentOutOfRangeException("value", "Value type not supported: " + value.GetType().ToString());
        }

        public object DeserializeFromString(string serializedValue, Type targetType)
        {
            targetType.RequireNotNull("targetType");

            if (serializedValue == null)
                if (targetType.IsValueType)
                    throw new ArgumentNullException("Cannot convert null to a value type: " + targetType.ToString());
                else
                    return null;

            if (targetType == typeof(Uri)) return StringToUri(serializedValue);

            throw new ArgumentOutOfRangeException("value", "Value type not supported: " + targetType.ToString());
        }

        private string UriToString(Uri value)
        {
            return value.OriginalString;
        }

        private Uri StringToUri(string serializedValue)
        {
            return new Uri(serializedValue, UriKind.RelativeOrAbsolute);
        }

        public IEnumerable<Type> SerializableTypes
        {
            get { return _SerializableTypes; }
        }

        private static readonly Type[] _SerializableTypes = new[]
        {
            typeof(Uri),
        };
    }
}
