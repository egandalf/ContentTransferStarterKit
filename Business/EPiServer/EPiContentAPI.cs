using EPiServerAPI;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.EPiServer
{
    public class EPiContentAPI
    {
        public string CreateContent<T>(T item) where T : GenericContent
        {
            var api = new ContentAPI();
            return api.CreateContent(item);
        }

        public string GetContent(string ContentReference)
        {
            var api = new ContentAPI();
            return api.GetContent<GenericContent>(ContentReference);
        }

        public string GetContentTypeForExtension(string ext)
        {
            var api = new ContentAPI();
            return api.GetContentTypeForExtension(ext);
        }

        public string CreateContent(string type, string ParentRef, ImageFile asset)
        {
            var api = new ContentAPI();
            return api.CreateContent(type, ParentRef, asset);
        }

        public IEnumerable<IDictionary<string, object>> GetChildContent(string parentRef)
        {
            var api = new ContentAPI();
            return api.GetChildContent(parentRef);
        }
    }
}
