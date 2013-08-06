using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMeCode.AppDomainHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd="";
            do
            {
                cmd = Console.ReadLine();
                cmd = cmd.Trim().ToLower();
                
                if (cmd == "new")
                {
                    AppDomain.CreateDomain("");    
                }

            } while (cmd != "exit");
        }
    }
}