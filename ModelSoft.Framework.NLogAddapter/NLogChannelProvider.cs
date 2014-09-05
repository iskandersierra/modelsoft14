using ModelSoft.Framework.Logging;

namespace ModelSoft.Framework.NLogAddapter
{
    public class NLogChannelProvider : LogChannelProviderBase
    {
        public NLogChannelProvider(
            bool useFullTypeName = false,
            bool isThreadSafe = true,
            string defaultChannelname = "Default",
            string nameFormat = "{0}")
            : base(useFullTypeName, isThreadSafe, defaultChannelname, nameFormat)
        {
        }

        protected override ILogChannel CreateChannel(string channelName, LogLevel level, LogLevel errorLevel)
        {
            var nlog = NLog.LogManager.GetLogger(channelName);
            var result = new NLogChannel(nlog, level, errorLevel);
            return result;
        }
    }
}