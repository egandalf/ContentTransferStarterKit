using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Organization
{
    public class MenuItem
    {
        public bool IsContent { get; set; }
        public string Name { get; set; }
        public long TargetId { get; set; }
        public string TargetReference { get; set; }
        public string Href { get; set; }
        public List<MenuItem> Children { get; set; }
        public MenuItem Parent { get; set; }
    }
}
