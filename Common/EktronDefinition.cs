using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class EktronDefinition : Attribute
    {
        public long XmlConfigId { get; set; }
    }
}
