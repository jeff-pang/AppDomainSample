using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ingeniare.CmdParser;
using ColorMeCode.AppDomainHost.Core.Controllers;

namespace ColorMeCode.AppDomainHost.Core.Commands
{
    public class DomainCommand:Command
    {
        public override string GetCommandName()
        {
            return "domain";
        }

        protected override bool execute(CommandArguments arguments, StringBuilder output)
        {
            if (arguments.ContainsParameter("new"))
            {
                AppDomainController.Instance.CreateAppDomain(arguments["new"].Values[0]);
            }
            else if (arguments.ContainsParameter("unload"))
            {
                AppDomainController.Instance.UnloadDomain(arguments["unload"].Values[0]);
            }
            
            return true;
        }
    }
}