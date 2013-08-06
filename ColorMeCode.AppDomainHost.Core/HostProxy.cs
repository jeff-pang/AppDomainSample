using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMeCode.AppDomainHost.Core
{
    public class HostProxy:MarshalByRefObject,IDisposable
    {
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}