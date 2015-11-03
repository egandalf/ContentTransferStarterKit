using Interfaces.Ektron;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EktronAPI;

namespace Business.Ektron
{
    public class EkContentAPI
    {
        public T GetContent<T>(long ContentId, int LanguageId) where T : GenericContent, new()
        {
            var dalAPI = new EktronAPI.Content.ContentAPI();
            return dalAPI.GetSingleItem<T>(ContentId);
        }

        public List<T> GetContentList<T>(long SourceId, Common.Enumeration.ContentSourceType SourceType, int LanguageId) where T : GenericContent, new()
        {
            var dalAPI = new EktronAPI.Content.ContentAPI();
            return dalAPI.GetList<T>(SourceId, Common.Enumeration.ContentSourceType.Folder, 50, 1);
            //return dalAPI.GetAll<T>(SourceId, Common.Enumeration.ContentSourceType.Folder, 1033);
        }
    }
}
