using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Objects
{
    public interface IGenericContent
    {
        long SourceStructureId { get; set; }
        string ParentContentReference { get; set; }
        string Name { get; set; }
        string PrimaryUrl { get; set; }
        string MainBody { get; set; }
        string TeaserText { get; set; }
        int LanguageId { get; set; }
        DateTime ExpireDate { get; set; }
        DateTime StartDate { get; set; }
        long SourceFolderId { get; set; }
        long SourceId { get; set; }
        string MetaTitle { get; set; }
        string MetaDescription { get; set; }
        string[] MetaKeywords { get; set; }
        bool IsPublished { get; set; }
    }
}
