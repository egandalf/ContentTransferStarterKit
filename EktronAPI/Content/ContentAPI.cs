using Common;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Content
{
    public class ContentAPI
    {
        public T GetSingleItem<T>(long ItemID) where T : GenericContent, new()
        {
            return GetSingleItem<T>(ItemID, CommonAPI.ContentInstance.ContentLanguage);
        }

        public T GetSingleItem<T>(long ItemID, int LanguageID) where T : GenericContent, new()
        {
            var singleItemCriteria = this.GetCriteriaForSingleItem(ItemID);
            
            // Bypassing restrictions for single item request so items may be retrieved indiscriminantly from a menu structure.
            //ApplyContentRestrictions<T>(ref singleItemCriteria);
            
            // store and reset language
            int origLang = CommonAPI.ContentInstance.ContentLanguage;
            CommonAPI.ContentInstance.ContentLanguage = LanguageID;

            var ContentResults = CommonAPI.ContentInstance.GetList(singleItemCriteria);

            // restore original language
            CommonAPI.ContentInstance.ContentLanguage = origLang;

            if (ContentResults.Any())
            {
                return ContentResults.First().ToGenericContent<T>();
            }
            return null;
        }

        public List<T> GetList<T>(long SourceID, Enumeration.ContentSourceType SourceType, int MaxResults = 100, int PageNumber = 1) where T : GenericContent, new()
        {
            return this.GetList<T>(SourceID, SourceType, CommonAPI.ContentInstance.ContentLanguage, MaxResults, PageNumber);
        }

        public List<T> GetList<T>(long SourceID, Enumeration.ContentSourceType SourceType, int LanguageID, int MaxResults = 100, int PageNumber = 1) where T : GenericContent, new()
        {
            ContentCriteria criteria;
            switch (SourceType)
            {
                case Enumeration.ContentSourceType.Folder:
                    criteria = GetFolderCriteria(SourceID);
                    break;
                case Enumeration.ContentSourceType.Collection:
                    criteria = GetCollectionCriteria(SourceID);
                    break;
                case Enumeration.ContentSourceType.Taxonomy:
                    criteria = GetTaxonomyCriteria(SourceID);
                    break;
                default:
                    criteria = GetFolderCriteria(SourceID);
                    break;
            }

            ApplyContentRestrictions<T>(ref criteria);

            criteria.FolderRecursive = true;
            criteria.PagingInfo = new Ektron.Cms.PagingInfo(MaxResults, PageNumber);
            criteria.ReturnMetadata = true;

            var sourceList = CommonAPI.ContentInstance.GetList(criteria);
            var results = (from source in sourceList
                           select source.ToGenericContent<T>()).ToList();

            return results;
        }

        public List<T> GetAll<T>(long SourceID, Enumeration.ContentSourceType SourceType) where T : GenericContent, new()
        {
            return this.GetAll<T>(SourceID, SourceType, CommonAPI.ContentInstance.ContentLanguage);
        }

        public List<T> GetAll<T>(long SourceID, Enumeration.ContentSourceType SourceType, int LanguageID) where T : GenericContent, new()
        {
            var results = new List<T>();

            ContentCriteria criteria;
            switch (SourceType)
            {
                case Enumeration.ContentSourceType.Folder:
                    criteria = GetFolderCriteria(SourceID);
                    break;
                case Enumeration.ContentSourceType.Collection:
                    criteria = GetCollectionCriteria(SourceID);
                    break;
                case Enumeration.ContentSourceType.Taxonomy:
                    criteria = GetTaxonomyCriteria(SourceID);
                    break;
                default:
                    criteria = GetFolderCriteria(SourceID);
                    break;
            }
            criteria.FolderRecursive = true;
            criteria.PagingInfo = new Ektron.Cms.PagingInfo(50, 1);
            criteria.ReturnMetadata = true;

            var sourceList = CommonAPI.ContentInstance.GetList(criteria);
            results = (from source in sourceList
                       select source.ToGenericContent<T>()).ToList();
            if (criteria.PagingInfo.TotalPages > 1)
            {
                for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
                {
                    criteria.PagingInfo.CurrentPage = i;
                    results.AddRange(from source in CommonAPI.ContentInstance.GetList(criteria)
                                         select source.ToGenericContent<T>());
                }
            }

            return results;
        }

        private ContentCriteria GetFolderCriteria(long FolderID)
        {
            var criteria = new ContentCriteria();
            criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, FolderID);
            return criteria;
        }

        private ContentTaxonomyCriteria GetTaxonomyCriteria(long TaxonomyID)
        {
            var criteria = new ContentTaxonomyCriteria();
            criteria.AddFilter(TaxonomyID);
            return criteria;
        }

        private ContentCollectionCriteria GetCollectionCriteria(long CollectionID)
        {
            var criteria = new ContentCollectionCriteria();
            criteria.AddFilter(CollectionID);
            return criteria;
        }

        private ContentCriteria GetCriteriaForSingleItem(long ItemID)
        {
            var criteria = new ContentCriteria();
            criteria.PagingInfo = new Ektron.Cms.PagingInfo(1, 1);
            criteria.AddFilter(ContentProperty.Id, CriteriaFilterOperator.EqualTo, ItemID);

            // This rule catches both archived and non-archived content.
            // Change to 0 for only non-archived content.
            // Change to 1 for only archived content.
            criteria.AddFilter(ContentProperty.IsArchived, CriteriaFilterOperator.GreaterThan, -1);
            
            // Because this API is only interested in HTML content
            //criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, 0);
            
            criteria.ReturnMetadata = true;

            return criteria;
        }

        private void ApplyContentRestrictions<T>(ref ContentCriteria criteria)
        {
            // Restricts results to only Smart Forms that are mapped to this type definition.
            var XmlConfigId = Common.PropertyRequest.GetEktronDefinition(typeof(T));
            criteria.AddFilter(ContentProperty.XmlConfigurationId, CriteriaFilterOperator.EqualTo, XmlConfigId);
        }
    }
}
