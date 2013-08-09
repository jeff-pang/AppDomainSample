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
        AutoCounter _counter;
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

        public void StartAutoCounter()
        {
            if (_counter == null)
            {
                _counter = new AutoCounter();
                _counter.Elapsed = new Action<int>(Elapsed);
            }
            _counter.Start();
        }

        private void Elapsed(int i)
        {
            _referencedObject.Value = i.ToString();
            _serializableObject.Value = i.ToString();
            _nonSerializableObject.Value = i.ToString();
        }
        
        public void SetConsoleColor(int color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            ColoredOutput coloredOutput = new ColoredOutput(color);
            Console.SetOut(coloredOutput);
        }

        public ReferencedObject GetReferencedObject()
        {
            return _referencedObject;
        }

        public SerializableObject GetSerializableObject()
        {
            return _serializableObject;
        }

        public NonSerializableObject GetNonSerializableObject()
        {
            return _nonSerializableObject;
        }

        public void ShowReferencedObject()
        {
            Console.WriteLine("Domain {0} Ref. Value {1}",AppDomain.CurrentDomain.FriendlyName,_referencedObject.Value);
        }

        public void ShowSerializableObject()
        {
            Console.WriteLine("Domain {0} Ser. Value {1}", AppDomain.CurrentDomain.FriendlyName, _referencedObject.Value);
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