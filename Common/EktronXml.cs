using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronXml : Attribute
    {
        public string Xpath { get; set; }
        public EktronXml() { }
        public EktronXml(string xpath)
        {
            this.Xpath = xpath;
        }
    }
}
