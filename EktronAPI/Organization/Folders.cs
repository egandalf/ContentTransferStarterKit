using Ektron.Cms;
using Models;
using EktronAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Organization
{
    public class Folders
    {
        public Folder GetItem(long FolderID, int LanguageID)
        {
            Folder result = new Folder();

            int origLang = CommonAPI.FolderInstance.ContentLanguage;
            CommonAPI.FolderInstance.ContentLanguage = LanguageID;
            var sourceData = CommonAPI.FolderInstance.GetItem(FolderID, true);
            CommonAPI.FolderInstance.ContentLanguage = origLang;

            if (sourceData != null)
            {
                result = sourceData.ToGenericFolder(LanguageID);
            }

            return result;
        }

        public List<Folder> GetList(long ParentID, int LanguageID)
        {
            List<Folder> results = new List<Folder>();

            int origLang = CommonAPI.FolderInstance.ContentLanguage;
            CommonAPI.FolderInstance.ContentLanguage = LanguageID;

            var criteria = GetFolderCriteria(ParentID);
            var sourceData = CommonAPI.FolderInstance.GetList(criteria);
            if (criteria.PagingInfo.TotalPages > 1)
            {
                for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
                {
                    criteria.PagingInfo.CurrentPage = i;
                    sourceData.AddRange(CommonAPI.FolderInstance.GetList(criteria));
                }
            }
            CommonAPI.FolderInstance.ContentLanguage = origLang;

            results = (from s in sourceData
                       select s.ToGenericFolder(LanguageID)).ToList();

            return results;
        }

        public Folder GetTree(long ParentID, int LanguageID)
        {
            throw new NotImplementedException();
        }

        private FolderCriteria GetFolderCriteria(long ParentID)
        {
            var criteria = new FolderCriteria(Ektron.Cms.Common.FolderProperty.FolderName, Ektron.Cms.Common.EkEnumeration.OrderByDirection.Ascending);
            criteria.AddFilter(Ektron.Cms.Common.FolderProperty.ParentId, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, ParentID);
            criteria.ReturnChildProperties = true;
            criteria.PagingInfo = new PagingInfo(50);
            return criteria;
        }
    }
}
