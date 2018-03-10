using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CookieDetector
{
    public class CookieDetector
    {
        static void Main(string[] args)
        {
            CookieDetector c = new CookieDetector();
            c.SetOptions(args);
            c.Run();  
        }

        public void SetOptions(string[] args)
        {
            
        }

        public void Run()
        {
            //"https://www.microsoft.com"
            string[] urls = { "https://www.cpp.edu/~jinjinglee/test.html" };
            ISiteVisitor visitor = new WebDriverSiteVisitor();
            visitor.Start();
            SiteCookieGroupList s = visitor.VisitSites(urls);
            visitor.Stop();

            Debug.Print(s.ToJSON());

        }
    }
}
