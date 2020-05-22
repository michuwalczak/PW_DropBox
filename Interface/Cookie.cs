using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Library
{
    [DataContractAttribute]
    public class Cookie
    {
        public Cookie(string clientName, string folderPath)
        {
            this.ClientName = clientName;
            this.FolderPath = folderPath;
        }
        
        [DataMemberAttribute]
        public string ClientName { get; set; }

        [DataMemberAttribute]
        public string FolderPath { get; set; }
    }
}
