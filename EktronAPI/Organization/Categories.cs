using Ektron.Cms;
using Ektron.Cms.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Organization
{
    public class Categories
    {
        ///// <summary>
        ///// Retrieves the Ektron Taxonomy categories to which this content has been assigned, using the default language.
        ///// </summary>
        ///// <param name="ContentID">The ID for the Ektron Content item assigned to Taxonomy</param>
        ///// <returns>A list of paired down category objects.</returns>
        //public List<TaxonomyCategory> GetCategoriesForContent(long ContentID)
        //{
        //    return GetCategoriesForContent(ContentID, CommonAPI.ContentInstance.ContentLanguage);
        //}

        ///// <summary>
        ///// Retrieves the Ektron Taxonomy categories to which this content has been assigned.
        ///// </summary>
        ///// <param name="ContentID">The ID for the Ektron Content item assigned to Taxonomy</param>
        ///// <param name="LanguageID">The language of the assigned content item.</param>
        ///// <returns>A list of paired-down category objects.</returns>
        //public List<TaxonomyCategory> GetCategoriesForContent(long ContentID, int LanguageID)
        //{
        //    List<TaxonomyCategory> results = new List<TaxonomyCategory>();

        //    var assignedCategories = CommonAPI.ContentInstance.GetAssignedTaxonomyList(ContentID, LanguageID);
        //    if (assignedCategories.Any())
        //    {
        //        results = (from cat in assignedCategories
        //                   select new TaxonomyCategory(cat)).ToList();
        //    }

        //    return results;
        //}

        ///// <summary>
        ///// Retrieves a single Taxonomy node.
        ///// </summary>
        ///// <param name="ID">The ID for the Taxonomy node.</param>
        ///// <param name="LanguageID">The language of the node to be returned.</param>
        ///// <returns>A paired-down category object.</returns>
        //public TaxonomyCategory GetItem(long ID, int LanguageID)
        //{
        //    // Store and set language for request.
        //    int origLang = CommonAPI.TaxonomyInstance.ContentLanguage;
        //    CommonAPI.TaxonomyInstance.ContentLanguage = LanguageID;

        //    var category = CommonAPI.TaxonomyInstance.GetItem(ID);

        //    // Restore original language to API
        //    CommonAPI.TaxonomyInstance.ContentLanguage = origLang;

        //    return new TaxonomyCategory(category);
        //}

        ///// <summary>
        ///// Retrieves a list of categories which match the criteria object filters.
        ///// </summary>
        ///// <param name="criteria">The criteria for category retrieval</param>
        ///// <returns>A list of paired-down category objects.</returns>
        //public List<TaxonomyCategory> GetList(Ektron.Cms.Organization.TaxonomyCriteria criteria)
        //{
        //    List<TaxonomyCategory> results = new List<TaxonomyCategory>();

        //    var categories = CommonAPI.TaxonomyInstance.GetList(criteria);
        //    if (categories.Any())
        //    {
        //        results = (from cat in categories
        //                   select new TaxonomyCategory(cat)).ToList();
        //        if (criteria.PagingInfo.TotalPages > 1)
        //        {
        //            for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
        //            {
        //                criteria.PagingInfo.CurrentPage = i;
        //                categories = CommonAPI.TaxonomyInstance.GetList(criteria);
        //                results.AddRange((from cat in categories
        //                                  select new TaxonomyCategory(cat)).ToList());
        //            }
        //        }
        //    }

        //    return results;
        //}


        ///// <summary>
        ///// Retrieves an entire Taxonomy tree, starting at the node indicated by the supplied ID.
        ///// </summary>
        ///// <param name="RootID">The root of the tree to be returned.</param>
        ///// <param name="LanguageID">The language in which nodes should be retrieved.</param>
        ///// <returns>A tree of type TaxonomyCategory</returns>
        //public TaxonomyCategory GetTree(long RootID, int LanguageID)
        //{
        //    TaxonomyCategory root = new TaxonomyCategory();

        //    // Store original language ID and reset to specified value
        //    int origLang = CommonAPI.TaxonomyInstance.ContentLanguage;
        //    CommonAPI.TaxonomyInstance.ContentLanguage = LanguageID;

        //    var rootCat = CommonAPI.TaxonomyInstance.GetItem(RootID);
        //    root = GetTree(rootCat);

        //    // restore original language
        //    CommonAPI.TaxonomyInstance.ContentLanguage = origLang;

        //    return root;
        //}

        //private TaxonomyCategory GetTree(TaxonomyData sourceData)
        //{
        //    var root = new TaxonomyCategory(sourceData);

        //    var criteria = GetTaxonomyListCriteria(sourceData.Id);
        //    var childTaxs = this.GetRawList(criteria);
        //    if (childTaxs.Any())
        //    {
        //        root.Children = (from tax in childTaxs
        //                         select GetTree(tax)).ToList();
        //    }

        //    return root;
        //}

        //private TaxonomyCriteria GetTaxonomyListCriteria(long ID)
        //{
        //    var criteria = new TaxonomyCriteria(TaxonomyProperty.Name, Ektron.Cms.Common.EkEnumeration.OrderByDirection.Ascending);
        //    criteria.AddFilter(TaxonomyProperty.ParentId, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, ID);
        //    criteria.ReturnRecursiveChildren = false;
        //    criteria.PagingInfo = new PagingInfo(1000);
        //    return criteria;
        //}

        //private List<TaxonomyData> GetRawList(TaxonomyCriteria criteria)
        //{
        //    var results = new List<TaxonomyData>();

        //    results = CommonAPI.TaxonomyInstance.GetList(criteria);
        //    if (criteria.PagingInfo.TotalPages > 1)
        //    {
        //        for (int i = 2; i <= criteria.PagingInfo.TotalPages; i++)
        //        {
        //            criteria.PagingInfo.CurrentPage = i;
        //            results.AddRange(CommonAPI.TaxonomyInstance.GetList(criteria));
        //        }
        //    }

        //    return results;
        //}
    }
}
