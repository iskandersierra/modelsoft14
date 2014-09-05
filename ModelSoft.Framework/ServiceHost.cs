using System;

namespace ModelSoft.Framework
{
    public static class ServiceHost<TService>
        where TService: class
    {
// ReSharper disable StaticFieldInGenericType
        private static readonly object ServiceLock = new object();
        private static TService _service = null;
        private static Func<TService> _serviceFactory = null;
// ReSharper restore StaticFieldInGenericType

        public static TService Service
        {
            get
            {
                if (_service == null)
                {
                    lock (ServiceLock)
                    {
                        if (_service == null)
                        {
                            if (_serviceFactory == null)
                                throw new InvalidOperationException(string.Format("Service factory is not configured for service {0}", typeof(TService).FullName));

                            var serviceInstance = _serviceFactory();
                            if (serviceInstance == null)
                                throw new InvalidOperationException(string.Format("Service factory is missconfigured for service {0}", typeof(TService).FullName));

                            _service = serviceInstance;
                        }
                    }
                }
                return _service;
            }
        }

        public static bool IsConfigured { get { return _serviceFactory != null; } }

        public static bool IsCreated { get { return _service != null; } }

        public static void SetServiceFactory(Func<TService> serviceFactory)
        {
            if (serviceFactory == null)
                throw new ArgumentNullException("serviceFactory");

            lock (ServiceLock)
            {
                if (_serviceFactory != null)
                    throw new InvalidOperationException(string.Format("Must configure service factory only once for service {0}", typeof(TService).FullName));

                _serviceFactory = serviceFactory;
            }
        }

        public static void SetService(TService serviceInstance)
        {
            if (serviceInstance == null)
                throw new ArgumentNullException("serviceInstance");

            SetServiceFactory(() => serviceInstance);
        }
    }
}
