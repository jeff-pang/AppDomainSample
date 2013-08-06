using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninjineer.AppDomainHost.Core
{
    public class HostProxy:MarshalByRefObject,IDisposable
    {
        public void WheraAmI()
        {
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
    }
}