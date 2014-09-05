using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using ModelSoft.Framework.Annotations;

namespace ModelSoft.Framework.Logging
{
    public static class LogManager
    {
        private static Func<ILogChannelProvider> _serviceCreator;
        private static ILogChannelProvider _service;
        private static object _lockObj = new object();

        public static ILogChannelProvider Default
        {
            get
            {
                if (_service == null && _serviceCreator != null)
                {
                    lock (_lockObj)
                    {
                        if (_service == null && _serviceCreator != null)
                        {
                            _service = _serviceCreator();
                            if (_service == null)
                                throw new InvalidOperationException("Service creator missing on LogManager");
                        }
                    }
                }
                return _service;
            }
        }

        public static void SetService([NotNull] Func<ILogChannelProvider> serviceCreator)
        {
            if (serviceCreator == null) throw new ArgumentNullException("serviceCreator");
            if (_serviceCreator != null) throw new InvalidOperationException("Service creator already set on LogManager");
            _serviceCreator = serviceCreator;
        }

        public static void SetService([NotNull] ILogChannelProvider service)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (_serviceCreator != null) throw new InvalidOperationException("Service creator already set on LogManager");
            _serviceCreator = () => service;
        }

        private const string LogContextKey = "ModelSoft.Framework.Logging.LogContextKey";
        private static LogContext LogContext
        {
            get
            {
                var context = CallContext.LogicalGetData(LogContextKey) as LogContext;
                if (context == null)
                {
                    context = new LogContext();
                    CallContext.LogicalSetData(LogContextKey, context);
                }
                return context;
            }
        }

        public static DisposableLogDataContainer OpenContainer(string source)
        {
            return new DisposableLogDataContainer(source);
        }

        public static LogData OnEnterContext(string source)
        {
            throw new NotImplementedException();
        }
        public static LogData GetCurrentContext()
        {
            throw new NotImplementedException();
        }
        public static LogData OnExitContext()
        {
            throw new NotImplementedException();
        }
    }

    public class LogContext
    {
        private Stack<LogData> _stack;

        public LogContext()
        {
            
        }

        public Stack<LogData> Stack
        {
            get { return _stack ?? (_stack = new Stack<LogData>()); }
        }
    }

    public class LogData
    {
        public LogData(string source, int level)
        {
            Source = source;
            Level = level;
        }

        public string Source { get; private set; }
        public int Level { get; private set; }
    }

    public class DisposableLogDataContainer : IDisposable
    {
        public DisposableLogDataContainer(string source)
        {
            this.LogData = LogManager.OnEnterContext(source);
        }

        public LogData LogData { get; private set; }

        public void Dispose()
        {
            if (LogData == null) return;
            LogManager.OnExitContext();
            LogData = null;
            GC.SuppressFinalize(this);
        }
    }
}