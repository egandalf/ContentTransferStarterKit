using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.EPiServer
{
    public interface IEPiContentAPI
    {
        T CreateContent<T>(T ContentInstance);
        T UpdateContent<T>(T ContentInstance);
        T ReadContent<T>(object ContentReference);
        void DeleteContent<T>(T ContentInstance);
    }
}
