using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EPiServerDefinition : Attribute
    {
        public string ContentTypeName { get; set; }
    }
}
