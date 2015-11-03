using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [EktronDefinition(XmlConfigId = 23)]
    public class CareerListing : GenericContent
    {
        [EktronXml("/FieldCollection/Heading")]
        public string Heading { get; set; }

        [EktronXml("/FieldCollection/Subheading")]
        public string Subheading { get; set; }

        [EktronXml("/FieldCollection/Overview")]
        [EktronRich]
        public string Overview { get; set; }

        [EktronXml("/FieldCollection/Responsibilities")]
        [EktronRich]
        public string Responsibilities { get; set; }

        [EktronXml("/FieldCollection/Requirements")]
        [EktronRich]
        public string Requirements { get; set; }

        [EktronXml("/FieldCollection/SubmissionEmail")]
        public string Email { get; set; }

        [EktronXml("/FieldCollection/JobTypeText")]
        public string JobType { get; set; }

        [EktronXml("/FieldCollection/JobIdText")]
        public string JobId { get; set; }

        [EktronXml("/FieldCollection/JobLocationText")]
        public string Location { get; set; }
    }
}
