using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI
{
    public class DummyInitializer
    {
        // DO NOT DELETE THIS CODE - NECESSARY FOR COPYING EKTRON ASSEMBLIES REQUIRED FOR UNITY TO THE FINAL PROJECT
        private void DummyFunctionToMakeSureReferencesGetCopied_DO_NOT_DELETE()
        {
            /*
             * A Microsoft "feature" attempts to intelligently determine which assembly references
             * are actually in use and only copies those in order to avoid junking up the final build
             * output. However, this does not account for files required by Unity because they're not 
             * directly used by this class library. This block provides that use in order to ensure 
             * they are copied over.
             * 
             * Note that this may not affect Visual Studio versions 2010 and prior.
             * 
             * See: https://connect.microsoft.com/VisualStudio/feedback/details/652785/visual-studio-does-not-copy-referenced-assemblies-through-the-reference-hierarchy
             * 
             * Workaround provided here: http://stackoverflow.com/questions/20280717/references-from-class-library-are-not-copied-to-running-project-bin-folder
             */

            // Ektron.Cms.Framework.UI.Services
            var ektron_cms_framework_ui_services = typeof(Ektron.Cms.Framework.Context.CmsContextService);

            // Ektron.Cms.Mobile
            var ektron_cms_mobile = typeof(Ektron.Cms.Mobile.CmsDeviceInfoRepository);

            // Ektron.Cms.Framework
            var ektron_cms_framework = typeof(Ektron.Cms.Framework.Services.Settings.UrlAliasing.AliasSettingsServiceClient);

            // Ektron.Cms.BusinessObjects.Caching.Settings.UrlAliasing.UrlAliasSettingsCache
            var ektron_cms_businessobjects_caching = typeof(Ektron.Cms.BusinessObjects.Caching.Settings.UrlAliasing.UrlAliasSettingsCache);
        }

        //public DummyInitializer()
        //{
        //    DummyFunctionToMakeSureReferencesGetCopied_DO_NOT_DELETE();
        //}
    }
}
