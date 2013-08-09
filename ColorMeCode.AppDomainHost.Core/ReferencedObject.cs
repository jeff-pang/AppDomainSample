using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorMeCode.AppDomainHost.Core
{
    public class ReferencedObject:MarshalByRefObject
    {
        public string Value { get; set; }
    }
}
