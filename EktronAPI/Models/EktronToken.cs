using EktronAPI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EktronAPI.Models
{
    public class EktronToken
    {
        private static string _token;

        public async Task<string> GetAuthTokenASync()
        {
            bool success = false;
            if (string.IsNullOrEmpty(_token))
            {
                success = await PopulateToken();
            }
            else
            {
                success = true;
            }
            if (success)
            {
                return _token;
            }
            return string.Empty;
        }

        private async Task<bool> PopulateToken()
        {
            var Creds = GetEktronCredentials();
            Task<bool> t = new Task<bool>(() =>
            {
                var token = CommonAPI.UserInstance_NoToken.Authenticate(Creds.Username, Creds.Password);
                if (!string.IsNullOrEmpty(token))
                {
                    _token = token;
                    return true;
                }
                return false;
            });
            t.Start();
            return await t;
        }

        private static EktronCredentials GetEktronCredentials()
        {
            if (!string.IsNullOrEmpty(EktronCredentials.Credentials.Username) && !string.IsNullOrEmpty(EktronCredentials.Credentials.Password))
            {
                return EktronCredentials.Credentials;
            }
            throw new SystemException("Missing Ektron credentials in configuration file.");
        }
    }
}
