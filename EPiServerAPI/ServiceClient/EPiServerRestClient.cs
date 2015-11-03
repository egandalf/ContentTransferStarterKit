using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPiServerAPI.ServiceClient
{
    public class EPiServerRestClient
    {
        public RestClient Client { get; set; }

        //User management
        public void CreateUser(string Username, string JsonData)
        {
            IRestRequest rq = new RestRequest("/user/{username}", Method.PUT)
                .AddParameter("username", Username, ParameterType.UrlSegment)
                .AddParameter("application/json", JsonData, ParameterType.RequestBody);
            Client.Put(rq);
        }

        public string ContentTypeFor(string Extension)
        {
            IRestRequest rq = new RestRequest("/contenttype/typefor/{ext}", Method.GET)
                .AddParameter("ext", Extension, ParameterType.UrlSegment);
            var rt = Client.Get<ContentTypeHolder>(rq);
            return rt.Data.Name;
        }

        //Content management
        public string CreateContent(string ContentType, string Parent, object ContentItem)
        {
            IRestRequest rq = new RestRequest("/content/{parent}/Create/{contenttype}", Method.POST)
                .AddUrlSegment("parent", Parent)
                .AddUrlSegment("contenttype", ContentType)
                .AddJsonBody(ContentItem);

            var response = Client.Post<ReferenceContainer>(rq);
            if (response.StatusCode == HttpStatusCode.Created) return response.Data.Reference;
            return null;
        }

        public string EnsurePathExist(string Path)
        {
            IRestRequest rq = new RestRequest("/content/ensurepathexist/SysContentFolder/{path}")
                .AddUrlSegment("path", Path);
            var response = Client.Post<ReferenceContainer>(rq);
            if (response.StatusCode == HttpStatusCode.OK) return response.Data.Reference;
            return null;
        }

        public void DeleteContent(string Reference)
        {
            IRestRequest rq = new RestRequest("/content/{reference}", Method.DELETE)
                .AddUrlSegment("reference", Reference);
            Client.Delete(rq);

        }



        //Load Content

        /*
        public ExpandoObject[] LoadContentChildren(string Reference)
        {
            IRestRequest rq = new RestRequest("content/{reference}/children", Method.GET)
                .AddParameter("reference", Reference, ParameterType.UrlSegment);
            var rt=Client.Get<JToken>(rq);
            //dynamic obj=JsonConvert.DeserializeObject<ExpandoObject>(rt.Content, new ExpandoObjectConverter());
            //TODO: Error handling
            return obj.Children as ExpandoObject[];
        }*/

        public IEnumerable<IDictionary<string, object>> LoadChildren(string Reference)
        {
            IRestRequest rq = new RestRequest("/content/{reference}/children", Method.GET)
                .AddParameter("reference", Reference, ParameterType.UrlSegment);
            var rt = Client.Get<Dictionary<string, object>>(rq);
            //TODO: Handle pagination
            //TODO: Error handling

            return ((JsonArray)rt.Data["Children"]).Cast<IDictionary<string, object>>();
        }

        public ExpandoObject LoadContent(string Reference)
        {
            IRestRequest rq = new RestRequest("/content/{reference}", Method.GET)
                .AddParameter("reference", Reference, ParameterType.UrlSegment);
            var rt = Client.Get<ExpandoObject>(rq);

            //TODO: Error handling
            return rt.Data;
        }

        protected virtual void Authenticate(string Username, string Password)
        {
            IRestRequest rq = new RestRequest("token", Method.POST)
                .AddParameter("grant_type", "password", ParameterType.GetOrPost)
                .AddParameter("username", Username, ParameterType.GetOrPost)
                .AddParameter("password", Password, ParameterType.GetOrPost);
            var resp = Client.Post<Dictionary<string, string>>(rq);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                string token = (string)resp.Data["access_token"];
                Client.Authenticator = new RestSharp.Authenticators.OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            }
            else
            {
                throw new Exception("Unable to authenticate");
            }
        }

        public EPiServerRestClient(string Url, string Username, string Password)
        {
            Client = new RestClient(new Uri(new Uri(Url), "/episerverapi/"));
            //Client.AddHandler("application/json", new DynamicJsonDeserializer());
            //Authenticate
            Authenticate(Username, Password);

        }
    }

    public class ContentTypeHolder
    {
        public string Name { get; set; }
    }

    public class ReferenceContainer
    {
        public string Reference { get; set; }
    }
}