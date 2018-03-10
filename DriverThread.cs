using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CookieDetector
{
    class DriverThread
    {
        private ISiteExplorer siteExplorer;
        private string url;

        public DriverThread(string url) 
        {
            this.url = url;
            this.siteExplorer = new WebDriverSiteExplorer();
        }

        public void Run()
        {
            
            siteExplorer.Start();
            siteExplorer.VisitSite(url);
            CookieJar cookies = siteExplorer.GetCookies();
            siteExplorer.Stop();
            Debug.WriteLine(cookies.ToString());
            cookies.CreateCSV("output.csv");

            //siteExplorer.GetCookies();
        }
    }
}
