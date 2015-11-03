using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Objects
{
    public interface IPressContact
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
    }
}
