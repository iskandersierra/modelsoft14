using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using ModelSoft.Framework.Annotations;
using ModelSoft.Framework.Reflection;
using PostSharp.Aspects;

namespace ModelSoft.Framework.Logging
{
    [Serializable]
    public class LogAttribute : MethodInterceptionAspect
    {
        public LogAttribute([NotNull] string channelName,
            LoggingType loggingType = LoggingType.EnterAndExit,
            LogLevel level = LogLevel.Info,
            LogLevel errorLevel = LogLevel.Error)
            : this()
        {
            if (channelName.IsWS())
                throw new ArgumentNullException("channelName");
            LoggingType = loggingType;
            ChannelName = channelName;
            ChannelNameType = ChannelNameType.Name;
            Level = level;
            ErrorLevel = errorLevel;
        }

        public LogAttribute(
            LoggingType loggingType = LoggingType.EnterAndExit,
            LogLevel level = LogLevel.Info,
            LogLevel errorLevel = LogLevel.Error)
            : this()
        {
            LoggingType = loggingType;
            ChannelNameType = ChannelNameType.Default;
            Level = level;
            ErrorLevel = errorLevel;
        }

        public LogAttribute(ChannelNameType channelType,
            LoggingType loggingType = LoggingType.EnterAndExit,
            LogLevel level = LogLevel.Info,
            LogLevel errorLevel = LogLevel.Error)
            : this()
        {
            if (channelType == ChannelNameType.Name)
                throw new ArgumentOutOfRangeException("channelType", channelType, "Channel type cannot be by Name. Use another constructor to create the attribute.");
            LoggingType = loggingType;
            ChannelNameType = channelType;
            Level = level;
            ErrorLevel = errorLevel;
        }

        private LogAttribute()
        {
            ShowParameters = true;
            ShowReturnValue = true;
            MeasureTime = false;
            ShowParametersName = false;
            ShowParametersType = false;
            MaxToStringLength = 100;
            MaxListLength = 5;
            MaxListDepth = 1;
            ShowInstanceType = true;
            ShowFullTypeName = false;
            ServiceLocation = AspectServiceLocation.AspectManagerDefault;
        }

        public string ChannelName { get; private set; }
        public LoggingType LoggingType { get; private set; }
        public LogLevel Level { get; private set; }
        public LogLevel ErrorLevel { get; private set; }

        public bool ShowParameters { get; set; }

        public bool ShowParametersName { get; set; }

        public bool ShowParametersType { get; set; }

        public int MaxToStringLength { get; set; }

        public int MaxListLength { get; set; }

        public int MaxListDepth { get; set; }

        public bool ShowReturnValue { get; set; }

        public bool ShowInstanceType { get; set; }

        public bool ShowFullTypeName { get; set; }

        public bool MeasureTime { get; set; }

        public ChannelNameType ChannelNameType { get; set; }

        public AspectServiceLocation ServiceLocation { get; set; }

        public override bool CompileTimeValidate(MethodBase method)
        {
            if (method.IsAbstract) return false;
            return base.CompileTimeValidate(method);
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            base.RuntimeInitialize(method);

            _method = method;
            _declaringType = method.DeclaringType;
        }

        [NonSerialized]
        private Type _declaringType;
        protected Type DeclaringType
        {
            get { return _declaringType; }
        }

        [NonSerialized]
        private MethodBase _method;
        protected MethodBase Method
        {
            get { return _method; }
        }

        [NonSerialized]
        private ILogChannelProvider _logChannelProvider;
        protected ILogChannelProvider LogChannelProvider
        {
            get { return _logChannelProvider; }
        }

        [NonSerialized]
        private ILogChannel _logChannel;
        protected ILogChannel LogChannel
        {
            get { return _logChannel; }
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var logChannel = GetLogChannel(ShowInstanceType, LogChannel, args.Instance);
            switch (LoggingType)
            {
                case LoggingType.EnterAndExit:
                    OnOnvokeEnterAndExit(args, logChannel);
                    break;
                case LoggingType.Enter:
                    OnOnvokeEnter(args, logChannel);
                    break;
                case LoggingType.UnhandledException:
                    OnOnvokeUnhandledException(args, logChannel);
                    break;
                default:
                    base.OnInvoke(args);
                    break;
            }
        }

        private void OnOnvokeUnhandledException(MethodInterceptionArgs args, ILogChannel logChannel)
        {
            if (logChannel.IsErrorEnabled)
            {
                try
                {
                    base.OnInvoke(args);
                }
                catch (Exception ex)
                {
                    var message = GetExitMessage(args);
                    logChannel.LogError(message, ex);
                    throw;
                }
            }
            else
            {
                base.OnInvoke(args);
            }
        }

        private void OnOnvokeEnter(MethodInterceptionArgs args, ILogChannel logChannel)
        {
            if (logChannel.IsEnabled)
            {
                var message = GetEnterMessage(args);
                logChannel.Log(message);
            }
            base.OnInvoke(args);
        }

        private void OnOnvokeEnterAndExit(MethodInterceptionArgs args, ILogChannel logChannel)
        {
            if (logChannel.IsEnabled)
            {
                try
                {
                    var message = GetEnterMessage(args);
                    logChannel.Log(message);

                    base.OnInvoke(args);

                    var exitMessage = GetExitMessage(args);
                    logChannel.Log(exitMessage);
                }
                catch (Exception ex)
                {
                    var message = GetExitMessage(args);
                    logChannel.LogError(message, ex);
                    throw;
                }
            }
            else
            {
                base.OnInvoke(args);
            }
        }

        private ILogChannelProvider GetLogProvider()
        {
            ILogChannelProvider logProvider;
            switch (ServiceLocation)
            {
                case AspectServiceLocation.CommonServiceLocator:
                {
                    var serviceLocatorType = Type.GetType("Microsoft.Practices.ServiceLocation.ServiceLocator", true);
                    dynamic serviceLocator = serviceLocatorType.InvokeMember("Current",
                        BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static, null, null, null);
                    logProvider = serviceLocator.GetInstance(typeof(ILogChannelProvider));
                }
                    break;
                case AspectServiceLocation.AspectManagerDefault:
                    logProvider = LogManager.Default;
                    break;
                default:
                    logProvider = new NullLogChannelProvider();
                    break;
            }
            return logProvider;
        }

        private ILogChannel GetLogChannel(bool showInstanceType, ILogChannel fallbackLogChannel, object instance)
        {
            if (_logChannelProvider == null)
            {
                _logChannelProvider = GetLogProvider();
                _logChannel = GetLogChannel(false, null, null);
            }

            switch (ChannelNameType)
            {
                case ChannelNameType.Default:
                    return LogChannelProvider.Default;
                case ChannelNameType.Name:
                    return LogChannelProvider.GetChannel(ChannelName, Level, ErrorLevel);
                case ChannelNameType.Type:
                {
                    if (!showInstanceType || instance == null)
                        return fallbackLogChannel ?? LogChannelProvider.GetChannel(GetTypeName(DeclaringType), Level, ErrorLevel);
                    var type = instance.GetType();
                    var typeName = ShowFullTypeName ? type.FullName : type.Name;
                    return LogChannelProvider.GetChannel(typeName, Level, ErrorLevel);
                }
                case ChannelNameType.Method:
                {
                    if (!showInstanceType || instance == null)
                        return fallbackLogChannel ??
                               LogChannelProvider.GetChannel(GetMethodName(DeclaringType), Level, ErrorLevel);
                    var type = instance.GetType();
                    return LogChannelProvider.GetChannel(GetMethodName(type), Level, ErrorLevel);
                }
            }
            return NullLogChannelProvider.Null.Default;
        }

        private string GetEnterMessage(MethodInterceptionArgs args)
        {
            var sb = new StringBuilder();

            sb.Append("ENTER ")
                .Append(GetMethodName(args.Instance));

            if (ShowParameters)
            {
                var parameters = args.Method.GetParameters();
                sb.Append("(");
                for (int i = 0; i < args.Arguments.Count; i++)
                {
                    if (i > 0) sb.Append(", ");
                    var value = args.Arguments[i];
                    var text = GetValueToString(value, MaxListDepth);
                    if (ShowParametersName)
                        sb.Append(parameters[i].Name).Append(" = ");
                    sb.Append(text);
                    if (ShowParametersType && value != null)
                        sb.Append(" <").Append(value.GetType().GetPlainName()).Append(">");
                }
                sb.Append(")");
            }

            return sb.ToString();
        }

        private string GetExitMessage(MethodInterceptionArgs args)
        {
            var sb = new StringBuilder();

            sb.Append("LEAVE ")
                .Append(GetMethodName(args.Instance));

            if (ShowReturnValue && args.Method is MethodInfo && ((MethodInfo)args.Method).ReturnType != CommonTypes.TypeOfVoid)
            {
                sb.Append(" RETURN ");
                var value = args.ReturnValue;
                var text = GetValueToString(value, MaxListDepth);
                sb.Append(text);
                if (ShowParametersType && value != null)
                    sb.Append(" <").Append(value.GetType().GetPlainName()).Append(">");
            }

            if (MeasureTime)
            {

            }

            return sb.ToString();
        }

        private string GetValueToString(object value, int depth)
        {
            if (value == null) return "<null>";
            var str = value as string;
            if (str != null) return @"""{0}""".Fmt(str.CutWithPeriods(MaxToStringLength));
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                if (depth <= 0) return "[...]";
                return enumerable.Cast<object>()
                    .ToStringList(", ",
                        o => GetValueToString(o, depth - 1), opening: "[", closing: "]", empty: "[]",
                        maxElementCount: MaxListLength);
            }
            return value.ToString().CutWithPeriods(MaxToStringLength);
        }

        private string GetMethodName(object instance)
        {
            return GetMethodName(instance == null ? DeclaringType : instance.GetType());
        }

        private string GetMethodName(Type type)
        {
            return "{0}.{1}".Fmt(GetTypeName(type), Method.Name);
        }

        private string GetTypeName(Type type)
        {
            return type == null 
                ? "NoType" 
                : ShowFullTypeName 
                    ? type.FullName 
                    : type.Name;
        }
    }

}
