using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CookieDetector
{
    public class LocalStorageCookieRetriever : IWebDriverCookieRetriever
    {
        private IWebDriver driver;

        CookieJar ICookieRetriever.GetCookies(string url)
        {
            if (driver == null)
            {
                throw new Exception();
            }

            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            CookieJar cookieJar = new CookieJar();
            long lsSize = 0;

            lsSize = (long)js.ExecuteScript("return window.localStorage.length;");
            
            // Loop through all localStorage data
            for (long j = 0; j < lsSize; j++)
            {
                LocalStorageCookie lsData = new LocalStorageCookie();
                lsData.Name = (String)js.ExecuteScript("return window.localStorage.key(" + j + ");");

                // It does not get third party cookies yet, so domain and retrieved from are the same for now
                //lsData.Domain = (new Uri(url).Host).Trim();
                lsData.Type = "localStorage";
                lsData.DateCreated = DateTime.Now.Ticks;
                lsData.Content = ((String)js.ExecuteScript("return window.localStorage.getItem('" + lsData.Name + "');"));
                cookieJar.Add(lsData);

            }
            return cookieJar;
        }
        
        void IWebDriverCookieRetriever.SetDriver(IWebDriver d)
        {
            driver = d;
        }
    }
}
