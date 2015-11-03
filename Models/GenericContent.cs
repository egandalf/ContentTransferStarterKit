using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Objects;
using Common;

namespace Models
{
    /// <summary>
    /// Represents a generic, unstructured content item from Ektron or EPiServer.
    /// </summary>
    [EktronDefinition(XmlConfigId = 0)]
    [EPiServerDefinition(ContentTypeName = "StandardPage")]
    public class GenericContent : IGenericContent
    {
        /// <summary>
        /// From Ektron, this represents the ID of the Smart Form.
        /// </summary>
        public long SourceStructureId { get; set; }

        /// <summary>
        /// From EPiServer, this represents the parent item in the content hierarchy.
        /// </summary>
        public string ParentContentReference { get; set; }

        /// <summary>
        /// The name or title of the content item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The primary, active URL of the content within Ektron.
        /// </summary>
        public string PrimaryUrl { get; set; }

        /// <summary>
        /// The raw HTML or XML of the content item. From EPiServer, this may be empty for more structured types.
        /// </summary>
        public string MainBody { get; set; }

        /// <summary>
        /// The raw HTML for the "teaser" or "summary" description of the content item.
        /// </summary>
        public string TeaserText { get; set; }

        /// <summary>
        /// The language ID for the content item.
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// The expiration date for the content item.
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// The date the content is scheduled to go live.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The Ektron source Folder Id.
        /// </summary>
        public long SourceFolderId { get; set; }

        /// <summary>
        /// The Ektron original content item Id.
        /// </summary>
        public long SourceId { get; set; }

        /// <summary>
        /// The metadata page title.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// The metadata page description.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// The metadata page keywords.
        /// </summary>
        public string[] MetaKeywords { get; set; }

        /// <summary>
        /// Gives indication for whether the item is currently in a published state. Note that this may be true even if the content is archived.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Used when pushing content into EPiServer DXC. Items which have been published in Ektron will be published in DXC.
        /// </summary>
        public string SaveAction
        {
            get
            {
                return IsPublished ? "Publish" : "Save";
            }
        }

        /// <summary>
        /// Indicates whether the item is an Ektron PageBuilder or PageBuilder Master layout.
        /// </summary>
        public bool IsPageLayout { get; set; }

        public GenericContent()
        {
            // Constructor
        }
    }
}
