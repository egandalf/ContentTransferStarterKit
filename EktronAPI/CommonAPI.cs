using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Framework.Settings.UrlAliasing;
using Ektron.Cms.Framework.User;
using EktronAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI
{
    internal class CommonAPI
    {
        private static Lazy<EktronToken> _authToken = new Lazy<EktronToken>(() => new EktronToken());
        private static EktronToken AuthToken
        {
            get
            {
                return _authToken.Value;
            }
        }

        private static Lazy<ContentManager> _contentInstance = null;
        public static ContentManager ContentInstance
        {
            get
            {
                if (_contentInstance == null || !_contentInstance.IsValueCreated)
                {
                    _contentInstance = new Lazy<ContentManager>(() => new ContentManager());
                    _contentInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                    _contentInstance.Value.InPreviewMode = true;
                    _contentInstance.Value.RequestInformation.PreviewMode = true;
                }
                return _contentInstance.Value;
            }
        }

        private static Lazy<LibraryManager> _libraryInstance = null;
        public static LibraryManager LibraryInstance
        {
            get
            {
                if (_libraryInstance == null || !_libraryInstance.IsValueCreated)
                {
                    _libraryInstance = new Lazy<LibraryManager>(() => new LibraryManager());
                    _libraryInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                    _libraryInstance.Value.InPreviewMode = true;
                    _libraryInstance.Value.RequestInformation.PreviewMode = true;
                }
                return _libraryInstance.Value;
            }
        }

        private static Lazy<FolderManager> _folderInstance = null;
        public static FolderManager FolderInstance
        {
            get
            {
                if (_folderInstance == null || !_folderInstance.IsValueCreated)
                {
                    _folderInstance = new Lazy<FolderManager>(() => new FolderManager());
                    _folderInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _folderInstance.Value;
            }
        }

        private static Lazy<TaxonomyManager> _taxonomyInstance = null;
        public static TaxonomyManager TaxonomyInstance
        {
            get
            {
                if (_taxonomyInstance == null || !_taxonomyInstance.IsValueCreated)
                {
                    _taxonomyInstance = new Lazy<TaxonomyManager>(() => new TaxonomyManager());
                    _taxonomyInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _taxonomyInstance.Value;
            }
        }

        private static Lazy<UserManager> _userInstance;
        public static UserManager UserInstance
        {
            get
            {
                if (_userInstance == null || !_userInstance.IsValueCreated)
                {
                    _userInstance = new Lazy<UserManager>(() => new UserManager());
                    _userInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _userInstance.Value;
            }
        }

        private static Lazy<TaxonomyItemManager> _taxonomyItemInstance;
        public static TaxonomyItemManager TaxonomyItemInstance
        {
            get
            {
                if (_taxonomyItemInstance == null || !_taxonomyItemInstance.IsValueCreated)
                {
                    _taxonomyItemInstance = new Lazy<TaxonomyItemManager>(() => new TaxonomyItemManager());
                    _taxonomyItemInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _taxonomyItemInstance.Value;
            }
        }

        private static Lazy<MenuManager> _menuInstance;
        public static MenuManager MenuInstance
        {
            get
            {
                if (_menuInstance == null || !_menuInstance.IsValueCreated)
                {
                    _menuInstance = new Lazy<MenuManager>(() => new MenuManager());
                    _menuInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _menuInstance.Value;
            }
        }

        private static Lazy<AliasManager> _aliasInstance;
        public static AliasManager AliasInstance
        {
            get
            {
                if (_aliasInstance == null || !_aliasInstance.IsValueCreated)
                {
                    _aliasInstance = new Lazy<AliasManager>(() => new AliasManager());
                    _aliasInstance.Value.RequestInformation.AuthenticationToken = AuthToken.GetAuthTokenASync().Result;
                }
                return _aliasInstance.Value;
            }
        }

        // Used to get the authentication token.
        // This singleton doesn't need to be lazy because it has no dependency on the auth token.
        private static UserManager _userInstance_NoToken;
        public static UserManager UserInstance_NoToken
        {
            get
            {
                if (_userInstance_NoToken == null)
                {
                    _userInstance_NoToken = new UserManager();
                }
                return _userInstance_NoToken;
            }
        }
    }
}
