using EktronAPI.Organization;
using Interfaces.Ektron;
using Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ektron
{
    public class EkMenuAPI : IEkMenuAPI
    {
        public MenuItem GetMenuTree(long RootId)
        {
            var menuApi = new MenuAPI();
            return menuApi.GetMenuTree(RootId, Common.Configuration.LanguageId);
        }

        public T GetMenuTree<T>(T RootNode)
        {
            //throw new NotImplementedException();
            return default(T);
        }

        public T GetMenuNode<T>(long NodeId, int LanguageId)
        {
            //throw new NotImplementedException();
            return default(T);
        }
    }
}
