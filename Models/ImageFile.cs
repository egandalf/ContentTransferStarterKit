using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [EPiServerDefinition(ContentTypeName = "ImageFile")]
    public class ImageFile
    {
        public string Name { get; set; }
        public string Teaser { get; set; }
        public FileData BinaryData { get; set; }
        public long SourceLibraryId { get; set; }
        public long SourceContentId { get; set; }
        public string SourceFileUrl { get; set; }

        public ImageFile()
        {

        }
    }
}
