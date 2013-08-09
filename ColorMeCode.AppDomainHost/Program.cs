using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ingeniare.CmdParser;
using ColorMeCode.AppDomainHost.Core.Commands;

namespace ColorMeCode.AppDomainHost
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandManager.LoadCommand(new DomainCommand());
            CommandManager.LoadCommand(new VariablesCommand());
            CommandManager.LoadCommand(new ReferencesCommand());
            
            Console.WriteLine("'exit' to exit");
            string cmd="";
            do
            {
                Console.Write(">");
                cmd=Console.ReadLine();
                cmd = cmd.Trim().ToLower();
                CommandManager.HandleCommandLine(cmd);

            } while (cmd != "exit");
        }
    }
}