using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.EPiServer
{
    public interface IEPiUserAPI
    {
        T CreateUser<T>(T UserInstance);
    }
}
