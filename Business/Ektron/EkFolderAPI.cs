using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ektron
{
    public class EkFolderAPI
    {
        private EktronAPI.Organization.Folders FolderAPI = new EktronAPI.Organization.Folders();

        public List<Folder> GetChildFolders(Models.Folder FolderRef)
        {
            return FolderAPI.GetList(FolderRef.Id, FolderRef.LanguageId);
        }
    }
}
