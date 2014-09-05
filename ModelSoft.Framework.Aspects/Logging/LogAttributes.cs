 
using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Logging
{

    [Serializable]
    public class LogWarnAttribute : LogAttribute
    {
        public LogWarnAttribute([NotNull] string channelName, LoggingType loggingType = LoggingType.EnterAndExit) 
            : base(channelName, loggingType, LogLevel.Warn, LogLevel.Error)
        {
        }

        public LogWarnAttribute(LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(loggingType, LogLevel.Warn, LogLevel.Error)
        {
        }

        public LogWarnAttribute(ChannelNameType channelType, LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(channelType, loggingType, LogLevel.Warn, LogLevel.Error)
        {
        }
    }

    [Serializable]
    public class LogInfoAttribute : LogAttribute
    {
        public LogInfoAttribute([NotNull] string channelName, LoggingType loggingType = LoggingType.EnterAndExit) 
            : base(channelName, loggingType, LogLevel.Info, LogLevel.Error)
        {
        }

        public LogInfoAttribute(LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(loggingType, LogLevel.Info, LogLevel.Error)
        {
        }

        public LogInfoAttribute(ChannelNameType channelType, LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(channelType, loggingType, LogLevel.Info, LogLevel.Error)
        {
        }
    }

    [Serializable]
    public class LogDebugAttribute : LogAttribute
    {
        public LogDebugAttribute([NotNull] string channelName, LoggingType loggingType = LoggingType.EnterAndExit) 
            : base(channelName, loggingType, LogLevel.Debug, LogLevel.Error)
        {
        }

        public LogDebugAttribute(LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(loggingType, LogLevel.Debug, LogLevel.Error)
        {
        }

        public LogDebugAttribute(ChannelNameType channelType, LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(channelType, loggingType, LogLevel.Debug, LogLevel.Error)
        {
        }
    }

    [Serializable]
    public class LogTraceAttribute : LogAttribute
    {
        public LogTraceAttribute([NotNull] string channelName, LoggingType loggingType = LoggingType.EnterAndExit) 
            : base(channelName, loggingType, LogLevel.Trace, LogLevel.Error)
        {
        }

        public LogTraceAttribute(LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(loggingType, LogLevel.Trace, LogLevel.Error)
        {
        }

        public LogTraceAttribute(ChannelNameType channelType, LoggingType loggingType = LoggingType.EnterAndExit, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
            : base(channelType, loggingType, LogLevel.Trace, LogLevel.Error)
        {
        }
    }

}
 
