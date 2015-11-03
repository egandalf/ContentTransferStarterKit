using Common;
using EPiServerAPI.Configuration;
using EPiServerAPI.ServiceClient;
using Models;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServerAPI
{
    public class ContentAPI
    {
        private static Lazy<EPiServerRestClient> _epiClient = null;
        private static EPiServerRestClient EPiClientInstance
        {
            get
            {
                if (_epiClient == null || !_epiClient.IsValueCreated)
                {
                    if (!string.IsNullOrEmpty(EPiServerCredentials.Credentials.ServiceUrl)
                        && !string.IsNullOrEmpty(EPiServerCredentials.Credentials.Username)
                        && !string.IsNullOrEmpty(EPiServerCredentials.Credentials.Password))
                    {
                        _epiClient = new Lazy<EPiServerRestClient>(() => new EPiServerRestClient(
                            EPiServerCredentials.Credentials.ServiceUrl,
                            EPiServerCredentials.Credentials.Username,
                            EPiServerCredentials.Credentials.Password));
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("The EPiServer endpoint URL, Username, or Password have not been correctly configured with this application.");
                    }
                }
                return _epiClient.Value;
            }
        }

        public string CreateContent<T>(T item) where T : GenericContent
        {
            string createTypeName = PropertyRequest.GetEPiServerDefinition(typeof(T));
            string parentName = item.ParentContentReference ?? EPiServerCredentials.Credentials.ImportRoot.ToString();
            var cref = EPiClientInstance.CreateContent(createTypeName, parentName, item);
            return cref;
        }

        public string CreateContent(string type, string parentReference, object item)
        {
            string cref = EPiClientInstance.CreateContent(type, parentReference, item);
            return cref;
        }

        public string GetContent<T>(string cref) where T : GenericContent
        {
            var k = EPiClientInstance.LoadContent(cref);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(k);
            return json;
        }

        public string GetContentTypeForExtension(string ext)
        {
            string type = EPiClientInstance.ContentTypeFor(ext);
            return type;
        }

        public IEnumerable<IDictionary<string, object>> GetChildContent(string parentRef)
        {
            var childItems = EPiClientInstance.LoadChildren(parentRef);
            return childItems;
        }
    }
}
