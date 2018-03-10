using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    public abstract class Cookie
    {
        private string retrievedFrom;
        private string name;
        private string domain;
        private string type;
        private string path;
        private long dateCreated;
        private IDictionary<string, string> profileProperties; // for when we figure out how to modify the browser profile
        private string content;

        public Cookie()
        {
            profileProperties = new Dictionary<string, string>();
        }

        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Domain { get => domain; set => domain = value; }
        public string Path { get => path; set => path = value; }
        public long DateCreated { get => dateCreated; set => dateCreated = value; }
        public string RetrievedFrom { get => retrievedFrom; set => retrievedFrom = value; }
        public string Content { get => content; set => content = value; }

        public void SetProfileProperties(Dictionary<string, string> properties)
        {
            profileProperties = new Dictionary<string,string>(properties);
        }

        public void AddProfileProperty(string property, string value)
        {
            profileProperties.Add(property, value);
        }
       
        
    }
}
