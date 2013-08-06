using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMeCode.AppDomainHost.Core.Controllers
{
    public class AppDomainController
    {
        static Dictionary<string, AppDomain> _domains;
        
        static AppDomainController _instance;
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
        }

        public AppDomain CreateAppDomain(string name)
        {
            if (!_domains.ContainsKey(name))
            {
                AppDomain domain = AppDomain.CreateDomain(name);
                _domains[name] = domain;
                return domain;
            }
            else
            {
                throw new Exception("App Domain '"+name+"' already exists");
            }            
        }

        public AppDomain GetDomain(string name)
        {
            if(_domains.ContainsKey(name))
            {
                return _domains[name];
            }
            else
            {
                return null;
            }
        }
    }
}
