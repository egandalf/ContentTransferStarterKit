using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [EktronDefinition(XmlConfigId=17)]
    [EPiServerDefinition(ContentTypeName = "EktronProductPage")]
    public class Product : GenericContent
    {
        [EktronXml("/FieldCollection/BodyContent")]
        [EktronRich]
        public string BodyContent { get; set; }

        [EktronXml("/FieldCollection/Section")]
        [EktronItemReference]
        public List<long> Sections { get; set; }
    }
}
