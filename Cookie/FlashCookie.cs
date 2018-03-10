using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace CookieDetector
{
    public class FlashCookie : Cookie
    {
        public FlashCookie() : base() { }

        public FlashCookie(String url, String path)
        {
            FromPath(url, path);
        }

        public static FlashCookie FromPath(String path, String url)
        {
            Cookie metaData = new FlashCookie();
            IDictionary<string, string> parsedPath = PathParser.ParseSolFilePath(path);
            // add error checking
            metaData.RetrievedFrom = url;
            metaData.Type = "flash";
            metaData.Domain = parsedPath["subdomain"] + parsedPath["domain"];
            metaData.Path = parsedPath["url"];
            metaData.Name = parsedPath["name"];
            FileInfo file = new FileInfo(path);
            metaData.DateCreated = file.CreationTime.Ticks;
            return (FlashCookie)metaData;
        }

        public override string ToString()
        {
            return 
                "Name: " + Name + "\n"
                + "Type: Flash Cookie\n"
                + "Retrieved From" + RetrievedFrom + "\n"
                + "Domain: " + Domain + "\n"
                + "Path: " + Path + "\n"
                + "Date Created: " + DateCreated + "\n"
                +"SubKeys: " + "" + "\n";
        }

        public string ToCSVRow()
        {
            // name,type,retrievedfrom,domain,path,datecreated,subkeys
            return Name + ",flash," + RetrievedFrom + "," + Domain + "," + Path + "," + DateCreated + ",";
        }

    }
}
