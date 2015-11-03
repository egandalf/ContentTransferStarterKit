using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Objects
{
    public interface IPressRelease : IGenericContent
    {
        string Headline { get; set; }
        string SubHead { get; set; }
        string Body { get; set; }
        string Boilerplate { get; set; }
        IPressContact[] PressContacts { get; set; }
    }
}
