using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace CookieDetector
{
    public interface IWebDriverCookieRetriever : ICookieRetriever
    {
        void SetDriver(IWebDriver d);
    }
}
