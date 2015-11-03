using Interfaces.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Represents a user object from Ektron or EPiServer.
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// From Ektron, the original User ID reference.
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// The user's username property (in Ektron, this is often the same as the email address).
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Generates a new password upon initial request. Because Ektron's passwords are hashed, there is no use attempting to transfer them to the new system.
        /// </summary>
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    _password = Guid.NewGuid().ToString();
                }
                return _password;
            }
        }
        private string _password;
        
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's display name. In Ektron, this is displayed instead of the username in most situations (because the username is often the email address).
        /// </summary>
        public string DisplayName { get; set; }
    }
}
