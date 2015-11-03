using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Configuration
{
    public class EktronCredentials : ConfigurationSection
    {
        private static EktronCredentials credentials = ConfigurationManager.GetSection("EktronCredentials") as EktronCredentials;

        public static EktronCredentials Credentials { get { return credentials; } }

        [ConfigurationProperty("Username", IsRequired = true)]
        public string Username
        {
            get
            {
                return (string)base["Username"];
            }
            set
            {
                base["Username"] = value;
            }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)base["Password"];
            }
            set
            {
                base["Password"] = value;
            }
        }

        [ConfigurationProperty("BaseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get
            {
                return (string)base["BaseUrl"];
            }
            set
            {
                base["BaseUrl"] = value;
            }
        }
    }
}
