using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using ColorMeCode.AppDomainHost.Core.Utilities;

namespace ColorMeCode.AppDomainHost.Core.Proxies
{
    public class ChildProxy:MarshalByRefObject,IDisposable
    {
        bool _disposed; 
        ReferencedObject _referencedObject;
        SerializableObject _serializableObject;
        NonSerializableObject _nonSerializableObject;

        public ChildProxy()
        {
            _referencedObject = new ReferencedObject();
            _serializableObject = new SerializableObject();
            _nonSerializableObject = new NonSerializableObject();
        }

        public string WheraAmI()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }
        
        public void SetConsoleColor(int color)
        {
            ColoredOutput coloredOutput = new ColoredOutput(color);
            Console.SetOut(coloredOutput);
        }

        public ReferencedObject GetReferencedObject()
        {
            return _referencedObject;
        }

        public void SetReferencedObject(string value)
        {
            _referencedObject.Value = value;
            ShowReferencedObject();
        }

        public void ShowReferencedObject()
        {
            Console.WriteLine("Domain {0} ReferencedObject Value {1}", AppDomain.CurrentDomain.FriendlyName, _referencedObject.Value);
        }

        public SerializableObject GetSerializableObject()
        {
            return _serializableObject;
        }
        
        public void SetSerializableObject(string value)
        {
            _serializableObject.Value=value;
            ShowSerializableObject();
        }

        public void ShowSerializableObject()
        {
            Console.WriteLine("Domain {0} SerializableObject Value {1}", AppDomain.CurrentDomain.FriendlyName, _serializableObject.Value);
        }
        
        public NonSerializableObject GetNonSerializableObject()
        {
            return _nonSerializableObject;
        }


        public override object InitializeLifetimeService()
        {
            return null;
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