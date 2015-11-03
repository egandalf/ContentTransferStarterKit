using Common;
using Interfaces.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Represents a Press Release content item coming from either Ektron or EPiServer.
    /// </summary>
    [EPiServerDefinition(ContentTypeName = "PressRelease")]
    [EktronDefinition(XmlConfigId = 6)]
    public class PressRelease : GenericContent, IPressRelease
    {
        /// <summary>
        /// The headline for the press release.
        /// </summary>
        [EktronXml("/root/Headline")]
        public string Headline { get; set; }
        
        /// <summary>
        /// The subhead for the press release.
        /// </summary>
        [EktronXml("/root/Subhead")]
        public string SubHead { get; set; }

        /// <summary>
        /// The HTML body of the press release.
        /// </summary>
        [EktronXml("/root/Body")]
        [EktronRich]
        public string Body { get; set; }

        /// <summary>
        /// The boilderplate text for the press release.
        /// </summary>
        [EktronXml("/root/Boiler")]
        public string Boilerplate { get; set; }

        /// <summary>
        /// The array of contact information for this press release. 
        /// </summary>
        [EktronXml("/root/Contact")]
        public List<PressContact> PressContacts { get; set; }

        IPressContact[] IPressRelease.PressContacts
        {
            get
            {
                return this.PressContacts.ToArray();
            }
            set
            {
                this.PressContacts = (value as PressContact[]).ToList();
            }
        }

        public PressRelease()
        {
            // Constructor
        }
    }
}
