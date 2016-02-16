using Business.Ektron;
using Business.EPiServer;
using Models;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCopyFolderContent
{
    class Program
    {
        private static EkContentAPI ekAPI = new EkContentAPI();
        private static EPiContentAPI epAPI = new EPiContentAPI();
        private static int LanguageId = Common.Configuration.LanguageId;

        static void Main(string[] args)
        {
            Console.WriteLine("Loading Ektron content... ");
            Console.WriteLine("(This may take a few minutes, depending on amount of data to return.)");
            var blogs = ekAPI.GetContentList<GenericContent>(125, Common.Enumeration.ContentSourceType.Folder, LanguageId);
            Console.WriteLine(blogs.Count() + " content items found.");
            Console.WriteLine("Loading content into EPiServer DXC.");

            string cref;
            foreach (var blog in blogs)
            {
                blog.ParentContentReference = "5915";
                cref = epAPI.CreateContent<GenericContent>(blog);
                Console.WriteLine("Content Transferred.");
                Console.WriteLine("Ektron ID: " + blog.SourceId + "\tEPiServer ID: " + cref);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}