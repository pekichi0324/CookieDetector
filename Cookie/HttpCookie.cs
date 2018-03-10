using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    public class HttpCookie : Cookie
    {
        private bool isSecure;
        private bool isHttpOnly;
        private string expiry;
        

        public HttpCookie() : base() { }

        // Full list: https://msdn.microsoft.com/en-us/library/system.web.httpcookie_properties(v=vs.110).aspx

        public bool IsSecure { get => isSecure; set => isSecure = value; }
        public bool IsHttpOnly { get => isHttpOnly; set => isHttpOnly = value; }
        public string Expiry { get => expiry; set => expiry = value; }
       

        // A function to convert selenium cookie object to our HttpCookie object
        public static HttpCookie ToHttpCookie(OpenQA.Selenium.Cookie c, string url) { 

            HttpCookie cookie = new HttpCookie();
            cookie.Name  = c.Name;
            cookie.Type = "http";
            cookie.Domain = c.Domain;
            cookie.Path = c.Path;
            cookie.RetrievedFrom = new Uri(url).Host;
            cookie.isSecure = c.Secure;
            cookie.expiry = c.Expiry.ToString(); 
            cookie.isHttpOnly = c.IsHttpOnly;
            cookie.DateCreated = DateTime.Now.Ticks;
            cookie.Content = c.Value;
            
            return cookie;
        }

        public override string ToString()
        {
            return
                "\n\nName: " + Name + "\n"
                + "Type: Http Cookie\n"
                + "Retrieved From" + RetrievedFrom + "\n"
                + "Domain: " + Domain + "\n"
                + "Path: " + Path + "\n"
                + "Secure: " + isSecure + "\n"
                + "Expiration: "  + expiry + "\n"
                + "Data Created: " + (new DateTime(DateCreated)).ToString() + "\n"
                + "HttpOnly: " + isHttpOnly + "\n"
                + "Key: " +  Content;
        }
    }
}
