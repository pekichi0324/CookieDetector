using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;

namespace CookieDetector
{
    public class WebDriverSiteVisitor : ISiteVisitor
    {
        private string profileDirectory;
        private string driverDirectory;
        SiteCookieGroupList cookieGroupList = new SiteCookieGroupList();
        private IWebDriver driver;
        private ChromeOptions options;


        public WebDriverSiteVisitor()
        {
            profileDirectory = ConfigurationManager.AppSettings["profileDirectory"];
            driverDirectory = ConfigurationManager.AppSettings["driverDirectory"];
            SetupEnvironment();
            options = CreateOptions();
        }

        void ISiteVisitor.Start()
        {
            driver = new ChromeDriver(driverDirectory, options);
        }

        void ISiteVisitor.Stop()
        {
            driver.Quit();
        }
        

        public SiteCookieGroup VisitSite(string url)
        {
            driver.Url = url;
            SiteCookieGroup group = new SiteCookieGroup(url);
            //group.Http = GetCookiesUsingWebDriver(new WebDriverHttpCookieRetriever(), url);
            //group.Flash = GetCookiesLocally(new FlashCookieRetriever(), url);
            group.LocalStorage = GetCookiesUsingWebDriver(new LocalStorageCookieRetriever(), url);
            group.Http = GetCookiesLocally(new SQLiteHttpCookieRetriever(), url);
            return group;
        }

        SiteCookieGroupList ISiteVisitor.VisitSites(string[] urls)
        {
            SiteCookieGroupList results = new SiteCookieGroupList();
            foreach(string url in urls)
            {
                results.Add(VisitSite(url));
            }
            return results;
        }

        private CookieJar GetCookiesUsingWebDriver(IWebDriverCookieRetriever retriever, String url)
        {
            retriever.SetDriver(driver);
            return retriever.GetCookies(url);
        }

        private CookieJar GetCookiesLocally(ILocalCookieRetriever retriever, String url)
        {
            return retriever.GetCookies(url);
        }

        private ChromeOptions CreateOptions()
        {
            options = new ChromeOptions();
            // options.AddArgument("headless"); DOES NOT LOAD FLASH
            options.AddArgument("user-data-dir=" + profileDirectory);
            options.AddArgument("--mute-audio");
            options.AddUserProfilePreference("profile.default_content_setting_values.plugins", 1);
            options.AddUserProfilePreference("profile.content_settings.plugin_whitelist.adobe - flash - player", 1);
            options.AddUserProfilePreference("profile.content_settings.plugin_whitelist.adobe-flash-player", 1);
            options.AddUserProfilePreference("profile.content_settings.exceptions.plugins.*,*.per_resource.adobe-flash-player", 1);
            // options.AddUserProfilePreference("PluginsAllowedForUrls", "https://arlo.netgear.com"); - Not necessary

            return options;
        }

  
        void ISiteVisitor.AddProfileProperty(string property, string value)
        {
            throw new NotImplementedException();
        }

        private void SetupEnvironment()
        {
            if (!File.Exists(driverDirectory + @"\chromedriver.exe"))
            {
                DownloadDriver();
            }
            if (!Directory.Exists(profileDirectory))
            {
                Directory.CreateDirectory(profileDirectory);
            }
        }

        private void DownloadDriver()
        {
            String tempName = "file.zip";
            Console.WriteLine("Downloading ChromeDriver...");
            WebClient webClient = new WebClient();
            String downloadURL = ConfigurationManager.AppSettings["driverDownloadUrl"];

            try { 
                webClient.DownloadFile(downloadURL, tempName);
                Console.WriteLine("Unzipping...");
                ZipFile.ExtractToDirectory(tempName, driverDirectory);
                File.Delete(tempName);
                Console.WriteLine("Complete");
            }
            catch (WebException w)
            {
                Console.WriteLine("Download failed.");
                throw w;
            }
        }
    }
}
