using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.EPiServer
{
    public interface IEPiBlobAPI
    {
        T CreateBlob<T>(T BlobInstance);
    }
}
