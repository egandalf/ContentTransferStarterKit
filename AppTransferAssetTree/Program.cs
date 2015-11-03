using Business.Ektron;
using Business.EPiServer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTransferAssetTree
{
    class Program
    {
        private static int RootDestination = 3796;
        private static string SystemFolderType = "SysContentFolder";

        private static EPiContentAPI epAPI = new EPiContentAPI();
        private static EPiFolderAPI epFolderAPI = new EPiFolderAPI();
        private static EkContentAPI ekAPI = new EkContentAPI();
        private static EkAssetAPI ekAssetAPI = new EkAssetAPI();
        private static EkFolderAPI ekFolderAPI = new EkFolderAPI();

        static void Main(string[] args)
        {
            //var ekLibAPI = new Business.Ektron.EkAssetAPI();
            //var images = ekLibAPI.GetImageAssets();
            //if (images.Any())
            //{
            //    Console.WriteLine(images.Count());

            //    int libCount = (from im in images
            //                    where im.SourceFileUrl.ToLower().Contains("/uploadedimages/")
            //                    select im.SourceContentId).Count();
            //    int assetCount = (from im in images
            //                      where im.SourceFileUrl.ToLower().Contains("/assets/0/")
            //                      select im.SourceContentId).Count();
            //    Console.WriteLine("Number of assets: " + assetCount);
            //    Console.WriteLine("Number of lib images: " + libCount);
            //}
            //else {
            //    Console.WriteLine("No images found.");
            //}
            Console.WriteLine("Starting...");
            CreateFolderRecursive(new Dictionary<string, string>(), RootDestination.ToString(), new Folder() { Name = "Ektron Assets", HasChildren = true, ParentId = 0, Id = 75 });
            //CheckFolderExists(RootDestination.ToString());
            
            Console.ReadLine();
        }

        private static void CheckFolderExists(string cref)
        {
            var k = epAPI.GetChildContent(cref);

            Console.WriteLine(k.Count());
        }

        private static void CreateFolderRecursive(Dictionary<string, string> dic, string ParentRef, Folder FolderRef)
        {
            if (FolderRef.HasChildren)
            {
                var folders = ekFolderAPI.GetChildFolders(FolderRef);
                if (folders.Any())
                {
                    string newRef;
                    foreach (var folder in folders)
                    {
                        Console.WriteLine("Creating folder '{0}' as a child of '{1}'", folder.Name, FolderRef.Name);
                        newRef = epFolderAPI.CreateFolder(folder.Name, ParentRef);
                        CreateFolderRecursive(dic, newRef, folder);
                    }
                }
            }

            List<ImageFile> assets = ekAssetAPI.GetImageAssets(FolderRef);
            if (assets.Any())
            {
                string type;
                string ext;
                foreach (var asset in assets)
                {
                    ext = asset.SourceFileUrl.Substring(asset.SourceFileUrl.LastIndexOf('.'));
                    if (string.IsNullOrEmpty(ext))
                    {
                        Console.WriteLine("Content Type not found for files of type '{0}'. File with Ektron ID {1} not uploaded.", ext, asset.SourceContentId);
                    }
                    if (!dic.ContainsKey(ext))
                    {
                        type = epAPI.GetContentTypeForExtension(ext);
                        dic.Add(ext, type);
                    }
                    Console.WriteLine("Uploading file with ID = {0} and Name = '{1}'.", asset.SourceContentId, asset.Name);
                    string result = epAPI.CreateContent(dic[ext], ParentRef, asset);
                    if (string.IsNullOrEmpty(result))
                    {
                        Console.WriteLine("ERROR! File not uploaded!");
                        if (asset.BinaryData.Data.Length > 1000000)
                        {
                            Console.WriteLine("File is too large for the API to consume.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Success!");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
