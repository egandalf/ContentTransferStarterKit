using Ektron.Cms.Organization;
using Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Organization
{
    public class MenuAPI
    {
        private int languageId { get; set; }
        private Settings.UrlAliasing aliasApi = new Settings.UrlAliasing();

        public MenuItem GetMenuTree(long RootId, int LanguageId)
        {
            this.languageId = LanguageId;
            var menu = CommonAPI.MenuInstance.GetTree(RootId);

            return MapMenuTree(menu);
        }

        private MenuItem MapMenuTree(MenuData menu)
        {
            var mi = new MenuItem()
            {
                IsContent = this.IsMenuItemContent(menu),
                Href = menu.Href,
                TargetId = aliasApi.GetContentIdForAlias(menu.Href, languageId),
                Name = menu.Text,
                Children = this.GetMenuChildren(menu.Items)
            };
            return mi;
        }

        private List<MenuItem> GetMenuChildren(ICollection<IMenuItemData> collection)
        {
            var children = new List<MenuItem>();
            if (collection.Any())
            {
                foreach (var c in collection)
                {
                    children.Add(new MenuItem()
                    {
                        IsContent = this.IsMenuItemContent(c),
                        Href = c.Href,
                        TargetId = c.ItemId > 0 && c.Type == Ektron.Cms.Common.EkEnumeration.MenuItemType.Content ? c.ItemId : aliasApi.GetContentIdForAlias(c.Href, languageId),
                        Name = c.Text,
                        Children = this.GetMenuChildren(c.Items)
                    });
                }
            }
            return children;
        }

        private bool IsMenuItemContent(IMenuItemData c)
        {
            return c.Type == Ektron.Cms.Common.EkEnumeration.MenuItemType.Content
                || aliasApi.GetContentIdForAlias(c.Href, this.languageId) > 0;
        }

        private bool IsMenuItemContent(MenuData menu)
        {
            return (menu.Type == Ektron.Cms.Common.EkEnumeration.MenuItemType.Content
                || aliasApi.GetContentIdForAlias(menu.Href, this.languageId) > 0);
        }
    }
}
