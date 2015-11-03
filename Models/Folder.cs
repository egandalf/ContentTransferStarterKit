using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Folder
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public long ParentId { get; set; }
        public bool HasChildren { get; set; }
        public int LanguageId { get; set; }

        public Folder()
        {
            // Constructor
        }
    }
}
