using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    public class FlashCookieRetriever : ILocalCookieRetriever
    {
        private string flashDirectory;

        public FlashCookieRetriever()
        {
            flashDirectory = ConfigurationManager.AppSettings["flashDirectory"];
        }

        public FlashCookieRetriever(string flashDirectory)
        {
            this.flashDirectory = flashDirectory;
        }

        CookieJar ICookieRetriever.GetCookies(string url)
        {
            
            if (!Directory.Exists(flashDirectory))
            {
                return new CookieJar();
            }
            var pathList = Directory.EnumerateFiles(flashDirectory, "*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".sol"));

            CookieJar jar = new CookieJar();
            foreach (String p in pathList)
            {
                System.Diagnostics.Debug.WriteLine(FlashCookie.FromPath(p, url).ToCSVRow());
                jar.Add(FlashCookie.FromPath(p, url));
            }

            return jar;
        }

        void ILocalCookieRetriever.setPath(string path)
        {
            flashDirectory = path;
        }
    }
}
