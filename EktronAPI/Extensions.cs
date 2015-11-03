using Ektron.Cms;
using Ektron.Cms.Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI
{
    public static class Extensions
    {
        public static T ToGenericContent<T>(this ContentData SourceContent) where T : GenericContent, new()
        {
            var gc = new T()
            {
                Name = SourceContent.Title,
                ExpireDate = SourceContent.ExpireDate,
                MainBody = SourceContent.Html,
                IsPublished = SourceContent.IsPublished,
                LanguageId = SourceContent.LanguageId,
                PrimaryUrl = SourceContent.Quicklink,
                MetaDescription = (from m in SourceContent.MetaData
                                       where m.Name.ToLower() == "description"
                                       select m.Text).DefaultIfEmpty(null).FirstOrDefault(),
                MetaKeywords = SplitKeywordString((from m in SourceContent.MetaData
                                    where m.Name.ToLower() == "keywords"
                                    select m.Text).DefaultIfEmpty(null).FirstOrDefault()),
                MetaTitle = (from m in SourceContent.MetaData
                                 where m.Name.ToLower() == "title"
                                 select m.Text).DefaultIfEmpty(SourceContent.Title).FirstOrDefault(),
                ParentContentReference = null, // Coming from Ektron, there is no parent reference.
                SourceFolderId = SourceContent.FolderId,
                SourceId = SourceContent.Id,
                SourceStructureId = SourceContent.XmlConfiguration.Id,
                StartDate = SourceContent.GoLiveDate.HasValue ? SourceContent.GoLiveDate.Value : DateTime.MinValue,
                TeaserText = SourceContent.Teaser,
                IsPageLayout = SourceContent.SubType == EkEnumeration.CMSContentSubtype.PageBuilderData || SourceContent.SubType == EkEnumeration.CMSContentSubtype.PageBuilderMasterData
            };

            long XmlConfigId = Common.PropertyRequest.GetEktronDefinition(typeof(T));
            if (XmlConfigId > 0)
            {
                var mapper = new XmlMapper.MapUtility();
                gc = mapper.RunMap<T>((T)gc, gc.MainBody);
            }

            return gc;
        }

        public static ImageFile ToImageFile(this LibraryData item)
        {
            var file = new ImageFile()
            {
                Teaser = item.Teaser,
                Name = item.Title,
                SourceContentId = item.ContentId,
                SourceFileUrl = GetImagePath(item.FileName),
                SourceLibraryId = item.Id
            };

            file.BinaryData = new FileData()
            {
                Name = item.FileName.Substring(item.FileName.LastIndexOf('/') + 1),
                Data = GetBinaryFileData(item.File) ?? GetBinaryDataFromFile(file.SourceFileUrl)
            };

            return file;
        }

        public static Folder ToGenericFolder(this FolderData item, int LanguageId)
        {
            var Folder = new Folder()
            {
                Name = item.Name,
                HasChildren = item.HasChildren,
                Id = item.Id,
                ParentId = item.ParentId,
                LanguageId = LanguageId
            };

            return Folder;
        }

        private static string GetImagePath(string p)
        {
            if (p.StartsWith("0/"))
            {
                return string.Format("{0}assets/{1}", 
                    EktronAPI.Configuration.EktronCredentials.Credentials.BaseUrl, 
                    p);
            }
            else
            {
                return string.Format("{0}{1}", 
                    EktronAPI.Configuration.EktronCredentials.Credentials.BaseUrl, 
                    p.TrimStart('/'));
            }
        }

        private static string GetBinaryDataFromFile(string p)
        {
            var EktronPath = EktronAPI.Configuration.EktronCredentials.Credentials.BaseUrl;
            string returnValue = null;
            using (var client = new WebClient())
            {
                byte[] fileData = client.DownloadData(p);
                if (fileData != null)
                    returnValue = Convert.ToBase64String(fileData);
                client.Dispose();
            }
            return returnValue;
        }

        private static string GetBinaryFileData(byte[] p)
        {
            return p != null ? Convert.ToBase64String(p) : null;
        }

        private static string[] SplitKeywordString(string keywords)
        {
            if (string.IsNullOrEmpty(keywords)) return new string[] { };

            string[] aryKeywords = keywords.Split(';');
            return aryKeywords;
        }
    }
}
