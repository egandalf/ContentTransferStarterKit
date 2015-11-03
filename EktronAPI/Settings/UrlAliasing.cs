using Ektron.Cms.Settings.UrlAliasing.DataObjects;
using EktronAPI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Settings
{
    public class UrlAliasing
    {
        public List<string> GetAllAliasesForContent(long ContentID, int LanguageID)
        {
            var criteria = this.GetContentAliasCriteria(ContentID);

            int origLang = CommonAPI.AliasInstance.ContentLanguage;
            CommonAPI.AliasInstance.ContentLanguage = LanguageID;

            var sourceList = CommonAPI.AliasInstance.GetList(criteria);

            CommonAPI.AliasInstance.ContentLanguage = origLang;

            return (from source in sourceList
                    select source.Alias).ToList();
        }

        public long GetContentIdForAlias(string Alias, int LanguageId)
        {
            AliasData aliasData = null;
            long TargetId = 0;
            if (Alias.StartsWith("http"))
            {
                aliasData = CommonAPI.AliasInstance.GetTarget(new Uri(Alias), LanguageId);
            }
            else
            {
                aliasData = CommonAPI.AliasInstance.GetTarget(new Uri(string.Format("{0}{1}", EktronCredentials.Credentials.BaseUrl, Alias.TrimStart('/'))), LanguageId);
            }
            if (aliasData != null) TargetId = aliasData.TargetId;
            return TargetId;
        }

        private AliasCriteria GetContentAliasCriteria(long ContentID)
        {
            var criteria = new AliasCriteria(AliasProperty.Alias, Ektron.Cms.Common.EkEnumeration.OrderByDirection.Ascending);
            criteria.AddFilter(AliasProperty.TargetId, Ektron.Cms.Common.CriteriaFilterOperator.EqualTo, ContentID);
            criteria.PagingInfo = new Ektron.Cms.PagingInfo(9999);
            return criteria;
        }
    }
}
