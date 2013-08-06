using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;

namespace ColorMeCode.AppDomainHost.Core
{
    public class ChildProxy:MarshalByRefObject,IDisposable
    {
        bool _disposed;
        public void WheraAmI()
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        SerializableObject _obj;
        public void PassByValue(SerializableObject obj)
        {
            _obj = obj;
        }

        public void ShowValue()
        {
            if (_obj != null)
            {
                Console.WriteLine(_obj.Value);
            }
            else
            {
                Console.WriteLine("Object is not set");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            RemotingServices.Disconnect(this);
            _disposed = true;
        }
        
        ~ChildProxy()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
    }
}