using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ColorMeCode.AppDomainHost.Core.Proxies;

namespace ColorMeCode.AppDomainHost.Core.Controllers
{
    public class AppDomainController
    {
        static Dictionary<string, AppDomain> _domains;
        static Dictionary<string, ChildProxy> _childProxies;
        static Dictionary<string, ReferencedObject> _referencedObjects;
        static Dictionary<string, SerializableObject> _serialisedObjects;

        static AppDomainController _instance;
        Stack<int> _consoleColors = new Stack<int>();
        
        public static AppDomainController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppDomainController();
                }

                return _instance;
            }
        }

        private AppDomainController()
        {
            _domains = new Dictionary<string, AppDomain>();            
            _consoleColors=new Stack<int>(new int[]{14,13,12,11,10,9});
            _referencedObjects = new Dictionary<string, ReferencedObject>();
            _serialisedObjects = new Dictionary<string, SerializableObject>();
            _childProxies = new Dictionary<string, ChildProxy>();
        }

        public AppDomain CreateAppDomain(string domainName)
        {
            if (!_domains.ContainsKey(domainName))
            {
                string assemblyName = Assembly.GetExecutingAssembly().FullName;
                AppDomain domain = AppDomain.CreateDomain(domainName);
                _domains[domainName] = domain;
                Console.WriteLine("Domain {0} created", domainName);
                
                ChildProxy cp = (ChildProxy)domain.CreateInstanceAndUnwrap(assemblyName, typeof(ChildProxy).FullName);
                _childProxies[domainName] = cp;
                Console.WriteLine("Proxy Created in {0}",cp.WheraAmI());

                _referencedObjects[domainName] = _childProxies[domainName].GetReferencedObject();
                _serialisedObjects[domainName] = _childProxies[domainName].GetSerializableObject();
                if (_consoleColors.Count == 0)
                {
                    _consoleColors = new Stack<int>(new int[] {14, 13, 12, 11, 10, 9 });
                }

                int consoleCol=_consoleColors.Pop();
                cp.SetConsoleColor(consoleCol);

                return domain;
            }
            else
            {
                throw new Exception("App Domain '" + domainName + "' already exists");
            }
        }

        public void UnloadDomain(string domainName)
        {
            if (_domains.ContainsKey(domainName))
            {
                AppDomain.Unload(_domains[domainName]);
                _domains.Remove(domainName);
                Console.WriteLine("Domain {0} unloaded", domainName);
            }
            else
            {
                Console.WriteLine("Domain {0} not found!", domainName);
            }
        }

        public AppDomain GetDomain(string domainName)
        {
            if (_domains.ContainsKey(domainName))
            {
                return _domains[domainName];
            }
            else
            {
                return null;
            }
        }


        public void ShowVar(string domainName)
        {
            if (_childProxies.ContainsKey(domainName))
            {
                Console.WriteLine("Domain {0} SerializableObject Value {1}", AppDomain.CurrentDomain.FriendlyName, _serialisedObjects[domainName].Value);
                _childProxies[domainName].ShowSerializableObject();
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }
        internal void SetVar(string domainName, string value)
        {

            if (_childProxies.ContainsKey(domainName))
            {
                _childProxies[domainName].SetSerializableObject(value);
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }

        public void RefreshVar(string domainName)
        {
            if (_childProxies.ContainsKey(domainName))
            {
                _serialisedObjects[domainName] = _childProxies[domainName].GetSerializableObject();
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }
        
        public void ShowRef(string domainName)
        {
            if (_childProxies.ContainsKey(domainName))
            {
                Console.WriteLine("Domain {0} ReferencedObject Value {1}", AppDomain.CurrentDomain.FriendlyName,_referencedObjects[domainName].Value);
                _childProxies[domainName].ShowReferencedObject();
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }

        internal void SetRef(string domainName, string value)
        {
            if (_childProxies.ContainsKey(domainName))
            {
                _childProxies[domainName].SetReferencedObject(value);
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }

        public void TryGetNonSerializable(string domainName)
        {
            if (_childProxies.ContainsKey(domainName))
            {
                try
                {
                    NonSerializableObject s = _childProxies[domainName].GetNonSerializableObject();
                    Console.WriteLine("Not possible to get here");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("You get an exception from {0}",domainName);
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine("Child domain {0} does not exist", domainName);
            }
        }

    }
}