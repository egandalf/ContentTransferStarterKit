using Interfaces.Ektron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ektron
{
    public class EkUserAPI : IEkUserAPI
    {
        public T GetUser<T>(long UserId)
        {
            throw new NotImplementedException();
        }
    }
}
