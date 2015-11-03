using Ektron.Cms.Content;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Content
{
    public class LibraryAPI
    {
        public List<ImageFile> GetAllLibraryImages()
        {
            var criteria = GetLibraryCriteria();

            var rawImageList = CommonAPI.LibraryInstance.GetList(criteria);
            if (criteria.PagingInfo.TotalPages > 1)
            {
                for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
                {
                    criteria.PagingInfo.CurrentPage = i;
                    rawImageList.AddRange(CommonAPI.LibraryInstance.GetList(criteria));
                }
            }

            if (rawImageList.Any())
                return (from i in rawImageList
                       select i.ToImageFile()).ToList();
            return new List<ImageFile>();
        }

        public List<ImageFile> GetLibraryImages(Folder FolderRef)
        {
            var criteria = GetLibraryCriteria();
            criteria.AddFilter(Ektron.Cms.Common.LibraryProperty.ParentId, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, FolderRef.Id);
            var rawImageList = CommonAPI.LibraryInstance.GetList(criteria);
            if (criteria.PagingInfo.TotalPages > 1)
            {
                for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
                {
                    criteria.PagingInfo.CurrentPage = i;
                    rawImageList.AddRange(CommonAPI.LibraryInstance.GetList(criteria));
                }
            }

            if (rawImageList.Any())
                return (from i in rawImageList
                        select i.ToImageFile()).ToList();
            return new List<ImageFile>();
        }

        private LibraryCriteria GetLibraryCriteria()
        {
            var criteria = new LibraryCriteria();
            criteria.AddFilter(Ektron.Cms.Common.LibraryProperty.TypeId, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, 1);
            //criteria.AddFilter(Ektron.Cms.Common.LibraryProperty.ContentType, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, 106); // omits image library
            return criteria;
        }
    }
}
