using System;

namespace ModelSoft.Framework.Logging
{
    public class NullLogChannelProvider : ILogChannelProvider
    {
        private static ILogChannelProvider _null;

        public static ILogChannelProvider Null
        {
            get { return _null ?? (_null = new NullLogChannelProvider()); }
        }

        public NullLogChannelProvider()
        {
            Default = new NullLogChannel();
        }

        public bool IsThreadSafe { get { return true; } }

        public ILogChannel Default { get; private set; }

        public ILogChannel GetChannel(string name, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
        {
            return Default;
        }

        public ILogChannel GetChannel(Type type, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
        {
            return Default;
        }

        class NullLogChannel : ILogChannel
        {
            public bool IsEnabled { get { return false; } }
            public bool IsErrorEnabled { get { return false; } }
            public LogLevel Level { get { return LogLevel.Info; } }
            public LogLevel ErrorLevel { get { return LogLevel.Error; } }
            public void Log(IFormatProvider formatProvider, string message)
            {
            }

            public void LogFormat(IFormatProvider formatProvider, string format, params object[] args)
            {
            }

            public void LogError(IFormatProvider formatProvider, string message, Exception exception)
            {
            }

            public void LogErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
            {
            }
        }

    }
}