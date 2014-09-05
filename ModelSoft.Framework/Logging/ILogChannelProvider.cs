using System;

namespace ModelSoft.Framework.Logging
{
    public interface ILogChannelProvider
    {
        bool IsThreadSafe { get; }

        ILogChannel Default { get; }

        ILogChannel GetChannel(string name, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error);

        ILogChannel GetChannel(Type type, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error);
    }
}