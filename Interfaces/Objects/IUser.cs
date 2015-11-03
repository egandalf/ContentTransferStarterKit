using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Objects
{
    public interface IUser
    {
        long UserID { get; set; }
        string Username { get; set; }
        string Password { get; }
        string Email { get; set; }
        string DisplayName { get; set; }
    }
}
