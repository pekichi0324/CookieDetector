using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CookieDetector
{
    public class CookieJar : IEnumerable<Cookie>
    {
        private List<Cookie> cookieList;
        public List<Cookie> CookieList { get => cookieList; set => cookieList = value; }
       
        public CookieJar()
        {
            cookieList = new List<Cookie>();
        }
  
        public void Add(Cookie cookie)
        {
            cookieList.Add(cookie);
        }

        public void Add(CookieJar jar)
        {
            foreach (var cookie in jar)
            {
                cookieList.Add(cookie);
            }
        }
        public int Count()
        {
            return cookieList.Count();
        }

        public override string ToString()
        {
             string result = "\nTotal cookies in the jar: " + Count().ToString() + "\n\n";
            foreach (Cookie c in cookieList)
            {
                // value is null, need to fix the issue
                if (c.Type == "http")
                    result += ((HttpCookie)c).ToString();
                else if (c.Type == "flash")
                    result += ((FlashCookie)c).ToString();
                else if (c.Type == "localStorage")
                    result += ((LocalStorageCookie)c).ToString();
            }
            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return cookieList.GetEnumerator();
        }

        // A func to look for a cookie of specific name and return true is it exists in the cookieList
        public Boolean CookieWithNameExists(string name)
        {
            foreach (Cookie c in cookieList)
            {
                if (c.Name == name)
                    return true;
            }
            return false;

        }
    }
}
