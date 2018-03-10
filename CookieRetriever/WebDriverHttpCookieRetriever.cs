using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;
using OpenQA.Selenium;

namespace CookieDetector
{
    public class WebDriverHttpCookieRetriever : IWebDriverCookieRetriever
    {
        private IWebDriver driver;

        CookieJar ICookieRetriever.GetCookies(string url)
        {
            CookieJar cookieJar = new CookieJar();
            
            var cookies = driver.Manage().Cookies.AllCookies;
         
           // loop to convert all selenium cookies into our HttpCookie object
            foreach (OpenQA.Selenium.Cookie c in cookies)
            {
                HttpCookie theCookie = HttpCookie.ToHttpCookie(c, url);
                cookieJar.Add(theCookie);
            }

            return cookieJar;
        }

        void IWebDriverCookieRetriever.SetDriver(IWebDriver d)
        {
            driver = d;
        }
    }
}
