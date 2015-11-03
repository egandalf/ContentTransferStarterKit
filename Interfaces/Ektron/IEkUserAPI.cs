using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Ektron
{
    public interface IEkUserAPI
    {
        T GetUser<T>(long UserId);
    }
}
