using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.EPiServer
{
    public class EPiFolderAPI
    {
        private EPiServerAPI.ContentAPI ContentAPI = new EPiServerAPI.ContentAPI();

        public string CreateFolder(string Name, string ParentRef)
        {
            return ContentAPI.CreateContent("SysContentFolder", ParentRef, new { Name = Name });
        }
    }
}
