using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Logging
{
    public abstract class LogChannelProviderBase : ILogChannelProvider
    {
        private readonly ConcurrentDictionary<string, ILogChannel> _channels;
        private readonly Dictionary<string, ILogChannel> _channels2;
        private readonly bool _isThreadSafe;
        private readonly bool _useFullTypeName;
        private readonly string _defaultName;
        private readonly string _nameFormat;


        protected LogChannelProviderBase(
            bool useFullTypeName = false, 
            bool isThreadSafe = true, 
            [NotNull] string defaultChannelname = "Default", 
            [NotNull] string nameFormat = "{0}")
        {
            if (nameFormat.IsWS()) throw new ArgumentNullException("nameFormat");
            if (defaultChannelname.IsWS()) throw new ArgumentNullException("defaultChannelname");

            _isThreadSafe = isThreadSafe;
            _useFullTypeName = useFullTypeName;
            _defaultName = defaultChannelname;
            _nameFormat = nameFormat;

            if (_isThreadSafe)
                _channels = new ConcurrentDictionary<string, ILogChannel>(StringComparer.OrdinalIgnoreCase);
            else
                _channels2 = new Dictionary<string, ILogChannel>(StringComparer.OrdinalIgnoreCase);
        }

        public bool IsThreadSafe {
            get
            {
                return _isThreadSafe;
            }
        }

        public ILogChannel Default {
            get
            {
                return GetChannel(_defaultName);
            }
        }

        public ILogChannel GetChannel(string name, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
        {
            var channelName = string.Format(_nameFormat, name, level);
            if (_isThreadSafe)
            {
                var result = _channels.GetOrAdd(channelName, n => CreateChannel(n, level, errorLevel));
                return result;
            }
            else
            {
                ILogChannel result;
                if (!_channels2.TryGetValue(channelName, out result))
                {
                    result = CreateChannel(channelName, level, errorLevel);
                    _channels2.Add(channelName, result);
                }
                return result;
            }
        }

        public ILogChannel GetChannel([NotNull] Type type, LogLevel level, LogLevel errorLevel = LogLevel.Error)
        {
            if (type == null) throw new ArgumentNullException("type");

            var typeName = _useFullTypeName ? type.FullName : type.Name;
            return GetChannel(typeName, level, errorLevel);
        }

        protected abstract ILogChannel CreateChannel(string channelName, LogLevel level, LogLevel errorLevel);
    }
}