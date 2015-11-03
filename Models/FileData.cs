using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FileData
    {
        /// <summary>
        /// Must include the file extension.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File data converted to a Base64 string.
        /// </summary>
        public string Data { get; set; }

        public FileData()
        {

        }
    }
}
