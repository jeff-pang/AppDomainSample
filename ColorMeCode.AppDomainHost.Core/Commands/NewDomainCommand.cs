using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ingeniare.CmdParser;
using ColorMeCode.AppDomainHost.Core.Controllers;

namespace ColorMeCode.AppDomainHost.Core.Commands
{
    public class NewDomainCommand:Command
    {
        public override string GetCommandName()
        {
            return "newdomain";
        }

        protected override bool execute(CommandArguments arguments, StringBuilder output)
        {
            if (arguments.Command == "newdomain" && arguments.Count == 1 && arguments[0].Values.Length==1)
            {
                AppDomainController.Instance.CreateAppDomain(arguments[0].Values[0]);
            }
            
            return true;
        }
    }
}