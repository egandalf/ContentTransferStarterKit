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
    /// Represents a "press contact" object, which was a nested group box within an Ektron Smart Form.
    /// </summary>
    [EPiServerDefinition(ContentTypeName = "PressContact")]
    public class PressContact : IPressContact
    {
        /// <summary>
        /// The EPiServer reference for this item.
        /// </summary>
        public string ContentReference { get; set; }

        /// <summary>
        /// The name of the Press Contact.
        /// </summary>
        [EktronXml("./Name")]
        public string Name { get; set; }

        /// <summary>
        /// The email for the Press Contact.
        /// </summary>
        [EktronXml("./Email")]
        public string Email { get; set; }

        /// <summary>
        /// The phone number for the Press Contact.
        /// </summary>
        [EktronXml("./Phone")]
        public string Phone { get; set; }

        public PressContact()
        {
            // Constructor
        }

        public override string ToString()
        {
            return ContentReference;
        }
    }
}
