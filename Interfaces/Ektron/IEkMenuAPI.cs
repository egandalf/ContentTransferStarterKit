using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Ektron
{
    public interface IEkMenuAPI
    {
        T GetMenuTree<T>(T RootNode);
        T GetMenuNode<T>(long NodeId, int LanguageId);
    }
}
