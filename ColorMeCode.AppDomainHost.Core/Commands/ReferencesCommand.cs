using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ingeniare.CmdParser;
using ColorMeCode.AppDomainHost.Core.Controllers;

namespace ColorMeCode.AppDomainHost.Core.Commands
{
    public class ReferencesCommand:Command
    {
        public override string GetCommandName()
        {
            return "ref";
        }

        protected override bool execute(CommandArguments arguments, StringBuilder output)
        {
            if (arguments.ContainsParameter("show"))
            {
                AppDomainController.Instance.ShowRef(arguments["show"].Values[0]);
            }
            else if (arguments.ContainsParameter("set") && arguments.ContainsParameter("value"))
            {
                AppDomainController.Instance.SetRef(arguments["set"].Values[0],arguments["value"].Values[0]);
            }
            return true;
        }
    }
}