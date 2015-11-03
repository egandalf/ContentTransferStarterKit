using EktronAPI.Content;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ektron
{
    public class EkAssetAPI
    {
        private LibraryAPI libAPI = new LibraryAPI();

        public List<ImageFile> GetImageAssets()
        {
            var images = libAPI.GetAllLibraryImages();
            return images;
        }

        public List<ImageFile> GetImageAssets(Folder FolderRef)
        {
            var images = libAPI.GetLibraryImages(FolderRef);
            return images;
        }
    }
}
