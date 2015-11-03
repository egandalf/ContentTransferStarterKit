using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Objects
{
    public interface IMenuItem
    {
        long ParentId { get; set; }
        IMenuItem[] ChildItems { get; set; }
        string Href { get; set; }
        bool IsContent { get; set; }
    }
}
