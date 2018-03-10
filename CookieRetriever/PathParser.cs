using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CookieDetector
{

    public class PathParser
    {
        // Expected string: path\#SharedObjects\3DFKLJSD\domain\path\name.sol
        private static string pathPattern = 
                        @"\\#SharedObjects\\.+?\\" + // #SharedObjects folder and random string
                        @"(.*\.)?" +                // subdomain
                        @"(.+?\..*?)+" +            // domain
                        @"(\\.*\\)" +               // path
                        @"(.*\.sol)";               // name

        // Expected string: prot://sub.domain.ext/path
        private static string urlPattern = 
                        @":\/\/" +          // protocol (://)
                        @"(.*?\.)?" +       // subdomain
                        @"(.+?\..*?)" +     // domain
                        @"(\/|\z)";         // path

        private PathParser() { }

        public static IDictionary<string,string> ParseUrl(String url)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();
            Regex r = new Regex(pathPattern);
            Match m = r.Match(url);
            if (m.Success)
            {
                IDictionary<string, string> parsedPath = PathParser.ParseSolFilePath(url);
                parsedPath["subdomain"] = m.Groups[1].Value;
                parsedPath["domain"] = m.Groups[2].Value;
                parsedPath["path"] = m.Groups[3].Value;
            }
            else
            {
                throw new Exception();
            }

            return results;
        }

        public static IDictionary<string, string> ParseSolFilePath(String path)
        {
            IDictionary<string, string> results = new Dictionary<string,string>();
            Regex r = new Regex(pathPattern);
            Match m = r.Match(path);
            if (m.Success)
            {
                results["subdomain"] = m.Groups[1].Value;
                results["domain"] = m.Groups[2].Value;
                results["url"] = m.Groups[3].Value;
                results["name"] = m.Groups[4].Value;
            }
            else
            {
                throw new Exception();
            }

            return results;
        }
    }
}
