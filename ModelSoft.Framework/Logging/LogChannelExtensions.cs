using System;
using System.Globalization;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Logging
{
    public static class LogChannelExtensions
    {
        public static void Log([NotNull] this ILogChannel channel, string message)
        {
            if (channel == null) throw new ArgumentNullException("channel");
            channel.Log(CultureInfo.CurrentCulture, message);
        }

        public static void LogFormat([NotNull] this ILogChannel channel, string message, params object[] args)
        {
            if (channel == null) throw new ArgumentNullException("channel");
            channel.LogFormat(CultureInfo.CurrentCulture, message, args);
        }

        public static void LogErrorFormat([NotNull] this ILogChannel channel, string message, [NotNull] Exception exception, params object[] args)
        {
            if (channel == null) throw new ArgumentNullException("channel");
            if (exception == null) throw new ArgumentNullException("exception");
            channel.LogErrorFormat(CultureInfo.CurrentCulture, message, exception, args);
        }

        public static void LogError([NotNull] this ILogChannel channel, string message,
            [NotNull] Exception exception)
        {
            if (channel == null) throw new ArgumentNullException("channel");
            if (exception == null) throw new ArgumentNullException("exception");
            channel.LogError(CultureInfo.CurrentCulture, message, exception);
        }

        public static void LogError([NotNull] this ILogChannel channel, Exception exception)
        {
            if (channel == null) throw new ArgumentNullException("channel");
            if (exception == null) throw new ArgumentNullException("exception");
            channel.LogError(exception.Message, exception);
        }

        public static ILogChannel GetChannel<T>([NotNull] this ILogChannelProvider logProvider, LogLevel level = LogLevel.Info, LogLevel errorLevel = LogLevel.Error)
        {
            if (logProvider == null) throw new ArgumentNullException("logProvider");
            return logProvider.GetChannel(typeof(T), level, errorLevel);
        }
    }
}