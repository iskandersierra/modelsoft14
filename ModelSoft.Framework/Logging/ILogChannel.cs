using System;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Logging
{
    public interface ILogChannel
    {
        bool IsEnabled { get; }

        bool IsErrorEnabled { get; }

        LogLevel Level { get; }
        
        LogLevel ErrorLevel { get; }

        void Log(IFormatProvider formatProvider, string message);

        void LogFormat(IFormatProvider formatProvider, string format, params object[] args);

        void LogError(IFormatProvider formatProvider, string message, [NotNull] Exception exception);
        
        void LogErrorFormat(IFormatProvider formatProvider, string format, [NotNull] Exception exception, params object[] args);
    }
}
