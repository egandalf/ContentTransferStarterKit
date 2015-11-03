using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServerAPI.Configuration
{
    public class EPiServerCredentials : ConfigurationSection
    {
        private static EPiServerCredentials credentials = ConfigurationManager.GetSection("EPiServerCredentials") as EPiServerCredentials;

        public static EPiServerCredentials Credentials { get { return credentials; } }

        [ConfigurationProperty("ServiceUrl", IsRequired = true)]
        public string ServiceUrl
        {
            get
            {
                return (string)base["ServiceUrl"];
            }
            set
            {
                base["ServiceUrl"] = value;
            }
        }

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

        [ConfigurationProperty("ImportRoot", IsRequired = true)]
        public int ImportRoot
        {
            get
            {
                return (int)base["ImportRoot"];
            }
            set
            {
                base["ImportRoot"] = value;
            }
        }
    }
}
