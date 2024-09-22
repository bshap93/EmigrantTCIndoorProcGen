using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class ServiceLocator
    {
        static ServiceLocator _instance;

        readonly Dictionary<Type, object> _services = new();
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null) _instance = new ServiceLocator();

                return _instance;
            }
        }

        public void RegisterService<T>(T service)
        {
            var type = typeof(T);
            _services.TryAdd(type, service);
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type)) return (T)_services[type];
            throw new Exception("Service not registered: " + type);
        }
    }
}
