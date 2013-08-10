using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ingeniare.CmdParser;
using ColorMeCode.AppDomainHost.Core.Controllers;

namespace ColorMeCode.AppDomainHost.Core.Commands
{
    public class VariablesCommand:Command
    {
        public override string GetCommandName()
        {
            return "var";
        }

        protected override bool execute(CommandArguments arguments, StringBuilder output)
        {
            if (arguments.ContainsParameter("refresh"))
            {
                AppDomainController.Instance.RefreshVar(arguments["refresh"].Values[0]);
            }
            else if (arguments.ContainsParameter("show"))
            {
                AppDomainController.Instance.ShowVar(arguments["show"].Values[0]);
            }
            else if (arguments.ContainsParameter("set") && arguments.ContainsParameter("value"))
            {
                AppDomainController.Instance.SetVar(arguments["set"].Values[0], arguments["value"].Values[0]);
            }
            else if(arguments.ContainsParameter("tryget"))
            {
                AppDomainController.Instance.TryGetNonSerializable(arguments["tryget"].Values[0]);
            }
            return true;
        }
    }
}