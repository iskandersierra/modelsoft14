using System;
using ModelSoft.Framework.Logging;
using NLog;
using LogLevel = ModelSoft.Framework.Logging.LogLevel;

namespace ModelSoft.Framework.NLogAddapter
{
    public class NLogChannel : ILogChannel
    {
        private readonly Logger _nlog;
        private readonly LogLevel _level;
        private readonly LogLevel _errorLevel;
        private readonly NLog.LogLevel _nlogLevel;
        private readonly NLog.LogLevel _nlogErrorLevel;


        public NLogChannel(Logger nlog, LogLevel level, LogLevel errorLevel)
        {
            if (nlog == null) throw new ArgumentNullException("nlog");
            _nlog = nlog;
            _level = level;
            _errorLevel = errorLevel;
            _nlogLevel = ConvertLabel(_level);
            _nlogErrorLevel = ConvertLabel(_errorLevel);
        }

        public bool IsEnabled
        {
            get 
            {
                return _nlog.IsEnabled(_nlogLevel);
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return _nlog.IsEnabled(_nlogErrorLevel);
            }
        }

        public LogLevel Level { get { return _level; } }

        public LogLevel ErrorLevel { get { return _errorLevel; } }

        public void Log(IFormatProvider formatProvider, string message)
        {
            _nlog.Log(_nlogLevel, formatProvider, message);
        }

        public void LogFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            var message = string.Format(formatProvider, format, args);
            Log(formatProvider, message);
        }

        public void LogError(IFormatProvider formatProvider, string message, Exception exception)
        {
            _nlog.Log(_nlogErrorLevel, formatProvider, message, exception);
        }

        public void LogErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            var message = string.Format(formatProvider, format, args);
            LogError(formatProvider, message, exception);
        }

        private NLog.LogLevel ConvertLabel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
