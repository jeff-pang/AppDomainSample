using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ColorMeCode.AppDomainHost.Core.Utilities
{
    public class AutoCounter
    {
        Timer _timer;
        int _value;
        public int Value { get { return _value; } }
        public Action<int> Elapsed;
        public AutoCounter()
        {
            _timer = new Timer(5000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }
        public void Start()
        {
            _timer.Start();
            Console.WriteLine("Domain {0}'s counter started", AppDomain.CurrentDomain.FriendlyName);
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _value++;
            Console.WriteLine("Domain {0}'s counter is now at {1}",AppDomain.CurrentDomain.FriendlyName,_value);
            if (Elapsed != null)
            {
                Elapsed(_value);
            }
        }
    }
}
