using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    // Cookie Container to store all the cookieJars for each types of cookie with the url which the cookies are retrieved from
    public class SiteCookieGroup 
    {
        private string retrievedFromUrl;
        private CookieJar http;
        private CookieJar flash;
        private CookieJar localStorage;
  
        public SiteCookieGroup()
        {
            retrievedFromUrl = "";
            http = new CookieJar();
            flash = new CookieJar();
            localStorage = new CookieJar();
        }

        public SiteCookieGroup(string url)
        {
            retrievedFromUrl = url;
            CookieJar httpCookieJar = new CookieJar();
            CookieJar FlashCookieJar = new CookieJar();
            CookieJar LocalStorageCookieJar = new CookieJar();
        }
        public string RetrievedFromUrl { get => retrievedFromUrl; set => retrievedFromUrl = value; }
        public CookieJar Http  { get => http; set => http = value; }
        public CookieJar Flash{ get => flash; set => flash = value; }
        public CookieJar LocalStorage{ get => localStorage; set => localStorage = value; }

    }
}
