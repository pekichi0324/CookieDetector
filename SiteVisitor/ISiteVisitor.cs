using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CookieDetector
{
    // There may be more/better implementations that don't use web driver in the future
    // RemoteSiteExplorer uses a remote web driver to keep an instance of web driver running
    // ThreadedSiteExplorer
    // ThreadedCrawlerExplorer 

    public interface ISiteVisitor
    {
        void Start();
        void Stop();
        SiteCookieGroup VisitSite(string url);
        SiteCookieGroupList VisitSites(string [] urls);
        void AddProfileProperty(string property, string value);
    }
}
